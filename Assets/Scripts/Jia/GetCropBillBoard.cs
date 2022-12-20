using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetCropBillBoard : MonoBehaviour
{
    public Camera camera3d;
    public float distance = 10;
    Vector3 oldScale;
    private Vector3 vecLookAtPos; //ע�ӵ�Ŀ�귽λ
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
        //�ѵ���ע��Ŀ�귽λ����Ϊ����
        vecLookAtPos = player.transform.position;
        //��������ע��Ŀ�귽λ��Yֵ��ʹ������˵�һ�£���������ƽ�ӣ���������ӻ��߸���
        vecLookAtPos.y = transform.position.y;
        transform.LookAt(vecLookAtPos);
        //SetQuadScale();
    }

}
