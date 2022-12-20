using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UI_CreateItem : MonoBehaviour
{
    private Text NameText;
    private Text MoneyText;
    private Button CreateButton;

    private CreateConfItem confItem;




    public void Init(CreateConfItem confItem)
    {
        //获取
        NameText = transform.Find("Name").GetComponent<Text>();
        MoneyText = transform.Find("Money").GetComponent<Text>();
        CreateButton = transform.Find("BG").GetComponent<Button>();
        CreateButton.onClick.AddListener(CreateButtonClick);//点击事件

        this.confItem = confItem;
        //currCount = 0;

        //Debug.Log(confItem.Name);
        NameText.text = confItem.Name;
        MoneyText.text = confItem.Money.ToString();

    }

    private void CreateButtonClick()
    {
        Debug.Log(confItem.Name);
        Player_C.Instance.WantCrop = true;
        Player_C.Instance.cropPrefab=confItem.Prefab;
        Player_C.Instance.cropCost = confItem.Money;
        UI_CreatePanel.Instance.SetActive(false);
        //Debug.Log("已选择作物");
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<ThirdPerson>().enabled = true;
        GameObject.FindGameObjectWithTag("Player").GetComponent<Player_C>().enabled = true;
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().enabled = true;
        Time.timeScale = 1f;

    }
}
