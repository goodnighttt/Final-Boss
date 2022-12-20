using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 生长周期！时间+成熟模型
/// </summary>
public class Tomato : BaseCrop
{
    public override int lifeTime => 3;

    public List<GameObject> prefabs = new List<GameObject>();
    public GameObject tempCrop;
    private GameObject getImg;

    public int Lv = 0;
    // Start is called before the first frame update
    void Start()
    {
        getImg = Resources.Load<GameObject>("get");
        tempCrop = Instantiate<GameObject>(prefabs[0], transform.position, Quaternion.identity, null);
        Invoke("upgrade", lifeTime);
    }

    private void upgrade()
    {
        //删去原本的模型，并实例化成熟的模型
        Lv = 1;
        Destroy(tempCrop.gameObject);
        tempCrop = Instantiate<GameObject>(prefabs[1], transform.position += new Vector3(0, 0.7f, 0), Quaternion.Euler(-90, 0, 0), null);
        //Instantiate<GameObject>(getImg, transform.position += new Vector3(0, 2, 0), Quaternion.Euler(0, 0, 0), null);

    }

    //private void OnCollisionEnter(Collision collision)
    //{
    //    if(collision.gameObject.tag=="Player" )
    //    {
    //        Debug.Log("player");
    //        if (Input.GetKeyDown(KeyCode.X))
    //        {
    //            Destroy(tempCrop.gameObject);
    //        }
    //    }
    //}




}
