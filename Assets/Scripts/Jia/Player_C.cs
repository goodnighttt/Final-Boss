using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_C : MonoBehaviour
{
    private GameObject soil;//锄地后的土地
    private List<GameObject> BuildList = new List<GameObject>();//存放所有种建物的数组
    //  private Transform tempBuild;
    private Vector3 tempBuild;
    private bool CanCreate;

    //鼠标单击在人物位置锄地，再点一下种某个模型
    void Start()
    {
        //soil = Resources.Load<GameObject>("soil");
        
        tempBuild = transform.position;
        

    }


 
    void Update()
    {
        if (CanCreate == true)
        {
            GameObject build = null;
            for (int i = 0; i < BuildList.Count; i++)
            {
                if (Vector3.Distance(transform.position, BuildList[i].transform.position) < 2.4f + 2)//鼠标位置与已有的田地距离<两个建筑物各一半相加再加2时，吸附
                {

                    build = BuildList[i];
                    break;
                }
            }
            if (build != null)//可以被吸附
            {
                // float offset = (build.Size / 2) + (tempBuild.Size / 2);
                Vector3 up = build.transform.position + new Vector3(0, 0, 2.4f);
                Vector3 down = build.transform.position + new Vector3(0, 0, -2.4f);
                Vector3 left = build.transform.position + new Vector3(-2.4f, 0, 0);
                Vector3 right = build.transform.position + new Vector3(2.4f, 0, 0);
                Vector3[] points = new Vector3[] { up, down, left, right };

                float dis = 10000;
                Vector3 temp = Vector3.zero;
                for (int i = 0; i < points.Length; i++)//找到四个位置中，最近的一个
                {
                    if (Vector3.Distance(transform.position, points[i]) < dis)
                    {
                        dis = Vector3.Distance(transform.position, points[i]);
                        temp = points[i];
                    }
                }
                tempBuild = temp;
            }
            else
            {
                
                tempBuild = transform.position;
            }
            //建造
            if (Input.GetMouseButtonDown(0))
            {

                //实例化一个要建造的物体
                soil = Resources.Load<GameObject>("soil");
                GameObject temp = Instantiate<GameObject>(soil, tempBuild, Quaternion.identity, null);
               // BuildList.Add(temp.GetComponent<GameObject>());
                BuildList.Add(temp);


            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            CanCreate = true;
        }
        else
        {
            CanCreate = false;
        }

      
    }
    
}
