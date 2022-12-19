using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnCreate : MonoBehaviour
{
    // Start is called before the first frame update
    //public Transform Cube;
    //public float Hight_Y = 5.0f;//高度
    //public float Forward_Z = 0f;//深度
    public GameObject prefab;
    public GameObject obj;
    public GameObject Temp;
    public int flag = 0;
    public Color oldColor;
    public float Alpha = 0.1f;
    public 
    void Start()
    {
        //Vector3 asteroidPos = new Vector3(-2.3f,-2.1f,0f);//预制体生成坐标（跟随X轴移动）
        //obj = Instantiate(prefab, asteroidPos, transform.rotation);//实例化预制体
        oldColor = GetComponent<Renderer>().material.color;
    }

    private void OnMouseOver()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, 100))
        {
            GameObject gameObject = hit.collider.gameObject; 
            

            Debug.Log(gameObject.name);
            Temp = gameObject;
            
        }

        Temp.GetComponent<Renderer>().material.color = new Color(255, 255, 255, Alpha);

        // transform.Rotate(0, 180 * Time.deltaTime, 0);
    }

    //鼠标移开时调用该函数；
    private void OnMouseExit()
    {
        GetComponent<Renderer>().material.color = oldColor;
    }

    // Update is called once per frame
    void Update()
    {
        
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, 100))
        {
            GameObject gameObject = hit.collider.gameObject;
            if(gameObject.name.Contains("obj"))
            {
                Temp = gameObject;
            }
           // Debug.Log(gameObject.name);
            
            //Debug.Log(Temp.name);
           
        }
        if (Input.GetMouseButton(0)&& Temp.name.Contains("obj"))
        {
            
            Vector3 ScreePos = Camera.main.WorldToScreenPoint(Temp.transform.position);
            Vector3 temp = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10f));
            Temp.transform.position = temp;
            Temp.GetComponent<Rigidbody>().detectCollisions = false;
            Temp.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);

        }
        if(Input.GetMouseButtonUp(0))
        {
            Temp.GetComponent<Rigidbody>().useGravity = true;
            Temp.GetComponent<Rigidbody>().detectCollisions = true;
            //GameObject Newobj = Instantiate(prefab, transform.position, transform.rotation);//实例化预制体
        }
    }

   
}
