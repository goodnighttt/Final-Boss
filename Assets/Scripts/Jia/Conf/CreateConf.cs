using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class CreateConfItem
{

    public string Name;//����
    public int Money;//���
    public GameObject Prefab;//ģ��

}


[CreateAssetMenu(fileName = "�����������ļ�", menuName = "����/����������")]
public class CreateConf : ScriptableObject//�����ļ�
{
    public CreateConfItem[] CreateConfItems;
}



