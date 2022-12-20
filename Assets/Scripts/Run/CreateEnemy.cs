using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateEnemy : MonoBehaviour
{
    // Start is called before the first frame update
    //�ó��������ɵĹ���
    public GameObject targetEnemy;
    //���ɹ����������
    public int enemyTotalNum = 3;
    //���ɹ����ʱ����
    public float intervalTime = 10;
    //���ɹ���ļ�����
    private int enemyCounter;


    //���ɹ���ļ�����

    // Use this for initialization
    void Start()
    {


        //��ʼʱ���������Ϊ0��
        enemyCounter = 0;
        //�ظ����ɹ���
        InvokeRepeating("CreatEnemy", 0.5F, intervalTime);
    }

    // Update is called once per frame
    void Update()
    {

    }
    //���������ɹ���
    private void CreatEnemy()
    {
        //�����Ҵ��

        {
            //���������
            Vector3 suiji = this.transform.position;
            suiji.x = this.transform.position.x + Random.Range(3.0f, 10.0f);
            suiji.y = this.transform.position.y + Random.Range(3.0f, 10.0f);
            suiji.z = 0;

            Vector3 suiji1 = this.transform.position;
            suiji1.x = this.transform.position.x + Random.Range(3.0f, 10.0f);
            suiji1.y = this.transform.position.y + Random.Range(3.0f, 10.0f);
            suiji1.z = 0;


            //����һֻ����
            if (Random.Range(0, 3) % 3 == 0)
            {
                Instantiate(targetEnemy, this.transform.position, Quaternion.identity);
                //targetEnemy.tag = "Enemy";
                //targetEnemy.GetComponent<Slime>().enabled = true;
                enemyCounter++;
            }
            if (Random.Range(0, 3) % 3 == 1)
            {
                Instantiate(targetEnemy, suiji, Quaternion.identity);
                //targetEnemy.tag = "Enemy";
                //targetEnemy.GetComponent<Slime>().enabled = true;
                enemyCounter++;
            }
            if (Random.Range(0, 3) % 3 == 2)
            {
                Instantiate(targetEnemy, suiji1, Quaternion.identity);
                //targetEnemy.tag = "Enemy";
                //targetEnemy.GetComponent<Slime>().enabled = true;
                enemyCounter++;
            }
            //��������ﵽ���ֵ
            if (enemyCounter == enemyTotalNum)
            {
                //ֹͣˢ��
                CancelInvoke();
            }
        }

    }

}
