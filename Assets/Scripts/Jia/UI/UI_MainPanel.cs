using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_MainPanel : MonoBehaviour
{
    public Text Money;

    public static UI_MainPanel Instance;
    private void Awake()
    {
        Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        Money = transform.Find("self/Informantion/MoneyNum").GetComponent<Text>();


    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Q))
        {
            Debug.Log("Q");
            UIManager.Instance.ShowCreatePanel();

            //限制鼠标和人物的移动 并使游戏暂停
            GameObject.FindGameObjectWithTag("MainCamera").GetComponent<ThirdPerson>().enabled = false;
            GameObject.FindGameObjectWithTag("Player").GetComponent<Player_C>().enabled = false;
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().enabled = false;
            Time.timeScale = 0f;
        }
    }
}
