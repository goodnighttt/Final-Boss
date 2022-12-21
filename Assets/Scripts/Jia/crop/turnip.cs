using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class turnip : BaseCrop
{
    public override int lifeTime => 100;
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
        tempCrop = Instantiate<GameObject>(prefabs[1], transform.position += new Vector3(0, 1, 0.2f), Quaternion.Euler(0, 0, 0), null);

        //收获

    }
}
