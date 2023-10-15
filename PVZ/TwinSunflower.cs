using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwinSunflower : Plant
{
    public GameObject bullet;
    public GameObject sunPrefab;
    public float interval;//时间间隔
    public Transform sunPosL;//左边的阳光生成位置
    public Transform sunPosR;//右边的阳光生成位置
    // Start is called before the first frame update
    private Animator animator;
    public float addDef;
    public float timer;
    public float skilltimer;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = health;
        animator = GetComponent<Animator>();
        //InvokeRepeating("BornSun", interval - 1, interval);
        InvokeRepeating("TestSkill", 0.5f, 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        jishiqi();
        skillwaitjishiqi();
    }
    public void createSunBullet()
    {
        Instantiate(bullet, sunPosL.position, Quaternion.identity);
        Instantiate(bullet, sunPosR.position, Quaternion.identity);
    }

    public void jishiqi()
    {
        timer += Time.deltaTime;
        if (timer >= interval)
        {
            if (MoShi % 3 == 0)
            {
                if (transform.Find("sundef").gameObject.activeSelf == true)
                {
                    def -= addDef;
                    transform.Find("sundef").gameObject.SetActive(false);
                }
                Invoke("BornSun", 0);
            }
            else if (MoShi % 3 == 1)
            {
                if (transform.Find("sundef").gameObject.activeSelf == true)
                {
                    def -= addDef;
                    transform.Find("sundef").gameObject.SetActive(false);
                }
                Invoke("createSunBullet", 0);
            }
            else
            {
                if (transform.Find("sundef").gameObject.activeSelf == false)
                {
                    def += addDef;
                    transform.Find("sundef").gameObject.SetActive(true);
                }
            }
            timer = 0;
        }
    }
    public void BornSun()
    {
        Instantiate(sunPrefab, sunPosL.position, Quaternion.identity);
        Instantiate(sunPrefab, sunPosR.position, Quaternion.identity);
    }
    void skillwaitjishiqi()
    {
        if (waitskill == true)
        {
            skilltimer += Time.deltaTime;
            if (skilltimer >= skillInterval)
            {
                waitskill = false;
                skilltimer = 0;
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
        GameManager.instance.ChangeSunNum(100);
    }
}
