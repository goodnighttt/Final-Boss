using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_CreatePanel : MonoBehaviour
{

    private Button CloseButton;

    private CreateConf cropConf;
    private CreateConf flowerConf;
    private CreateConf animalConf;
    private Button cropButton;
    private Button flowerButton;
    private Button animalButton;

    private GameObject UI_Prefab;
    private Transform parent;//用于标记父节点的位置


    public static UI_CreatePanel Instance;
    private void Awake()
    {
        Instance = this;
    }


    void Start()
    {
        //农作物配置
        cropConf = Resources.Load<CreateConf>("Conf/Crops");
        //花朵配置
        flowerConf = Resources.Load<CreateConf>("Conf/Flowers");
        //动物配置
        animalConf = Resources.Load<CreateConf>("Conf/Animals");

 

        //获取分类按钮
        cropButton = transform.Find("type/crops").GetComponent<Button>();
        flowerButton = transform.Find("type/flowers").GetComponent<Button>();
        animalButton = transform.Find("type/animals").GetComponent<Button>();
        cropButton.onClick.AddListener(CropButtonOnClick);//点击事件
        flowerButton.onClick.AddListener(FlowerButtonOnClick);//点击事件
        animalButton.onClick.AddListener(AnimalButtonOnClick);//点击事件

        UI_Prefab = Resources.Load<GameObject>("UI/CreateItem");
        parent = transform.Find("Background/groups");


        CloseButton = transform.Find("Background/close").GetComponent<Button>();
        CloseButton.onClick.AddListener(CloseButtonClick);
        SetActive(false);

        //创建全部建造选项，并初始化数值
        for (int i = 0; i < cropConf.CreateConfItems.Length; i++)
        {
            //Debug.Log(createConf.CreateConfItems[i].Name);
            UI_CreateItem item = Instantiate(UI_Prefab, parent).GetComponent<UI_CreateItem>();
            item.Init(cropConf.CreateConfItems[i]);
        }
    }

    private void CropButtonOnClick()
    {
        for(int i=0;i< transform.Find("Background/groups").childCount; i++)
        {
            Destroy(transform.Find("Background/groups").GetChild(i).gameObject);
        }
   
        for (int i = 0; i < cropConf.CreateConfItems.Length; i++)
        {
            //Debug.Log(cropConf.CreateConfItems[i].Name);
            UI_CreateItem item = Instantiate(UI_Prefab, parent).GetComponent<UI_CreateItem>();
            item.Init(cropConf.CreateConfItems[i]);
        }
    }
    private void FlowerButtonOnClick()
    {
        for (int i = 0; i < transform.Find("Background/groups").childCount; i++)
        {
            Destroy(transform.Find("Background/groups").GetChild(i).gameObject);
        }
        Debug.Log(flowerConf.CreateConfItems.Length);
        for (int i = 0; i < flowerConf.CreateConfItems.Length; i++)
        {
            Debug.Log(flowerConf.CreateConfItems[i].Name);
            UI_CreateItem item = Instantiate(UI_Prefab, parent).GetComponent<UI_CreateItem>();
            item.Init(flowerConf.CreateConfItems[i]);
        }
    }
    private void AnimalButtonOnClick()
    {
        for (int i = 0; i < transform.Find("Background/groups").childCount; i++)
        {
            Destroy(transform.Find("Background/groups").GetChild(i).gameObject);
        }
        for (int i = 0; i < animalConf.CreateConfItems.Length; i++)
        {
            //Debug.Log(createConf.CreateConfItems[i].Name);
            UI_CreateItem item = Instantiate(UI_Prefab, parent).GetComponent<UI_CreateItem>();
            item.Init(animalConf.CreateConfItems[i]);
        }
    }

    private void CloseButtonClick()
    {
        SetActive(false);//关闭
        GameObject.Find("Slime_01").GetComponent<NPCInteract>().enabled = true;
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<ThirdPerson>().enabled = true;
        GameObject.FindGameObjectWithTag("Player").GetComponent<Player_C>().enabled = true;
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().enabled = true;
        Time.timeScale = 1f;
    }
    //修改面板显示
    public void SetActive(bool isShow)
    {
        gameObject.SetActive(isShow);
    }
}
