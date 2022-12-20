using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class CreateConfItem
{

    public string Name;//名称
    public int Money;//金币
    public GameObject Prefab;//模型

}


[CreateAssetMenu(fileName = "创建物配置文件", menuName = "配置/创建物配置")]
public class CreateConf : ScriptableObject//配置文件
{
    public CreateConfItem[] CreateConfItems;
}



