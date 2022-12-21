using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carrot : BaseCrop
{
    public override int lifeTime => 20;
    public List<GameObject> prefabs = new List<GameObject>();
    public GameObject tempCrop;

    public int Lv = 0;
    // Start is called before the first frame update
    void Start()
    {
        tempCrop = Instantiate<GameObject>(prefabs[0], transform.position, Quaternion.identity, null);
        Invoke("upgrade", lifeTime);
    }

    private void upgrade()
    {
        //删去原本的模型，并实例化成熟的模型
        Lv = 1;
        Destroy(tempCrop.gameObject);
        tempCrop = Instantiate<GameObject>(prefabs[1], transform.position += new Vector3(0, 0.7f, 0), Quaternion.Euler(0, 0, 0), null);

        //收获

    }
}
