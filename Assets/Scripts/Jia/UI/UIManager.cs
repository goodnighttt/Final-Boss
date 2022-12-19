using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{

    private UI_MainPanel mainPanel;
    private UI_CreatePanel createPanel;

    public static UIManager Instance;
    private void Awake()
    {
        Instance = this;
    }



    // Start is called before the first frame update
    void Start()
    {
        mainPanel = transform.Find("MainPanel").GetComponent<UI_MainPanel>();
        createPanel = transform.Find("CreatePanel").GetComponent<UI_CreatePanel>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //打开建造面板
    public void ShowCreatePanel()
    {
        //Time.timeScale = 0f;
        createPanel.SetActive(true);
    }
}
