using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_C : MonoBehaviour
{
    private GameObject soil;//���غ������
    private List<GameObject> BuildList = new List<GameObject>();//��������ֽ��������
    private List<GameObject> CropList = new List<GameObject>();//�������ũ���������
    //  private Transform tempBuild;
    private Vector3 tempBuild;
    private bool CanSoil;//�����ж�
    public bool CanCrop;//��ũ�����ж�
    public bool WantCrop;
    public bool CanGetCrop;//�ջ�ũ�����ж�
    private GameObject oldCrop;
   
    //��ײ�������ص�λ�ã�������ũ����
    private Vector3 soilPos;
    public GameObject cropPrefab;//ũ����ģ��
    public int cropCost;//ũ����۸�

    public static Player_C Instance;


    private bool Preview;
    public GameObject Prefab_Crop_Empty;//�յ�

    //�����ʱ���еĿյ�
    private GameObject crop_Empty;
    private void Awake()
    {
        Instance = this;
    }

    //��굥��������λ�ó��أ��ٵ�һ����ĳ��ģ��
    void Start()
    {
        //soil = Resources.Load<GameObject>("soil");

        //tempBuild = transform.position;
        crop_Empty = GameObject.Instantiate<GameObject>(Prefab_Crop_Empty);

    }


 
    void Update()
    {
        if (WantCrop == true)
        {
            Preview = false;
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            WantCrop = false;
            if (CanSoil == true)
            {
                Preview = !Preview;

            }
        }

        if (Input.GetKeyDown(KeyCode.X) && Preview == false)
        {
            //if (CanSoil == true && WantCrop == false)
            //{
            //    makeSoil();
            //}

            if (CanCrop == true && WantCrop==true)
            {
                makeCrop();
            }


        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (CanGetCrop == true)
            {
                getCrop();
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            WantCrop = false;
        }

        if (Preview == true)
        {
            crop_Empty.SetActive(true);
            Destroy(crop_Empty.GetComponent<Rigidbody>());
            Destroy(crop_Empty.GetComponent<MeshCollider>());
            crop_Empty.GetComponent<MeshRenderer>().material.color = new Color(100, 100, 100, 0.1f);


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
                Vector3 left = build.transform.position + new Vector3(-2.4f,0, 0);
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

                //���tempλ�����Ƿ��Ѿ��������ˣ���������أ���ô�Ͳ��ܷ���
                for (int i = 0; i < BuildList.Count; i++)
                {
                   if(temp == BuildList[i].transform.position)
                    {
                        Preview = false;
                    }
                }

                tempBuild = temp;
            }
            else
            {

                tempBuild = transform.position;
            }
            //����



            //ʵ����һ��Ҫ���������Ԥ��Ч��
            crop_Empty.transform.position = tempBuild;
            if (Input.GetKeyDown(KeyCode.X))
            {
                soil = Resources.Load<GameObject>("soil");
                GameObject tempObj = Instantiate<GameObject>(soil, tempBuild, Quaternion.identity, null);
                //BuildList.Add(temp.GetComponent<GameObject>());
                BuildList.Add(tempObj);



            }
            //soil = Resources.Load<GameObject>("soil");
            //GameObject tempObj = Instantiate<GameObject>(soil, tempBuild, Quaternion.identity, null);
            // BuildList.Add(temp.GetComponent<GameObject>());
            //BuildList.Add(tempObj);
        }
        else
        {
            crop_Empty.SetActive(false);
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        //�����ж�
        if (collision.gameObject.tag == "Ground"&& collision.gameObject.tag != "Soil")
        {
            CanSoil = true;
        }
        else
        {
            CanSoil = false;
        }


        //��ֲũ�����ж�
        if (collision.gameObject.tag == "Soil")
        {
            CanCrop = true;
            soilPos = collision.gameObject.transform.position;
            Debug.Log(soilPos);
        }
        else
        {
            CanCrop = false;
          
        }

        //�ջ�ũ�����ж�
        if (collision.gameObject.tag == "CropLv2")
        {
            CanGetCrop = true;
            oldCrop = collision.gameObject;

        }
        else
        {
            CanGetCrop = false;
        }


    }

    //����
    private void makeSoil()
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
       
        

            //ʵ����һ��Ҫ���������
            soil = Resources.Load<GameObject>("soil");
            GameObject tempObj = Instantiate<GameObject>(soil, tempBuild, Quaternion.identity, null);
            // BuildList.Add(temp.GetComponent<GameObject>());
            BuildList.Add(tempObj);


        
    }
    

    //��ũ����
    public void makeCrop()
    {
        //���tempλ�����Ƿ��Ѿ��������ˣ���������أ���ô�Ͳ��ܷ���
        GameObject tempcrop;
        bool isPlanted = false;
        Debug.Log(CropList.Count);
        if(CropList.Count == 0)
        {
            //ʵ����һ��Ҫ���������
            //soil = Resources.Load<GameObject>("soil");
            tempcrop = Instantiate<GameObject>(cropPrefab, soilPos, Quaternion.identity, null);
            CropList.Add(tempcrop);
            //�۽��
            UI_MainPanel.Instance.Money.text = (int.Parse(UI_MainPanel.Instance.Money.text) - cropCost).ToString();
        }
        else
        {
            for (int i = 0; i < CropList.Count; i++)
            {
                if (soilPos == CropList[i].transform.position)
                {
                    isPlanted = true;
                    break;
                }
                
            }

            if(isPlanted == false)
            {
                //ʵ����һ��Ҫ���������
                //soil = Resources.Load<GameObject>("soil");
                tempcrop = Instantiate<GameObject>(cropPrefab, soilPos, Quaternion.identity, null);
                CropList.Add(tempcrop);
                //�۽��
                UI_MainPanel.Instance.Money.text = (int.Parse(UI_MainPanel.Instance.Money.text) - cropCost).ToString();
            }
        }
        

        ////ʵ����һ��Ҫ���������
        ////soil = Resources.Load<GameObject>("soil");
        //GameObject tempcrop = Instantiate<GameObject>(cropPrefab, soilPos, Quaternion.identity, null);
        //CropList.Add(tempcrop);
        ////�۽��
        //UI_MainPanel.Instance.Money.text = (int.Parse(UI_MainPanel.Instance.Money.text) - cropCost).ToString();
    }

    //�ջ�
    public void getCrop()
    {
        Destroy(oldCrop);
        UI_MainPanel.Instance.Money.text = (int.Parse(UI_MainPanel.Instance.Money.text) +200).ToString();
    }
}
