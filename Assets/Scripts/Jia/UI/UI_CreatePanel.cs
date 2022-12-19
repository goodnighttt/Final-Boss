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
    }
    //修改面板显示
    public void SetActive(bool isShow)
    {
        gameObject.SetActive(isShow);
    }
}
