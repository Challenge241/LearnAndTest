using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Peashooter : Plant
{
    public float interval;//ʱ����
    public float timer;//��ʱ��,ע��һ����ʱ����ò�Ҫͬʱ���������£����磺�����ӵ��ͼ�����ȴ
    public GameObject bullet;//�ӵ�
    public Transform bulletPos;//�ӵ�λ��


    // Start is called before the first frame update
    void Start()
    {
        canLevelUp = true;
        currentHealth=health;
        //jishiqi();//����һ���ü�ʱ��
        InvokeRepeating("creatbullet", interval, interval);//����������Э��
        InvokeRepeating("TestLeveUp", 0.5f, 0.5f);
        InvokeRepeating("TestSkill", 0.5f, 0.5f);
    }
    void Update()
    {
        skillwaitjishiqi();
    }
   /* void jishiqi()
    {
        timer += Time.deltaTime;
        if (timer >= interval)
        {
            timer = 0;
            Instantiate(bullet, bulletPos.position, Quaternion.identity);
        }
    }*/
   
    void creatbullet()
    {
        Instantiate(bullet, bulletPos.position, Quaternion.identity);
    }
    public new void TestLeveUp()
    {
        if (haveLevelUp == true)
        {
           if(transform.Find("RepeaterJpg").gameObject.activeSelf ==false)
            {
                transform.Find("RepeaterJpg").gameObject.SetActive(true);
            }
            if (transform.Find("InvisiblePeaJpg").gameObject.activeSelf == false)
            {
                transform.Find("InvisiblePeaJpg").gameObject.SetActive(true);
            }
        }
    }
    void skillwaitjishiqi()
    {
        if (waitskill == true)
        {
            timer += Time.deltaTime;
            if (timer >= skillInterval)
            {
                waitskill = false;
                timer = 0;
                transform.Find("skillReady").gameObject.SetActive(true);
            }
        }
    }
    public void TestSkill()
    {
        if (haveskill == true)
        {
            skill();
            haveskill = false;
            waitskill = true;
            transform.Find("skillReady").gameObject.SetActive(false);
        }
    }
    public void skill()
    {
        //Debug.LogWarning("2");
        for (int i=1;i<=60;i++)
        Invoke("creatbullet", Random.Range(0.1f, 4.4f));
    }
}

