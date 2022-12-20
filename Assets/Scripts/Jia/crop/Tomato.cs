using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �������ڣ�ʱ��+����ģ��
/// </summary>
public class Tomato : BaseCrop
{
    public override int lifeTime => 3;

    public List<GameObject> prefabs = new List<GameObject>();
    public GameObject tempCrop;
    private Sprite getImg;

    public int Lv = 0;
    // Start is called before the first frame update
    void Start()
    {
        getImg = Resources.Load<Sprite>("get");
        tempCrop = Instantiate<GameObject>(prefabs[0], transform.position, Quaternion.identity, null);
        Invoke("upgrade", lifeTime);
    }

    private void upgrade()
    {
        //ɾȥԭ����ģ�ͣ���ʵ���������ģ��
        Lv = 1;
        Destroy(tempCrop.gameObject);
        tempCrop = Instantiate<GameObject>(prefabs[1], transform.position += new Vector3(0, 0.7f, 0), Quaternion.Euler(-90, 0, 0), null);
        //Instantiate<Sprite>(getImg, transform.position += new Vector3(0, 2, 0), Quaternion.Euler(0, 0, 0), null);

    }




}
