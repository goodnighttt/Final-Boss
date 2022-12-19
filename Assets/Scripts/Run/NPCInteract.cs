using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCInteract : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject TalkPlane;
    [SerializeField] private GameObject containerGameObject;
    [SerializeField] private PlayerMovement playerMovement;

    private bool isinteractable = false;
    void Start()
    {
        TalkPlane.SetActive(false);
    }

    public void Interact()
    {
        TalkPlane.SetActive(true);
        Hide();
        //Debug.Log("交互");
    }
    // Update is called once per frame
    private void Show()
    {
        containerGameObject.SetActive(true);
    }

    private void Hide()
    {
        containerGameObject.SetActive(false);

    }
    //void Start()
    //{

    //}

    // Update is called once per frame
    void Update()
    {
        if (playerMovement.GetInteractbleObject() != null)
        {
            //Debug.Log("交互");
            Show();
           

            //与NPC进行交互
            if (Input.GetKeyDown(KeyCode.E))
            {
                Interact();
                isinteractable = true;
            }

            if (Input.GetKeyDown(KeyCode.Y)&& isinteractable == true)
            {
                Debug.Log("商店");
                TalkPlane.SetActive(false);
                //Hide();
                UIManager.Instance.ShowCreatePanel();
                GameObject.Find("Slime_01").GetComponent<NPCInteract>().enabled = false;
                GameObject.FindGameObjectWithTag("MainCamera").GetComponent<ThirdPerson>().enabled = false;
                GameObject.FindGameObjectWithTag("Player").GetComponent<Player_C>().enabled = false;
                GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().enabled = false;


                Time.timeScale = 0f;
                Hide();
                GameObject.Find("Slime_01").GetComponent<NPCInteract>().enabled = false;
            }

            if (Input.GetKeyDown(KeyCode.N)&&isinteractable == true)
            {
                TalkPlane.SetActive(false);
            }
        }
        else
        {
            Hide();
            TalkPlane.SetActive(false);

            //isinteractable = false;
        }

       

        
    }
}
