using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Slime : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject player;
    public AudioClip[] footsounds;
    private NavMeshAgent nav;

    private AudioSource sound;
    private Animator anim;
    private string state = "idle";
    private bool alive = true;
    public enum WalkType { Patroll, ToOrigin }
    private WalkType walkType;

    private float wait = 0;
    public Transform eyes;

    private bool highAlert = false;
    private float alertness = 20f;
    void Start()
    {
        nav = GetComponent<NavMeshAgent>();//获得NavMeshAgent
        sound = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
        nav.speed = 1.2f;
        anim.speed = 1.2f;
    }

    public void footstep(int _num)
    {
        sound.clip = footsounds[_num];
        sound.Play();
    }

    //检测AI是否可以看到玩家
    public void checkSight()
    {
        if(alive)
        {
            RaycastHit ray;
            if(Physics.Linecast(eyes.position,player.transform.position,out ray))
            {
                //print("hit" + ray.collider.gameObject.name);
                if(ray.collider.gameObject.name == "Malamute")
                {
                    if(state!= "kill")
                    {
                        state = "chase";
                        nav.speed = 3.5f;
                        anim.speed = 3.5f;
                        
                    }
                }
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        //Debug.DrawLine(eyes.position, player.transform.position, Color.green);
        //nav.isStopped = false;
        //nav.updateRotation = true;
        //if (walkType == WalkType.ToOrigin)
        //{
        //    nav.SetDestination(originPos);
        //    // Debug.Log("WalkToOrg");
        //    //SetFace(faces.WalkFace);
        //    // agent reaches the destination
        //    if (nav.remainingDistance < nav.stoppingDistance)
        //    {
        //        walkType = WalkType.Patroll;

        //        //facing to camera
        //        transform.rotation = Quaternion.identity;

        //        //currentState = SlimeAnimationState.Idle;
        //    }

        //}
        if(alive)
        {
            anim.SetFloat("velocity", nav.velocity.magnitude);

            //Idle//
            if(state == "idle")
            {
                //随机选择一个目标移动
                Vector3 randomPos = Random.insideUnitSphere * alertness;
                NavMeshHit navhit;
                NavMesh.SamplePosition(transform.position + randomPos,out navhit,20f,NavMesh.AllAreas);
               

                //Go near the player
                if (highAlert)
                {
                    NavMesh.SamplePosition(player.transform.position + randomPos, out navhit, 20f, NavMesh.AllAreas);
                    //每一次，失去了对于玩家位置的视觉
                    alertness += 5f;

                    if(alertness >20f)
                    {
                        highAlert = false;
                        nav.speed = 1.2f;
                        anim.speed = 1.2f;
                    }
                }


                nav.SetDestination(navhit.position);
                state = "walk";

            }

            //Walk//
            if(state == "walk")
            {
                if(nav.remainingDistance <= nav.stoppingDistance && !nav.pathPending)
                {
                    state = "search";
                    wait = 5f;
                }
            }
            
            //Search//
            if(state == "search")
            {
                if(wait > 0f)
                {
                    wait -= Time.deltaTime;
                    transform.Rotate(0, 120f * Time.deltaTime, 0f);
                }
                else
                {
                    state = "idle";
                }
            }

            //Chase//
            if(state == "chase")
            {
                nav.destination = player.transform.position;

                //失去玩家的视野//
                float distance = Vector3.Distance(transform.position, player.transform.position);
                if(distance > 10f)
                {
                    state = "hunt";
                }
            }

            //Hunt//
            if(state == "hunt")
            {
                if (nav.remainingDistance <= nav.stoppingDistance && !nav.pathPending)
                {
                    state = "search";
                    wait = 5f;
                    highAlert = true;
                    alertness = 5f;
                    checkSight();
                }
            }
        }
       
        //nav.SetDestination(player.transform.position);
        
    
    
    }
}
