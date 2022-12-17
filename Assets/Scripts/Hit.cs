using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hit : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform pointA;
    public Transform pointB;
    public LayerMask layer;
    int hitCount;
    public Transform[] Points; //射线发射点
    public Dictionary<int, Vector3> dic_lastPoints = new Dictionary<int, Vector3>(); //存放上个位置信息
   
    //public GameObject particle;//粒子效果
    private void Start()
    {
        if (dic_lastPoints.Count == 0)
        {
            for (int i = 0; i < Points.Length; i++)
            {
                dic_lastPoints.Add(Points[i].GetHashCode(), Points[i].position);
            }
        }
    }

    //public void Select(string str)
    //{
    //    GameObject[] enemy = GameObject.FindGameObjectsWithTag("Slime");//得到所有怪物
    //    List<GameObject> tempList = new List<GameObject>();
    //    //把符合攻击条件的怪物放在一个列表里面
    //    for(int i =0;i<enemy.Length;i++)
    //    {
    //        float diss = Vector3.Distance(transform.position, enemy[i].transform.position);
    //        float angle = Vector3.Angle(transform.forward, enemy[i].transform.position - transform.position);
    //        if (diss < normalDis && angle < 50)
    //        {
    //            tempList.Add(enemy[i]);
    //        }
    //        //if(str == "SmallScale")
    //        //{

    //        //  }
    //        //else if(str == "BigScale")
    //        //{
    //        //    if (diss < skillDis)
    //        //        tempList.Add(enemy[i]);
    //        //}

    //    }

    //    //遍历所有符合条件的元素
    //    foreach (var objects in tempList)
    //    {
    //        if(objects.GetComponent<Rigidbody>()!= null)
    //        {
    //            objects.GetComponent<Rigidbody>().freezeRotation = true;
    //            //利用刚体组件添加爆炸力
    //            objects.GetComponent<Rigidbody>().AddExplosionForce(200, transform.position, 5);
    //        }
    //        objects.GetComponent<Slime>().TakeDamage(20);
    //    }
    //}




    //private void LateUpdate()
    //{
    //    var newA = pointA.position;
    //    var newB = pointB.position;
    //    //Debug.DrawLine(newA, newB, Color.red, 1f);
    //    SetPostion(Points);
    //}

    public void Attack()
    {
        var newA = pointA.position;
        var newB = pointB.position;
        //Debug.DrawLine(newA, newB, Color.red, 1f);
        SetPostion(Points);
    }

    void SetPostion(Transform[] points)
    {
        for (int i = 0; i < points.Length; i++)
        {
            var nowPos = points[i];
            dic_lastPoints.TryGetValue(nowPos.GetHashCode(), out Vector3 lastPos);
            //Debug.DrawLine(nowPos.position, lastPos, Color.blue, 1f); ;
            //Debug.DrawRay(lastPos, nowPos.position - lastPos, Color.blue, 1f);

            Ray ray = new Ray(lastPos, nowPos.position - lastPos);
            RaycastHit[] raycastHits = new RaycastHit[6];
            Physics.RaycastNonAlloc(ray, raycastHits, Vector3.Distance(lastPos, nowPos.position), layer, QueryTriggerInteraction.Ignore);

            foreach (var item in raycastHits)
            {
                if (item.collider == null) 
                    continue;
                //下面做击中后的一些判断和处理
                //比如扣血之类的,
                //需要注意:在同一帧会多次击中一个对象
                Debug.Log(item.collider.name);
                //if (particle)
                //{
                //    var go = Instantiate(particle, item.point, Quaternion.identity);
                //    Destroy(go, 3f);
                //}
                hitCount++;
                break;
            }

            if (nowPos.position != lastPos)
            {
                dic_lastPoints[nowPos.GetHashCode()] = nowPos.position;//存入上个位置信息
            }
        }
    }

    //private void OnGUI()
    //{
    //    var labelstyle = new GUIStyle();
    //    labelstyle.fontSize = 32;
    //    labelstyle.normal.textColor = Color.white;
    //    int height = 40;
    //    GUIContent[] contents = new GUIContent[]
    //    {
    //           new GUIContent($"hitCount:{hitCount}"),
    //           new GUIContent($"frameCount:{Time.frameCount }"),
    //     };

    //    for (int i = 0; i < contents.Length; i++)
    //    {
    //        GUI.Label(new Rect(0, height * i, 180, 80), contents[i], labelstyle);
    //    }
    //} 

    //public Transform HoeforkHead;
    //public Vector3 HoeforkHeadPrePosition;
    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.tag == "Slime")
    //    {
    //        Debug.Log("打一下");
    //        other.GetComponent<Slime>().TakeDamage(20);
    //    }
    //}

    //void Start()
    //{
        
    //}

    //public void Attack()
    //{
    //    //Debug.Log("调用了");
    //    Vector3 p0 = HoeforkHead.position;
    //    RaycastHit hinfo;
    //    int mask = 1 << LayerMask.NameToLayer("Slime");// 只取与FoeHitReceiver层相交
    //    GameObject hitObj = null;
    //    if (Physics.Linecast(p0, HoeforkHeadPrePosition, out hinfo, mask))
    //    {
    //        hitObj = hinfo.collider.gameObject;
    //    }
    //    if (hitObj != null)
    //    {
    //        // 击中
    //        Debug.Log("打一下");
    //        GameObject.Find("Slime_Viking").GetComponent<Slime>().TakeDamage(20);

    //    }
    //    HoeforkHeadPrePosition = p0;
    //}
    //// Update is called once per frame
    //void Update()
    //{
        
    //}
}
