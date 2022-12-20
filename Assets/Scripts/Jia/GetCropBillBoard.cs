using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetCropBillBoard : MonoBehaviour
{
    public Camera camera3d;
    public float distance = 10;
    Vector3 oldScale;
    private Vector3 vecLookAtPos; //注视的目标方位
    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        oldScale = transform.localScale;
        player = GameObject.FindGameObjectWithTag("Player");
    }
    //Vector2 CalcWidthAndHeight()
    //{
    //    float frustumHeight = 2.0f * distance * Mathf.Tan(camera3d.fieldOfView * 0.5f * Mathf.Deg2Rad);
    //    float frustumWidth = frustumHeight * camera3d.aspect;
    //    return new Vector2(frustumWidth, frustumHeight);
    //}
    //void SetQuadScale()
    //{
    //    Vector2 furstum = CalcWidthAndHeight();
    //    transform.localScale = new Vector3(furstum.x * oldScale.x, furstum.y * oldScale.y, 1);
    //    transform.localPosition = new Vector3(0, 0, distance);
    //}
    // Update is called once per frame
    void FixedUpdate()
    {
        //把敌人注视目标方位设置为主角
        vecLookAtPos = player.transform.position;
        //重新设置注视目标方位的Y值，使其与敌人的一致，这样才能平视，否则会仰视或者俯视
        vecLookAtPos.y = transform.position.y;
        transform.LookAt(vecLookAtPos);
        //SetQuadScale();
    }

}
