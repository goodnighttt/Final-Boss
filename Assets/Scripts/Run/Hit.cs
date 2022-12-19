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
    public Transform[] Points; //���߷����
    public Dictionary<int, Vector3> dic_lastPoints = new Dictionary<int, Vector3>(); //����ϸ�λ����Ϣ
   
    //public GameObject particle;//����Ч��
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
    //    GameObject[] enemy = GameObject.FindGameObjectsWithTag("Slime");//�õ����й���
    //    List<GameObject> tempList = new List<GameObject>();
    //    //�ѷ��Ϲ��������Ĺ������һ���б�����
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

    //    //�������з���������Ԫ��
    //    foreach (var objects in tempList)
    //    {
    //        if(objects.GetComponent<Rigidbody>()!= null)
    //        {
    //            objects.GetComponent<Rigidbody>().freezeRotation = true;
    //            //���ø��������ӱ�ը��
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
                //���������к��һЩ�жϺʹ���
                //�����Ѫ֮���,
                //��Ҫע��:��ͬһ֡���λ���һ������
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
                dic_lastPoints[nowPos.GetHashCode()] = nowPos.position;//�����ϸ�λ����Ϣ
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
    //        Debug.Log("��һ��");
    //        other.GetComponent<Slime>().TakeDamage(20);
    //    }
    //}

    //void Start()
    //{
        
    //}

    //public void Attack()
    //{
    //    //Debug.Log("������");
    //    Vector3 p0 = HoeforkHead.position;
    //    RaycastHit hinfo;
    //    int mask = 1 << LayerMask.NameToLayer("Slime");// ֻȡ��FoeHitReceiver���ཻ
    //    GameObject hitObj = null;
    //    if (Physics.Linecast(p0, HoeforkHeadPrePosition, out hinfo, mask))
    //    {
    //        hitObj = hinfo.collider.gameObject;
    //    }
    //    if (hitObj != null)
    //    {
    //        // ����
    //        Debug.Log("��һ��");
    //        GameObject.Find("Slime_Viking").GetComponent<Slime>().TakeDamage(20);

    //    }
    //    HoeforkHeadPrePosition = p0;
    //}
    //// Update is called once per frame
    //void Update()
    //{
        
    //}
}
