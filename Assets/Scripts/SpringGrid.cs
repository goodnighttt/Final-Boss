using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpringGrid : MonoBehaviour
{
    // Start is called before the first frame update
    public float mass = 1f;//�ʵ������
    public Vector3 v;//�ʵ���ٶ�
    // Start is called before the first frame update
    void Start()
    {

    }
    public float dt = 0.0007f;//144֡��ÿ֡��ʱ7ms
    // Update is called once per frame
    void Update()
    {
        transform.position += v * dt;//�ٶȲ�����λ��
        v *= 0.99f;//����������ʹ�ٶȼ�С
        AddForce(new Vector3(0, -9.8f, 0));//������
    }

    public void AddForce(Vector3 force)
    {
        if (mass > 100) 
            return;//����������ĳһֵ��������������ֱ�ӷ��أ����ڹ̶��ĵ㡣
        Vector3 a = force / mass;//���������ڵ�ǰ�ʵ��ϲ����ļ��ٶ�
        v += a * dt;//���ٶȶ��ʵ��ٶȵ����ã��������ٶ�
        transform.position += 0.5f * a * dt * dt;//���ٶȲ�����λ��
    }
}
