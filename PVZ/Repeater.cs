using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Repeater : Plant
{
    public float interval;//时间间隔
    public float timer;//计时器
    public GameObject bullet;//子弹
    public Transform bulletPos;//子弹位置
    public Transform bulletPos1;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = health;
        //jishiqi();//方法一，用计时器
        InvokeRepeating("creatbullet2", interval, interval);//方法二，用invokeRepeating方法
        InvokeRepeating("TestLeveUp", 0.5f, 0.5f);
        InvokeRepeating("TestSkill", 0.5f, 0.5f);
    }
    void Update()
    {
        skillwaitjishiqi();
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
    /*void jishiqi()
    {
        timer += Time.deltaTime;
        if (timer >= interval)
        {
            timer = 0;
            Instantiate(bullet, bulletPos.position, Quaternion.identity);
        }
    }*/
    void creatbullet2()
    {
        Instantiate(bullet, bulletPos.position, Quaternion.identity);
        Instantiate(bullet, bulletPos1.position, Quaternion.identity);
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
            Invoke("creatbullet2", Random.Range(0.1f, 6.6f));
    }
    // Update is called once per frame
}

