using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GatlingPea : Plant
{
    public float interval;//ʱ����
    public float timer;//��ʱ��
    public GameObject bullet;//�ӵ�
    public Transform bulletPos;//�ӵ�λ��
    public Transform bulletPos1;
    public Transform bulletPos2;
    public Transform bulletPos3;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = health;
        //jishiqi();//����һ���ü�ʱ��
        InvokeRepeating("creatbullet4", interval, interval);//����������invokeRepeating����
        InvokeRepeating("TestSkill", 0.5f, 0.5f);
    }
    void Update()
    {
        skillwaitjishiqi();
    }
    /*void jishiqi()
    {
        timer += Time.deltaTime;
        if (timer >= interval)
        {
            timer = 0;
            Instantiate(bullet, bulletPos.position, Quaternion.identity);
        }
    }*/
    void creatbullet4()
    {
        Instantiate(bullet, bulletPos.position, Quaternion.identity);
        Instantiate(bullet, bulletPos1.position, Quaternion.identity);
        Instantiate(bullet, bulletPos2.position, Quaternion.identity);
        Instantiate(bullet, bulletPos3.position, Quaternion.identity);
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
        for (int i = 1; i <= 60; i++)
            Invoke("creatbullet4", Random.Range(0.1f, 9.9f));
    }
}

