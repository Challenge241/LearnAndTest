using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvisiblePea : Plant
{
    public float interval;//时间间隔
    public float timer;//计时器
    public GameObject bullet;//子弹
    public GameObject bulletA;
    public Transform bulletPos;//子弹位置
    public Transform bulletPosA;//子弹位置
    public Transform bulletPosB;//子弹位置

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = health;
        //jishiqi();//方法一，用计时器
        InvokeRepeating("creatbullet3", interval, interval);//方法二，用协程
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
        Instantiate(bulletA, bulletPosA.position, Quaternion.identity);
        Instantiate(bulletA, bulletPosB.position, Quaternion.identity);
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
            Invoke("creatbullet3", Random.Range(0.1f, 5.5f));
    }
}
