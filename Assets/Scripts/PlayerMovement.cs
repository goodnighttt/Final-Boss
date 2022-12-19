using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]//挂载物体上要求有刚体
public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update
    //public Animator anim;
    //private bool run;
    private Rigidbody _rigidbody;
    public Vector3 CurrentInput { get; private set; }
    public float MaxWalkSpeed = 10;
    public float MaxRunSpeed = 20;
    public float turnspeed = 500;
    public float JumpForce = 300f;
    public float m_timer = 0;
    public Animator anim;
    private bool run;
   // public Transform MassCentre;
    public Transform GroundCheck;
    public float CheckRadius = 0.01f;
    private bool IsGround;

    public LayerMask layerMask;

    public bool IsJump = false;
    public bool alive = true;

    private float lastTime;
    private float curTime;

    public bool Hit = false;

    private AudioSource sound;
    public AudioClip[] footsounds;
    private AudioSource sound1;
    public AudioClip[] attacksounds;

    public float normalDis = 5;

    //切换武器/耕具效果
    public Transform playerRightHandBone;
    public Transform Hoe;
    public Transform HoeFork;
    public GameObject playerWeapon;
    public Transform[] Weapons;
    public int weaponIndex = 0;
    Transform pos;
    public string state = "idle";
    //血量值
    float HP = 100;
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        //_rigidbody.centerOfMass = MassCentre.localPosition;
        sound = GetComponent<AudioSource>();
        sound1 = GetComponent<AudioSource>();

        //Transform weaponTrans = Instantiate(Resources.Load<Transform>("Assets/Suriyun/Farmer Girl SD/Prefab/0"));
        Transform weaponTrans = Instantiate(Weapons[0]);
        
        weaponTrans.parent = playerRightHandBone;
        weaponTrans.localPosition = new Vector3(0, 0, 0);
        
        //weaponTrans.localRotation = Quaternion.Euler(168, 90, 0);
        weaponTrans.localScale = new Vector3(1f, 1f, 1f);

        playerWeapon = weaponTrans.gameObject;

    }

    //判断敌人视线碰撞//
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "Eye")
        {
            other.transform.parent.GetComponent<Slime>().checkSight();//检查视线
        }

        //if(other.tag == "Slime")
        //{
        //    Debug.Log("打一下");
        //    other.GetComponent<Slime>().TakeDamage(20);
        //}
    }

    public void Select()
    {
        GameObject[] enemy = GameObject.FindGameObjectsWithTag("Slime");//得到所有怪物
        List<GameObject> tempList = new List<GameObject>();
        //把符合攻击条件的怪物放在一个列表里面
        for (int i = 0; i < enemy.Length; i++)
        {
            float diss = Vector3.Distance(transform.position, enemy[i].transform.position);
            float angle = Vector3.Angle(transform.forward, enemy[i].transform.position - transform.position);
            if (diss < normalDis && angle < 50)
            {
                //Debug.Log(enemy[i].name);
                tempList.Add(enemy[i]);
            }
            //if(str == "SmallScale")
            //{

            //  }
            //else if(str == "BigScale")
            //{
            //    if (diss < skillDis)
            //        tempList.Add(enemy[i]);
            //}

        }

        //遍历所有符合条件的元素
        foreach (var objects in tempList)
        {
            //Debug.Log(objects.name);
            //if (objects.GetComponent<Rigidbody>() != null)
            //{
            //    objects.GetComponent<Rigidbody>().freezeRotation = true;
            //    //利用刚体组件添加爆炸力
            //    objects.GetComponent<Rigidbody>().AddExplosionForce(200, transform.position, 5);
            //}
            objects.GetComponent<Slime>().TakeDamage(20);
        }
    }

    public void footstep(int _num)
    {
        sound.clip = footsounds[_num];
        sound.Play();
    }

    public void attacksound(int _num)
    {
        sound1.clip = attacksounds[_num];
        sound1.Play();
    }

    void Start()
    {

        anim = GetComponent<Animator>();
        //run = false;
    }

    public void TakeDamage(int damageAmount)
    {
        HP -= damageAmount;
        if (HP <= 0)
        {
           
        }
        else
        {
            //GetComponent<Material>().color = new Color(255, 255, 255, 0.1f);
            //state = "damage";
            //anim.SetTrigger("Damage");

            GetComponent<Rigidbody>().freezeRotation = true;
            //利用刚体组件添加爆炸力
            GetComponent<Rigidbody>().AddExplosionForce(200, transform.position, 5);

            

        }
    }

    public void SwitchWeapon()
    {
        Destroy(playerWeapon);

        weaponIndex++;
        if (weaponIndex > 1)
        {
            weaponIndex = 0;
        }
        Transform weaponResource = Instantiate(Weapons[weaponIndex]);
        if (weaponResource != null)
        {
            playerWeapon = weaponResource.gameObject;

            playerWeapon.transform.parent = playerRightHandBone;
            playerWeapon.transform.localPosition = new Vector3(0,0,0);
            playerWeapon.transform.localRotation = Quaternion.Euler(-0, 0, 0);
            playerWeapon.transform.localScale = new Vector3(1f, 1f, 1f);
        }
    }


    // Update is called once per frame
    void Update()
    {
       
        Hit = false;
        if (CurrentInput.magnitude != 0)
        {

            Quaternion quaDir = Quaternion.LookRotation(CurrentInput, Vector3.up);
            //缓慢转动到目标点
            transform.rotation = Quaternion.Lerp(transform.rotation, quaDir, Time.deltaTime * turnspeed);
        }
        //Debug.DrawRay(transform.position, Vector2.down * 0.11f, Color.red);
        //RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 0.15f, 1 << 8);
        //if (hit.collider != null)
        //    IsGround = true;
        //else
        //    IsGround = false;
        IsGround = Physics.CheckSphere(GroundCheck.position, CheckRadius, layerMask);
        //if(IsGround == false)
            //Debug.Log(IsGround);

        if (Input.GetKeyDown(KeyCode.Space)&& IsGround)
        {
            state = "jump";
            anim.SetBool("Jump", true);
            //curTime += Time.deltaTime;
            _rigidbody.AddForce(new Vector3(0, 1, 0) * JumpForce);

            //_rigidbody.AddForce(Vector3.up * JumpForce);

            //if (curTime >= 0.2)
            //{
               
            //    curTime = 0;
            //    //lastTime = curTime;
            //}

            //System.Threading.Thread.Sleep(200); //参数为ms 即左边暂停2s
          
            
        
        }
        else
        {
            anim.SetBool("Jump", false);
        }


        if (Input.GetMouseButtonDown(0))
        {
            state = "attack";
            anim.SetBool("Attack", true);
            //GameObject.Find("HoeforkHead").GetComponent<Hit>().enabled = true;
            Select();
            //GameObject.Find("Hoefork").GetComponent<Hit>().Attack();
            //Hit = true;
            //_rigidbody.MovePosition(_rigidbody.position + CurrentInput * MaxRunSpeed * Time.fixedDeltaTime);

        }
        else
        {
            anim.SetBool("Attack", false);
            //GameObject.Find("HoeforkHead").GetComponent<Hit>().enabled = false;
        }

        if(Input.GetKeyDown(KeyCode.Z))
        {
            state = "diga";
            anim.SetBool("DigA", true);
            //HoeFork.
        }
        else
        {
            anim.SetBool("DigA", false);
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            state = "digb";
            anim.SetBool("DigB", true);
        }
        else
        {
            anim.SetBool("DigB", false);
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            SwitchWeapon();
        }
    
    }

 
  


    private void FixedUpdate()
    {

        IsGround = Physics.CheckSphere(GroundCheck.position, CheckRadius, layerMask);
        
            //Debug.Log(IsGround);

        if (Input.GetKey(KeyCode.W)|| Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        {
            state = "walk";
                if(IsGround == true)
                {
                    anim.SetBool("Walk", true);
                }
                else
            {
                anim.SetBool("Walk", false);
            }
            
            _rigidbody.MovePosition(_rigidbody.position + CurrentInput * MaxWalkSpeed * Time.fixedDeltaTime);
        }
        else
        {
            anim.SetBool("Walk", false);
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            state = "run";
            anim.SetBool("Run", true);
            _rigidbody.MovePosition(_rigidbody.position + CurrentInput * MaxRunSpeed * Time.fixedDeltaTime);

        }
        else
        {
            anim.SetBool("Run", false);
        }


     




        //if (Input.GetKeyDown("1"))
        //{
        //    anim.Play("idle", -1, 0f);
        //}

        //if (Input.GetKey(KeyCode.LeftShift))
        //{
        //    run = true;
        //}
        //else
        //{
        //    run = false;
        //}
    }


    public void SetMovementInput(Vector3 input)
    {
       
        CurrentInput = Vector3.ClampMagnitude(input, 1); 
    }
}
