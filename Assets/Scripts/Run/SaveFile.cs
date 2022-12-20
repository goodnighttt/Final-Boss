using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using UnityEngine;
using UnityEngine.SceneManagement;
/// <summary>
/// ��̬��ȡÿ������,
/// </summary>
public class XML : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

        //���Ժ�iphong�ϵ�·���ǲ�һ���ģ������ñ�ǩ�ж�һ�¡�
#if UNITY_EDITOR
        string filepath = Application.dataPath + "/StreamingAssets" + "/my.xml";
#elif UNITY_IPHONE
      string filepath = Application.dataPath +"/Raw"+"/my.xml";
#endif
        //����ļ����ڻ���ʼ������
        if (File.Exists(filepath))
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(filepath);
            XmlNodeList nodeList = xmlDoc.SelectSingleNode("gameObjects").ChildNodes;
            foreach (XmlElement scene in nodeList)
            {
                //��Ϊ�ҵ�XML�ǰ�������Ϸ����ȫ�������� ���������ж�һ��ֻ������Ҫ�ĳ����е���Ϸ����
                //JSON������ԭ������
                if (!scene.GetAttribute("name").Equals("Assets/StarTrooper.unity"))
                {
                    continue;
                }

                foreach (XmlElement gameObjects in scene.ChildNodes)
                {

                    string asset = "Prefab/" + gameObjects.GetAttribute("name");
                    Vector3 pos = Vector3.zero;
                    Vector3 rot = Vector3.zero;
                    Vector3 sca = Vector3.zero;
                    foreach (XmlElement transform in gameObjects.ChildNodes)
                    {
                        foreach (XmlElement prs in transform.ChildNodes)
                        {
                            if (prs.Name == "position")
                            {
                                foreach (XmlElement position in prs.ChildNodes)
                                {
                                    switch (position.Name)
                                    {
                                        case "x":
                                            pos.x = float.Parse(position.InnerText);
                                            break;
                                        case "y":
                                            pos.y = float.Parse(position.InnerText);
                                            break;
                                        case "z":
                                            pos.z = float.Parse(position.InnerText);
                                            break;
                                    }
                                }
                            }
                            else if (prs.Name == "rotation")
                            {
                                foreach (XmlElement rotation in prs.ChildNodes)
                                {
                                    switch (rotation.Name)
                                    {
                                        case "x":
                                            rot.x = float.Parse(rotation.InnerText);
                                            break;
                                        case "y":
                                            rot.y = float.Parse(rotation.InnerText);
                                            break;
                                        case "z":
                                            rot.z = float.Parse(rotation.InnerText);
                                            break;
                                    }
                                }
                            }
                            else if (prs.Name == "scale")
                            {
                                foreach (XmlElement scale in prs.ChildNodes)
                                {
                                    switch (scale.Name)
                                    {
                                        case "x":
                                            sca.x = float.Parse(scale.InnerText);
                                            break;
                                        case "y":
                                            sca.y = float.Parse(scale.InnerText);
                                            break;
                                        case "z":
                                            sca.z = float.Parse(scale.InnerText);
                                            break;
                                    }
                                }
                            }
                        }

                        //�õ� ��ת ���� ƽ�� �Ժ��¡����Ϸ����
                        GameObject ob = (GameObject)Instantiate(Resources.Load(asset), pos, Quaternion.Euler(rot));
                        ob.transform.localScale = sca;

                    }
                }
            }
        }
    }


    void OnGUI()
    {
        if (GUI.Button(new Rect(0, 0, 200, 200), "XML WORLD"))
        {
            // Application.LoadLevel("JSONScene");
            SceneManager.LoadScene("Level1");
        }
        //if (GUI.Button(new Rect(0, 200, 500, 200), "XML skfnakgn"))   //�⼸����ťֻ�ǲ���,һ��������,����Ҫ����ɾ��,�����Լ�����
        //{
        //    // Application.LoadLevel("JSONScene");
        //    SceneManager.LoadScene("SampleScene");
        //}
        //if (GUI.Button(new Rect(0, 400, 500, 200), "XML dfdf"))
        //{
        //    Application.LoadLevel("1234");
        //    // SceneManager.LoadScene("1234");
        //}
    }
}
