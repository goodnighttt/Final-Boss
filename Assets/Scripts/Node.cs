using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject node;//质点的预设体
    public int boundLength, height;//布料的底边长和高

    List<GameObject> nodes;//质点的List
    List<Spring> springs;//弹簧的List

    float structK, shearK, bendK,//分别是结构弹簧、剪切弹簧、弯曲弹簧的参数
          structL, shearL, bendL;
    //结构弹簧用来控制横向的形变
    //剪切弹簧用来控制斜向的形变
    //弯曲弹簧用来控制相隔两个点之间的弯曲形变，以免弯曲过大导致网格坍塌


    public LineRenderer LineRenderer;//LineRenderer组件
    public int flag = 0;
    public List<LineRenderer> lines1;
    public List<LineRenderer> lines2;
    public List<LineRenderer> lines3;
    public List<LineRenderer> lines4;


    //public RectTransform startPoint;//起始点，UI用
    //public RectTransform endPoint;//终点，UI用
    //public GameObject linePrefab;//一个空物体下挂一个SpriteRender就行了
    //public float lineWidth;//线的宽度
    //private GameObject m_line;//线的实例
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
        structK = 1000f; shearK = 10f; bendK = 1f;//劲度系数

        nodes = new List<GameObject>();

        // 生成质点们
        for (int i = 0; i < boundLength; i++)
            for (int j = 0; j < height; j++)
            {
                GameObject o = Instantiate(node, transform);
                o.transform.position = new Vector3(i, j, 0);//初始位置随便设
                nodes.Add(o);
            }
        //设置固定的质点，用其质量来区分
        nodes[(boundLength*height)-1].GetComponent<SpringGrid>().mass = 100000f;
        nodes[boundLength - 1].GetComponent<SpringGrid>().mass = 100000f;

        // 构建弹簧们
        springs = new List<Spring>();
        //结构弹簧
        for (int i = 0; i < boundLength; i++)//i控制列的遍历
        {
            for (int j = 0; j < height-1; j++)//j控制行的遍历
            {
                //代表同一列上不同高度的相邻两个质心间的弹簧

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
                lRend.startColor = Color.white;//设置颜色
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
                //代表同一行上不同宽度的相邻两个质心间的弹簧
                //所以结构弹簧既控制横向 又控制竖向

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
                lRend.startColor = Color.white;//设置颜色
                lRend.endColor = Color.white;
                lines2.Add(lRend);
                //LineRenderer.SetPosition(0, nodes[j * boundLength + i].transform.position);
                //LineRenderer.SetPosition(1, nodes[(j + 1) * boundLength + i].transform.position);
            }
        }
        Debug.Log(lines2.Count);

        //剪切弹簧
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
                lRend.startColor = Color.white;//设置颜色
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
                lRend.startColor = Color.white;//设置颜色
                lRend.endColor = Color.white;
                lines4.Add(lRend);
                //LineRenderer.SetPosition(0, nodes[i * height + j].transform.position);
                //LineRenderer.SetPosition(1, nodes[(i - 1) * height + j + 1].transform.position);
            }
        }

        Debug.Log(lines4.Count);

        //弯曲弹簧
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

        

        //结构弹簧
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

        //代表同一列上不同高度的相邻两个质心间的弹簧
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


        //代表同一行上不同宽度的相邻两个质心间的弹簧
        //所以结构弹簧既控制横向 又控制竖向

        //剪切弹簧
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
    public Transform nodeA, nodeB;//链接的两个质点的Transform
    public float k, L;//劲度系数，自然长度

    public Spring(GameObject a, GameObject b, float k, float L)
    {
        nodeA = a.transform;
        nodeB = b.transform;
        this.k = k;
        this.L = L;
    }

    public void Flex()
    {
        Vector3 dAB = nodeB.transform.position - nodeA.transform.position;//由A指向B的向量
        float scalarF = k * (dAB.magnitude - L);//产生的力的大小
                                                //对两个质点进行弹力的添加
        nodeA.GetComponent<SpringGrid>().AddForce(dAB.normalized * scalarF);
        nodeB.GetComponent<SpringGrid>().AddForce(-dAB.normalized * scalarF);
    }
}