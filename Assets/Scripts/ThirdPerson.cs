using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPerson : MonoBehaviour
{
    // Start is called before the first frame update
    public float Pitch { get; private set; }//抬升，绕着X轴旋转
    public float Yaw { get; private set; }//绕着Y轴旋转
    public float mouseSensitivity = 3;//灵敏度
    public float cameraRotatingSpeed = 80;

    public float cameraYSpeed = 5;
    private Transform _target;
    private Transform _camera1;
    private Transform _camera2;
    //public Camera camera1; 
    //public Camera camera2;
    public Camera[] cameralist;
    [SerializeField]
    private AnimationCurve _armLengthCurve;
    private int camindex = 0;

    private void Awake()
    {
        //_camera1 = transform.GetChild(0);//获得空物体下的子物体Camera
        //_camera2 = transform.GetChild(1);
        //_camera2.enabled = false;
        cameralist[0].enabled = true;
        cameralist[1].enabled = false;
        //camera1.enabled = true;
        //camera2.enabled = false;
        _camera1 = cameralist[0].transform;
        //_camera2 = cameralist[1].transform;
    }

    void Start()
    {

    }

    public void InitCamera(Transform target)
    {
        this._target = target;
        transform.position = target.position;
    }
    // Update is called once per frame
    void Update()
    {
        UpdateRotation();
        UpdatePosition();
        UpdateArmLength();
        ChangeView();
    }

    

    private void UpdateRotation()
    {
        Yaw += Input.GetAxis("Mouse X") * mouseSensitivity;
       // Yaw += Input.GetAxis("CameraRateX") * cameraRotatingSpeed * Time.deltaTime;
        Pitch += Input.GetAxis("Mouse Y") * mouseSensitivity;
       // Pitch += Input.GetAxis("CameraRateY") * cameraRotatingSpeed * Time.deltaTime;
        Pitch = Mathf.Clamp(Pitch, -30, 90);//本语句可以限制摄像镜头的旋转角度

        transform.rotation = Quaternion.Euler(Pitch, Yaw, 0);
    }

    private void UpdatePosition()
    {
        Vector3 position = _target.position;
        float newY = Mathf.Lerp(transform.position.y, _target.position.y+1, Time.deltaTime * cameraYSpeed);
        transform.position = new Vector3(position.x, newY, position.z);
    }


    private void UpdateArmLength()
    {
        _camera1.localPosition = new Vector3(0,0, _armLengthCurve.Evaluate(Pitch) * -1);
    }

    private void ChangeView()
    {
        if(Input.GetKeyDown(KeyCode.V))
        {
            //_camera1 = transform.GetChild(1);//获得空物体下的俯视视角Camera
            camindex += 1;
            if(camindex % 2 == 0)
            {
                cameralist[0].enabled = true;
                cameralist[1].enabled = false;
                cameralist[0].fieldOfView = 60;
                _camera1 = cameralist[0].transform;
            }
            else
            {
                cameralist[1].enabled = true;
                cameralist[0].enabled = false;
                cameralist[1].fieldOfView = 80;
                _camera1 = cameralist[1].transform;
            }
            //camindex = camindex % 2;
            //cameralist[camindex].enabled = true;
            
            ////_camera2.enabled = true;
            
            //_camera1 = _camera2.transform;
            
        }
    }
}
