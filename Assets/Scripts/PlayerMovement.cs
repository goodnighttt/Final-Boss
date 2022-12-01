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
    public float JumpForce = 200f;
    public float m_timer = 0;
    public Animator anim;
    private bool run;
    public Transform MassCentre;
    public Transform GroundCheck;
    public float CheckRadius = 1f;
    private bool IsGround;

    public LayerMask layerMask;

    public bool IsJump = false;
    public bool alive = true;

    private float lastTime;
    private float curTime;
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        //_rigidbody.centerOfMass = MassCentre.localPosition;
       
    }

    //判断敌人视线碰撞//
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "Eye")
        {
            other.transform.parent.GetComponent<Slime>().checkSight();//检查视线
        }
    }
    void Start()
    {

        anim = GetComponent<Animator>();
        //run = false;
    }

    // Update is called once per frame
    void Update()
    {
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
        //Debug.Log(IsGround);

        if (Input.GetKeyDown(KeyCode.Space)&& IsGround)
        {
            
            anim.SetBool("Jump", true);
            //curTime += Time.deltaTime;
            _rigidbody.AddForce(new Vector3(0, 1, 0) * JumpForce * 2);

            _rigidbody.AddForce(Vector3.up * JumpForce);

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
    }

 
  


    private void FixedUpdate()
    {
        
       

        if (Input.GetKey(KeyCode.W)|| Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        {
            anim.SetBool("Walk", true);
            _rigidbody.MovePosition(_rigidbody.position + CurrentInput * MaxWalkSpeed * Time.fixedDeltaTime);
        }
        else
        {
            anim.SetBool("Walk", false);
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
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
