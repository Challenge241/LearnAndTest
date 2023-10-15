using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SunFlower : Plant
{
    public GameObject sunPrefab;
    public float interval;//时间间隔
    public Transform sunPos;//阳光位置
    public Transform sunPosL;//左边的阳光生成位置
    public Transform sunPosR;//右边的阳光生成位置
    float sunNum = 0;//用于判断是奇数次生成的阳光还是偶数次生成的阳光
    public float timer;
    public float timerskill;
    public GameObject bullet;
    public Transform bulletPos;
    public float addDef;
    // Start is called before the first frame update
    private Animator animator;
    void haveready()
    {
        animator.SetBool("Ready", true);
    }
    public void BornSun()
    {
        Instantiate(sunPrefab, sunPos.position, Quaternion.identity);
        
    }
    public void BornSun1()//我的方法，让阳光生成在左右两边,优点是简洁
    {
       // Debug.LogWarning("随机数");
        if (Random.Range(0, 2) == 1)
        {
            sunPos = sunPosR;
        }
        else { sunPos = sunPosL; }
       // Debug.LogWarning("阳光生产");
        Instantiate(sunPrefab, sunPos.position, Quaternion.identity);
    }
    public void BornSun2()//别人的方法，让阳光生成在左右两边，优点是生成阳光的位置有随机性
    {
        GameObject sunNew = Instantiate(sunPrefab);
        sunNum++;
        float randomX;
        //奇数太阳在左边生成
        if (sunNum % 2 == 1)
        {
            randomX = Random.Range(transform.position.x - 30, transform.position.x - 20);
        }
        else
        {
            randomX = Random.Range(transform.position.x + 30, transform.position.x + 20);
        }
        float randomY = Random.Range(transform.position.y - 20, transform.position.y + 20);
        sunNew.transform.position = new Vector2(randomX, randomY);
    }
    public void BornSunOver()
    {
       // Debug.LogWarning("BornSunOver()");
        BornSun1();
        animator.SetBool("Ready", false);
    }
    void Start()
    {
        currentHealth = health;
        animator = GetComponent<Animator>();
        //InvokeRepeating("haveready", interval - 1, interval);
        InvokeRepeating("TestLeveUp", 0.5f, 0.5f);
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
        Instantiate(bullet, bulletPos.position, Quaternion.identity);
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
                //Debug.LogWarning("haveready");
                Invoke("haveready", 0); 
            }
            else if(MoShi%3==1)
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
    void skillwaitjishiqi()
    {
        if (waitskill == true)
        {
            timer += Time.deltaTime;
            if (timerskill >= skillInterval)
            {
                waitskill = false;
                timerskill = 0;
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
        //使用技能后立即增加阳光
        //为什么不生产阳光呢？因为大量阳光叠在一起互相干扰可能导致点击没反应
        GameManager.instance.ChangeSunNum(50);
    }
}
