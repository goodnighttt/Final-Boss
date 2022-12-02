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

    public Transform deathCam;//死亡时的摄像头
    public Transform camPos;//摄像机位置
    public enum WalkType { Patroll, ToOrigin }
    private WalkType walkType;

    private float wait = 0;
    public Transform eyes;

    private bool highAlert = false;
    private float alertness = 20f;

    public float wanderScope = 20f;
    void Start()
    {
        nav = GetComponent<NavMeshAgent>();//获得NavMeshAgent
        sound = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
        nav.speed = 1f;
        anim.speed = 1f;
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
                    if(state != "kill")
                    {
                        state = "chase";
                        nav.speed = 2f;
                        anim.speed = 2f;
                        
                    }
                }
            }
        }
    }

    protected bool AgentDone()
    {
        //有两个条件，一个是导航代理的 pathPending 属性变为 false。另一个条件是导航代理与目的地的剩余距离 remainingDistance 小于其停止距离 stoppingDistance。
        return !nav.pathPending && nav.remainingDistance <= nav.stoppingDistance;
    }


    // Update is called once per frame
    void Update()
    {
        //Debug.DrawLine(eyes.position, player.transform.position, Color.green);
        //Debug.Log(nav.remainingDistance);
        if(alive)
        {
            anim.SetFloat("velocity", nav.velocity.magnitude);

            //Idle//
            if(state == "idle")
            {
               // Debug.Log("默认状态");
                //随机选择一个目标移动
                Vector3 randomPos = Random.insideUnitSphere * alertness;
                Debug.Log(randomPos);
                NavMeshHit navhit;
                NavMesh.SamplePosition(transform.position + randomPos, out navhit, 20f, NavMesh.AllAreas);

                //if(AgentDone())
                //{
                //    Vector3 randomRange = new Vector3((Random.value - 0.5f) * 2 * wanderScope, 0, (Random.value - 0.5f) * 2 * wanderScope);

                //    Vector3 nextDestination = transform.position + randomRange;

                //    nav.destination = nextDestination;
                //}


                //Go near the player
                if (highAlert)
                {
                    NavMesh.SamplePosition(player.transform.position + randomPos, out navhit, 20f, NavMesh.AllAreas);
                    //每一次，失去了对于玩家位置的视觉
                    alertness += 5f;

                    if (alertness > 20f)
                    {
                        highAlert = false;
                        nav.speed = 1f;
                        anim.speed = 1f;
                    }
                }


                nav.SetDestination(navhit.position);
                state = "walk";

            }

            //Walk//
            if(state == "walk")
            {
                //Debug.Log("开始散步");
                if(nav.remainingDistance <= nav.stoppingDistance && !nav.pathPending)
                {
                    state = "search";
                    wait = 3f;
                }
            }
            
            //Search//
            if(state == "search")
            {
               // Debug.Log("开始搜寻");
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
                //Debug.Log("开始追赶");
                nav.destination = player.transform.position;

                //失去玩家的视野//
                float distance = Vector3.Distance(transform.position, player.transform.position);
                if(distance > 10f)
                {
                    state = "hunt";
                }
                //else if(nav.remainingDistance <= nav.stoppingDistance + 1f && !nav.pathPending)
                //{
                //    if(player.GetComponent<PlayerMovement>().alive)
                //    {
                //        state = "kill";
                //        player.GetComponent<PlayerMovement>().alive = false;
                //        //player.GetComponent<PlayerMovement>();
                //    }
                //}
            }

            //Hunt//
            if(state == "hunt")
            {
                Debug.Log("开始狩猎");
                if ((nav.remainingDistance <= nav.stoppingDistance) && (!nav.pathPending))
                {
                    float distance = Vector3.Distance(transform.position, player.transform.position);
                    Debug.Log(distance);
                    //if (distance < 9)
                    //{
                        //nav.enabled = false;
                        Debug.Log("移动结束了");
                        state = "search";
                        wait = 3f;
                        highAlert = true;
                        alertness = 5f;
                        checkSight();
                    //}

                }
            }
        }
       
        //nav.SetDestination(player.transform.position);
        
    
    
    }
}
