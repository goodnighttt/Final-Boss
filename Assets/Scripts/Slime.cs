using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;


public class Slime : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject player;
    public AudioClip[] footsounds;
    public AudioClip[] attacksounds;
    private NavMeshAgent nav;

    private AudioSource sound;
    private AudioSource sound1;
    private Animator anim;
    public GameObject SmileBody;
    private Material faceMaterial;
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


    public float proTime = 0.0f;
    public float NextTime = 0.0f;

    private Rigidbody rigidbody;

    private Vector3 vecLookAtPos; //注视的目标方位

    private int HP = 100;
    bool isatk = false;
    public float timer = 4.0f;


    public Slider healthBar;
    public void TakeDamage(int damageAmount)
    {
        HP -= damageAmount;
        if(HP <= 0)
        {
            nav.enabled = false;
            alive = false;
            //state = "die";
            anim.SetTrigger("die");
            //faceMaterial.SetTexture(TexID,Tex);
            GameObject.Destroy(gameObject,2f);
        }
        else
        {
            //GetComponent<Material>().color = new Color(255, 255, 255, 0.1f);
            //state = "damage";
            anim.SetTrigger("damage");
            
             GetComponent<Rigidbody>().freezeRotation = true;
                //利用刚体组件添加爆炸力
             GetComponent<Rigidbody>().AddExplosionForce(200, transform.position, 5);
            //alive = false;
            isatk = true;
            state = "idle";

        }
    }


    void Start()
    {
        rigidbody = this.GetComponent<Rigidbody>();
        nav = GetComponent<NavMeshAgent>();//获得NavMeshAgent
        sound = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
        //faceMaterial = SmileBody.GetComponent<Renderer>().materials[1];
        nav.speed = 1f;
        anim.speed = 1f;
    }

    public void footstep(int _num)
    {
        sound.clip = footsounds[_num];
        sound.Play();
    }

    public void attack(int _num)
    {
        
        sound.clip = attacksounds[_num];
        sound.Play();
    }

    //检测AI是否可以看到玩家
    public void checkSight()
    {
        //rigidbody.constraints = RigidbodyConstraints.FreezeRotationY;
        //Debug.Log("正在视线检测");
       

        if(alive && isatk == false)
        {
            RaycastHit ray;
            if(Physics.Linecast(eyes.position,player.transform.position,out ray))
            {
                //print("hit" + ray.collider.gameObject.name);
                if(ray.collider.gameObject.transform.parent.name == "Mesh")
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

    //IEnumerator Attacking()
    //{
    //    float distance = Vector3.Distance(transform.position, player.transform.position);
    //    anim.SetBool("attack", true);
    //    //anim.Play("Idle",-1);
    //    if (distance > 5f)
    //    {
    //        anim.SetBool("attack", false);
    //        state = "chase";
    //    }

    //    yield return new WaitForSeconds(1.5f);
    //}

    public void Attacking()
    {
        float distance = Vector3.Distance(transform.position, player.transform.position);
        anim.SetBool("attack", true);
        //anim.Play("Idle",-1);
        if (distance > 5f)
        {
            anim.SetBool("attack", false);
            state = "chase";
        }
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.value = HP;
        //Debug.Log(state);
        //Debug.DrawLine(eyes.position, player.transform.position, Color.green);
        //Debug.Log(nav.remainingDistance);
        if (isatk == true)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                isatk = false;
                timer = 4.0f;
            }
        }

        if (alive)
        {
            anim.SetFloat("velocity", nav.velocity.magnitude);

            //Idle//
            if(state == "idle")
            {
               // Debug.Log("默认状态");
                //随机选择一个目标移动
                Vector3 randomPos = Random.insideUnitSphere * alertness;
                //Debug.Log(randomPos);
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
                nav.destination = player.transform.position + Random.insideUnitSphere * 0.1f;

                //失去玩家的视野//
                float distance = Vector3.Distance(transform.position, player.transform.position);
                if(distance > 10f)
                {
                    state = "hunt";
                    //anim.SetBool("attack", false);
                }
                if(distance < 2.5f)
                {
                    //StartCoroutine(Attacking());
                    state = "attack";
                    //anim.SetBool("attack", true);
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


            //Attack//
            if(state == "attack")
            {
                //InvokeRepeating("Attacking", 1.0f, 1.0f);
                
                //把敌人注视目标方位设置为主角
                vecLookAtPos = player.transform.position;
                //重新设置注视目标方位的Y值，使其与敌人的一致，这样才能平视，否则会仰视或者俯视
                vecLookAtPos.y = transform.position.y;
                anim.transform.LookAt(vecLookAtPos);

                proTime = Time.fixedTime;
                if (proTime - NextTime >= 2.5)
                //if (proTime - NextTime > 3) 
                {
                    float distance = Vector3.Distance(transform.position, player.transform.position);
                    //anim.SetBool("attack", true);
                    //state = "chase";
                    //anim.Play("Idle",-1);
                    anim.Play("Attack",0,0f);
                    GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().TakeDamage(20);
                    if (distance > 8f)
                    {
                        Debug.Log(distance);
                        anim.SetBool("attack", false);
                        state = "chase";
                    }
                    //print("FixedTime Here" + (proTime - NextTime));

                    //transform.Translate (Vector3.up*scaleSpeed*Time.deltaTime);
                    NextTime = proTime;
                }
                

               


                //nav.enabled = true;
                //nav.destination = player.transform.position;

                //NavMeshHit navhit1;
                //Vector3 randomPos1 = Random.insideUnitSphere * 1;
                //NavMesh.SamplePosition(player.transform.position + randomPos1, out navhit1, 1.5f, NavMesh.AllAreas);
                //nav.SetDestination(navhit1.position);

            }

            //Die//
           

            //Hunt//
            if(state == "hunt")
            {
                //Debug.Log("开始狩猎");
                if ((nav.remainingDistance <= nav.stoppingDistance) && (!nav.pathPending))
                {
                    state = "search";
                    wait = 3f;
                    highAlert = true;
                    alertness = 5f;
                    checkSight();
                    //float distance = Vector3.Distance(transform.position, player.transform.position);
                    //Debug.Log(distance);
                    //if (distance < 9)
                    //{
                        //nav.enabled = false;
                       // Debug.Log("移动结束了");
                        
                    //}

                }
            }
        }
       
        //nav.SetDestination(player.transform.position);
        
    
    
    }
}
