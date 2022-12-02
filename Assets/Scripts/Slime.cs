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

    public Transform deathCam;//����ʱ������ͷ
    public Transform camPos;//�����λ��
    public enum WalkType { Patroll, ToOrigin }
    private WalkType walkType;

    private float wait = 0;
    public Transform eyes;

    private bool highAlert = false;
    private float alertness = 20f;

    public float wanderScope = 20f;
    void Start()
    {
        nav = GetComponent<NavMeshAgent>();//���NavMeshAgent
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

    //���AI�Ƿ���Կ������
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
        //������������һ���ǵ�������� pathPending ���Ա�Ϊ false����һ�������ǵ���������Ŀ�ĵص�ʣ����� remainingDistance С����ֹͣ���� stoppingDistance��
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
               // Debug.Log("Ĭ��״̬");
                //���ѡ��һ��Ŀ���ƶ�
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
                    //ÿһ�Σ�ʧȥ�˶������λ�õ��Ӿ�
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
                //Debug.Log("��ʼɢ��");
                if(nav.remainingDistance <= nav.stoppingDistance && !nav.pathPending)
                {
                    state = "search";
                    wait = 3f;
                }
            }
            
            //Search//
            if(state == "search")
            {
               // Debug.Log("��ʼ��Ѱ");
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
                //Debug.Log("��ʼ׷��");
                nav.destination = player.transform.position;

                //ʧȥ��ҵ���Ұ//
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
                Debug.Log("��ʼ����");
                if ((nav.remainingDistance <= nav.stoppingDistance) && (!nav.pathPending))
                {
                    float distance = Vector3.Distance(transform.position, player.transform.position);
                    Debug.Log(distance);
                    //if (distance < 9)
                    //{
                        //nav.enabled = false;
                        Debug.Log("�ƶ�������");
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
