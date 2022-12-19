using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpringGrid : MonoBehaviour
{
    // Start is called before the first frame update
    public float mass = 1f;//质点的质量
    public Vector3 v;//质点的速度
    // Start is called before the first frame update
    void Start()
    {

    }
    public float dt = 0.0007f;//144帧，每帧用时7ms
    // Update is called once per frame
    void Update()
    {
        transform.position += v * dt;//速度产生的位移
        v *= 0.99f;//空气阻力会使速度减小
        AddForce(new Vector3(0, -9.8f, 0));//重力。
    }

    public void AddForce(Vector3 force)
    {
        if (mass > 100) 
            return;//若质量大于某一值，不作受力计算直接返回，用于固定的点。
        Vector3 a = force / mass;//此力作用于当前质点上产生的加速度
        v += a * dt;//加速度对质点速度的作用：用来加速度
        transform.position += 0.5f * a * dt * dt;//加速度产生的位移
    }
}
