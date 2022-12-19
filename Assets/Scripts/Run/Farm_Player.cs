using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Farm_Player : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Prefab_Crop_Sunflower;//���տ�
    public GameObject Prefab_Crop_Empty;//�յ�

    //�����ʱ���еĿյ�
    private GameObject crop_Empty;
    void Start()
    {
        crop_Empty = GameObject.Instantiate<GameObject>(Prefab_Crop_Empty);
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit, int.MaxValue, 1 <<
            LayerMask.NameToLayer("Ground")))
        {
            if(hit.collider!=null && hit.collider.gameObject.tag=="Ground")
            {
                //����괦��һ���յظ�����
                crop_Empty.transform.position = hit.point;
            }
        }
        
    }
}
