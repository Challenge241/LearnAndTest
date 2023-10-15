using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThreePeater : Plant
{
    public float interval;//时间间隔
    public float timer;//计时器，计时技能
    public float timer2;//计时器,计时子弹
    public GameObject bullet;//子弹
    public Transform bulletPos;//子弹位置
    public Transform bulletPosA;//子弹位置
    public Transform bulletPosB;//子弹位置
    public Transform bulletPosjihuo1;//子弹位置
    public Transform bulletPosjihuo2;//子弹位置

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = health;
        InvokeRepeating("TestLeveUp", 0.5f, 0.5f);
        //jishiqi();//方法一，用计时器,方法二用invokeRepeating
        //InvokeRepeating("creatbullet3", interval, interval);//方法二，用协程
        InvokeRepeating("TestSkill", 0.5f, 0.5f);
    }
    void Update()
    {
        jishiqi();
        skillwaitjishiqi();
    }
    void jishiqi()
    {
        timer2 += Time.deltaTime;
        if (timer2 >= interval)
        {
            if (MoShi % 2 == 0)
            { Invoke("creatbullet3", 0); }
            else
            {
                Invoke("creatbullet3jihuo", 0);
            }
            timer2 = 0;
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
    void creatbullet3()
    {
        Instantiate(bullet, bulletPos.position, Quaternion.identity);
        Instantiate(bullet, bulletPosA.position, Quaternion.identity);
        Instantiate(bullet, bulletPosB.position, Quaternion.identity);
    }
    void creatbullet3jihuo()
    {
        Instantiate(bullet, bulletPos.position, Quaternion.identity);
        Instantiate(bullet, bulletPosjihuo1.position, Quaternion.identity);
        Instantiate(bullet, bulletPosjihuo2.position, Quaternion.identity);
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
        if (MoShi % 2 == 0)
        {
            for (int i = 1; i <= 60; i++)
                Invoke("creatbullet3", Random.Range(0.1f, 4.4f));
        }
        else
        {
            for (int i = 1; i <= 60; i++)
                Invoke("creatbullet3jihuo", Random.Range(0.1f, 4.4f));
        }
        
    }
}
