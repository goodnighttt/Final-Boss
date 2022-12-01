using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject node;//�ʵ��Ԥ����
    public int boundLength, height;//���ϵĵױ߳��͸�

    List<GameObject> nodes;//�ʵ��List
    List<Spring> springs;//���ɵ�List

    float structK, shearK, bendK,//�ֱ��ǽṹ���ɡ����е��ɡ��������ɵĲ���
          structL, shearL, bendL;
    //�ṹ�����������ƺ�����α�
    //���е�����������б����α�
    //�������������������������֮��������α䣬������������������̮��


    public LineRenderer LineRenderer;//LineRenderer���
    public int flag = 0;
    public List<LineRenderer> lines1;
    public List<LineRenderer> lines2;
    public List<LineRenderer> lines3;
    public List<LineRenderer> lines4;


    //public RectTransform startPoint;//��ʼ�㣬UI��
    //public RectTransform endPoint;//�յ㣬UI��
    //public GameObject linePrefab;//һ���������¹�һ��SpriteRender������
    //public float lineWidth;//�ߵĿ��
    //private GameObject m_line;//�ߵ�ʵ��
    //private RectTransform m_rect;
    //private RectTransform m_lineRect;

    // Start is called before the first frame update
    void Start()
    {
        lines1 = new List<LineRenderer>();
        lines2 = new List<LineRenderer>();
        lines3 = new List<LineRenderer>();
        lines4 = new List<LineRenderer>();
        structL = 1f; shearL = Mathf.Sqrt(2f); bendL = 2f;
        structK = 1000f; shearK = 10f; bendK = 1f;//����ϵ��

        nodes = new List<GameObject>();

        // �����ʵ���
        for (int i = 0; i < boundLength; i++)
            for (int j = 0; j < height; j++)
            {
                GameObject o = Instantiate(node, transform);
                o.transform.position = new Vector3(i, j, 0);//��ʼλ�������
                nodes.Add(o);
            }
        //���ù̶����ʵ㣬��������������
        nodes[(boundLength*height)-1].GetComponent<SpringGrid>().mass = 100000f;
        nodes[boundLength - 1].GetComponent<SpringGrid>().mass = 100000f;

        // ����������
        springs = new List<Spring>();
        //�ṹ����
        for (int i = 0; i < boundLength; i++)//i�����еı���
        {
            for (int j = 0; j < height-1; j++)//j�����еı���
            {
                //����ͬһ���ϲ�ͬ�߶ȵ������������ļ�ĵ���

                //springs.Add(new Spring(
                //                    nodes[j * boundLength + i],
                //                    nodes[j * boundLength + i + 1],
                //                    structK,
                //                    structL));
                springs.Add(new Spring(
                                   nodes[i*height+j],
                                   nodes[i*height+j+1],
                                   structK,
                                   structL));
                GameObject test = new GameObject();
                test.name = "test";
                LineRenderer lRend = test.AddComponent<LineRenderer>();
                lRend.startWidth = 0.02f;
                lRend.endWidth = 0.02f;
                lRend.startColor = Color.white;//������ɫ
                lRend.endColor = Color.white;

                lines1.Add(lRend);
                
                //LineRenderer lRend = new LineRenderer();
                //LineRenderer.SetPosition(0, nodes[j * boundLength + i].transform.position);
                //LineRenderer.SetPosition(1, nodes[j * boundLength + i + 1].transform.position);
            }

        }
        Debug.Log(lines1.Count);
       


        for (int i = 0; i < boundLength-1; i++)
        {
            for (int j = 0; j < height; j++)
            {
                //����ͬһ���ϲ�ͬ��ȵ������������ļ�ĵ���
                //���Խṹ���ɼȿ��ƺ��� �ֿ�������

                //springs.Add(new Spring(
                //        nodes[j * boundLength + i],
                //        nodes[(j + 1) * boundLength + i],
                //        structK,
                //        structL));
                springs.Add(new Spring(
                        nodes[i*height+j],
                        nodes[(i+1)*height+j],
                        structK,
                        structL));
                GameObject test = new GameObject();
                test.name = "test";
                LineRenderer lRend = test.AddComponent<LineRenderer>();
                lRend.startWidth = 0.02f;
                lRend.endWidth = 0.02f;
                lRend.startColor = Color.white;//������ɫ
                lRend.endColor = Color.white;
                lines2.Add(lRend);
                //LineRenderer.SetPosition(0, nodes[j * boundLength + i].transform.position);
                //LineRenderer.SetPosition(1, nodes[(j + 1) * boundLength + i].transform.position);
            }
        }
        Debug.Log(lines2.Count);

        //���е���
        for (int i = 0; i < boundLength - 1; i++)
        {
            for (int j = 0; j < height - 1; j++)
            {
                //springs.Add(new Spring(
                //nodes[i * height + j],
                //nodes[(i + 1) * height + j + 1],
                //shearK,
                //shearL));
                springs.Add(new Spring(
                nodes[i * height + j],
                nodes[(i + 1) * height + j + 1],
                shearK,
                shearL));
                GameObject test = new GameObject();
                test.name = "test";
                LineRenderer lRend = test.AddComponent<LineRenderer>();
                lRend.startWidth = 0.02f;
                lRend.endWidth = 0.02f;
                lRend.startColor = Color.white;//������ɫ
                lRend.endColor = Color.white;
                lines3.Add(lRend);
                //LineRenderer.SetPosition(0, nodes[i * height + j].transform.position);
                //LineRenderer.SetPosition(1, nodes[(i + 1) * height + j + 1].transform.position);
            }
            
        }
        Debug.Log(lines3.Count);

        for (int i = 1; i < boundLength; i++)
        {
            for (int j = 0; j < height - 1; j++)
            {
                springs.Add(new Spring(
                      nodes[i * height + j],
                      nodes[(i - 1) * height + j + 1],
                      shearK,
                      shearL));
                GameObject test = new GameObject();
                test.name = "test";
                LineRenderer lRend = test.AddComponent<LineRenderer>();
                lRend.startWidth = 0.02f;
                lRend.endWidth = 0.02f;
                lRend.startColor = Color.white;//������ɫ
                lRend.endColor = Color.white;
                lines4.Add(lRend);
                //LineRenderer.SetPosition(0, nodes[i * height + j].transform.position);
                //LineRenderer.SetPosition(1, nodes[(i - 1) * height + j + 1].transform.position);
            }
        }

        Debug.Log(lines4.Count);

        //��������
        for (int i = 0; i < boundLength - 2; i++)
        {
            for (int j = 0; j < height; j++)
            {
                springs.Add(new Spring(
                        nodes[j * boundLength + i],
                        nodes[j * boundLength + i + 2],
                        bendK,
                        bendL));
               
            }
        }
           
                

        for (int i = 0; i < boundLength; i++)
        {
            for (int j = 0; j < height - 2; j++)
            {
                springs.Add(new Spring(
                nodes[j * boundLength + i],
                nodes[(j + 2) * boundLength + i],
                bendK,
                bendL));
            }
            
        }
           
    }

    // Update is called once per frame
    void Update()
    {
        foreach (Spring s in springs) 
            s.Flex();

        

        //�ṹ����
        for (int i = 0; i < boundLength; i++)
        {
            for (int j = 0; j < height-1; j++)
            {
                //GameObject test = new GameObject();
                //test.name = "test";
                //LineRenderer line1 = test.AddComponent<LineRenderer>();
                //line1.startWidth = 0.1f;
                //line1.endWidth = 0.1f;
                //GameObject line = Instantiate(linePrefab, transform.parent);
                //Debug.Log(lines1.Count);
                lines1[i * (height-1) + j].SetPosition(0, nodes[i * height + j].transform.position);
                lines1[i * (height-1) + j].SetPosition(1, nodes[i * height + j + 1].transform.position);
                //Debug.Log(i * height + j);
                //flag++;
                //flag++;
                //Destroy(test, 0.001f);
            }

        }

        //����ͬһ���ϲ�ͬ�߶ȵ������������ļ�ĵ���
        for (int i = 0; i < boundLength-1; i++)
        {
            for (int j = 0; j < height; j++)
            {
                //GameObject test = new GameObject();
                //test.name = "test";
                //LineRenderer line1 = test.AddComponent<LineRenderer>();
                //line1.startWidth = 0.1f;
                //line1.endWidth = 0.1f;
                lines2[i * height + j].SetPosition(0, nodes[i * height + j].transform.position); 
                lines2[i * height + j].SetPosition(1, nodes[(i + 1) * height + j].transform.position);
                //flag++;
                //Destroy(test, 0.001f);
            }
        }


        //����ͬһ���ϲ�ͬ��ȵ������������ļ�ĵ���
        //���Խṹ���ɼȿ��ƺ��� �ֿ�������

        //���е���
        for (int i = 0; i < boundLength - 1; i++)
        {
            for (int j = 0; j < height - 1; j++)
            {
                //GameObject test = new GameObject();
                //test.name = "test";
                //LineRenderer line1 = test.AddComponent<LineRenderer>();
                //line1.startWidth = 0.1f;
                //line1.endWidth = 0.1f;
                lines3[i * (height-1) + j].SetPosition(0, nodes[i * height + j].transform.position);
                lines3[i * (height-1) + j].SetPosition(1, nodes[(i + 1) * height + j + 1].transform.position);
                //flag++;
                //Destroy(test, 0.001f);
            }

        }


        for (int i = 1; i < boundLength; i++)
        {
            for (int j = 0; j < height - 1; j++)
            {
                //GameObject test = new GameObject();
                //test.name = "test";
                //LineRenderer line1 = test.AddComponent<LineRenderer>();
                //line1.startWidth = 0.1f;
                //line1.endWidth = 0.1f;
                lines4[(i-1) * (height-1) + j].SetPosition(0, nodes[i * height + j].transform.position);
                lines4[(i-1) * (height-1) + j].SetPosition(1, nodes[(i - 1) * height + j + 1].transform.position);
                //flag++;
                //Destroy(test, 0.001f);
            }
        }

        
       

    }
}



public class Spring
{
    public Transform nodeA, nodeB;//���ӵ������ʵ��Transform
    public float k, L;//����ϵ������Ȼ����

    public Spring(GameObject a, GameObject b, float k, float L)
    {
        nodeA = a.transform;
        nodeB = b.transform;
        this.k = k;
        this.L = L;
    }

    public void Flex()
    {
        Vector3 dAB = nodeB.transform.position - nodeA.transform.position;//��Aָ��B������
        float scalarF = k * (dAB.magnitude - L);//���������Ĵ�С
                                                //�������ʵ���е��������
        nodeA.GetComponent<SpringGrid>().AddForce(dAB.normalized * scalarF);
        nodeB.GetComponent<SpringGrid>().AddForce(-dAB.normalized * scalarF);
    }
}