using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SunFlower : Plant
{
    public GameObject sunPrefab;
    public float interval;//ʱ����
    public Transform sunPos;//����λ��
    public Transform sunPosL;//��ߵ���������λ��
    public Transform sunPosR;//�ұߵ���������λ��
    float sunNum = 0;//�����ж������������ɵ����⻹��ż�������ɵ�����
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
    public void BornSun1()//�ҵķ�������������������������,�ŵ��Ǽ��
    {
       // Debug.LogWarning("�����");
        if (Random.Range(0, 2) == 1)
        {
            sunPos = sunPosR;
        }
        else { sunPos = sunPosL; }
       // Debug.LogWarning("��������");
        Instantiate(sunPrefab, sunPos.position, Quaternion.identity);
    }
    public void BornSun2()//���˵ķ������������������������ߣ��ŵ������������λ���������
    {
        GameObject sunNew = Instantiate(sunPrefab);
        sunNum++;
        float randomX;
        //����̫�����������
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
        //ʹ�ü��ܺ�������������
        //Ϊʲô�����������أ���Ϊ�����������һ������ſ��ܵ��µ��û��Ӧ
        GameManager.instance.ChangeSunNum(50);
    }
}
