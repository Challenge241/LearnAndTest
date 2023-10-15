using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallNut : Plant
{
    public Animator animator;
    public float timer;
    public bool isSpike = false;
    public GameObject IronSpikeNutPrefab;
    GameObject ironSpikeNut;
    GameObject temp;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth=health;
        animator = GetComponent<Animator>();//��д����ᱨ��ָ���쳣
        InvokeRepeating("healthTest",2,2);
        InvokeRepeating("TestLeveUp", 0.5f, 0.5f);
        InvokeRepeating("TestSkill", 0.5f, 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        skillwaitjishiqi();
    }
    public void healthTest()
    {
        //Debug.Log("healthTest");
        //Debug.Log(currentHealth);
        //Debug.Log(health);
        if (currentHealth > 0.6 * health)
        {
            animator.SetBool("Cracked", false);
            animator.SetBool("Unhealthy", false);
        }
        if (0.3* health < currentHealth&& currentHealth <= 0.6*health)
        {
            //Debug.Log("<=0.6*health");
            animator.SetBool("Unhealthy", true);
            animator.SetBool("Cracked", false);
        }
        if(currentHealth <= 0.3 * health)
        {
            animator.SetBool("Cracked", true);
            animator.SetBool("Unhealthy",true);
        }
        else { }
    }
   /* public new float ChangeHealth(float num)
    {
        //Debug.Log("OK");
        currentHealth = Mathf.Clamp(currentHealth + num, 0, health);
        if (currentHealth <= 0)
        {
            GameObject.Destroy(gameObject);
        }
        return currentHealth;
    }*/
    public new void TestLeveUp()
    {
        if (isSpike == false)
        {
            if (haveLevelUp == true)
            {
                if (transform.Find("TallNutJpg").gameObject.activeSelf == false)
                {
                    transform.Find("TallNutJpg").gameObject.SetActive(true);
                }
                if (transform.Find("SpikeNutJpg").gameObject.activeSelf == false)
                {
                    transform.Find("SpikeNutJpg").gameObject.SetActive(true);
                }
                if (transform.Find("BoomNutJpg").gameObject.activeSelf == false)
                {
                    transform.Find("BoomNutJpg").gameObject.SetActive(true);
                }
            }
        }
        else
        {
            if (haveLevelUp == true)
            {
                //�õ�����Ԥ�Ƽ�
                ironSpikeNut = Instantiate(IronSpikeNutPrefab);
                //�õ���ǰ����ĸ���������
                temp = transform.parent.gameObject;
                //�����ظ���������ֲ��ĸ�����
                ironSpikeNut.transform.parent = temp.transform;
                ironSpikeNut.transform.localPosition = Vector3.zero;
                //����ԭ���壬���������ص�
                Destroy(gameObject);
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
        currentHealth = health;
        animator.SetBool("Cracked", false);
        animator.SetBool("Unhealthy", false);
        defBuff();
    }
    public void defBuff()
    {
        def += 16;
        Invoke("defBack", 15);
    }
    public void defBack() {
        def -= 16;
    }
}
