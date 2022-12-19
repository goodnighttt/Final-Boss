using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_CreatePanel : MonoBehaviour
{

    private Button CloseButton;
    // Start is called before the first frame update
    void Start()
    {
        CloseButton = transform.Find("Background/close").GetComponent<Button>();
        CloseButton.onClick.AddListener(CloseButtonClick);
        SetActive(false);
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
