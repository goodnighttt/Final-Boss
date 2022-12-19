using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_C : MonoBehaviour
{
    private GameObject soil;//���غ������
    private List<GameObject> BuildList = new List<GameObject>();//��������ֽ��������
    //  private Transform tempBuild;
    private Vector3 tempBuild;
    private bool CanCreate;

    //��굥��������λ�ó��أ��ٵ�һ����ĳ��ģ��
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
                if (Vector3.Distance(transform.position, BuildList[i].transform.position) < 2.4f + 2)//���λ�������е���ؾ���<�����������һ������ټ�2ʱ������
                {

                    build = BuildList[i];
                    break;
                }
            }
            if (build != null)//���Ա�����
            {
                // float offset = (build.Size / 2) + (tempBuild.Size / 2);
                Vector3 up = build.transform.position + new Vector3(0, 0, 2.4f);
                Vector3 down = build.transform.position + new Vector3(0, 0, -2.4f);
                Vector3 left = build.transform.position + new Vector3(-2.4f, 0, 0);
                Vector3 right = build.transform.position + new Vector3(2.4f, 0, 0);
                Vector3[] points = new Vector3[] { up, down, left, right };

                float dis = 10000;
                Vector3 temp = Vector3.zero;
                for (int i = 0; i < points.Length; i++)//�ҵ��ĸ�λ���У������һ��
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
            //����
            if (Input.GetMouseButtonDown(0))
            {

                //ʵ����һ��Ҫ���������
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
