using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikeweed : Plant
{
    public float damageJichu;
    public float damageInterval = 0.5f;
    public float damageTimer;
    private GameObject p2;
    private GameObject LevelUpPlant2;
    public float suijishu;
    public float baojiBeilv = 1;
    // Start is called before the first frame update
    void Start()
    {
        damageTimer = 0;
        currentHealth = health;
        InvokeRepeating("TestLeveUp", 0.5f, 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void BaoJi()
    {
        suijishu = Random.Range(1, 1001);
        if (suijishu == 1)
        {
            baojiBeilv = 30;
        }
        else if (1 < suijishu && suijishu <= 6)
        {
            baojiBeilv = 10;
        }
        else if (10 < suijishu && suijishu <= 20)
        {
            baojiBeilv = 5;
        }
        else if (50 < suijishu && suijishu <= 100)
        {
            baojiBeilv = 3;
        }
        else if (200 < suijishu && suijishu <= 300)
        {
            baojiBeilv = 2;
        }
        else if (400 < suijishu && suijishu <= 550)
        {
            baojiBeilv = 1.5f;
        }
        else if (600 < suijishu && suijishu <= 617)
        {
            baojiBeilv = 4;
        }
        else
        {
            baojiBeilv = 1;
        }
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        float damage = 0;
        if (other.tag == "Zombie")
        {
            damageTimer += Time.deltaTime;
            if (damageTimer >= damageInterval)
            {
                BaoJi();
                damage = damageJichu * baojiBeilv;
                //Debug.Log("Spike"+damage);
                damageTimer = 0;
                other.GetComponent<ZombieNormal>().ChangeHealth(-damage);
            }
        }
        if (other.tag == "SpecialZombie")
        {
            damageTimer += Time.deltaTime;
            if (damageTimer >= damageInterval)
            {
                BaoJi();
                damage = damage * baojiBeilv;
                damageTimer = 0;
                other.GetComponent<SuperInvisibleZombie>().ChangeHealth(-damage);
            }
        }
    }
    public new void TestLeveUp()
    {
        if (haveLevelUp == true)
        {
            //拿到物体预制件
            LevelUpPlant2 = Instantiate(LevelUpPlantPrefab);
            //拿到当前物体的父物体土地
            p2 = transform.parent.gameObject;
            //将土地赋给升级版植物的父物体
            LevelUpPlant2.transform.parent = p2.transform;
            LevelUpPlant2.transform.localPosition = Vector3.zero;
            //销毁原物体，避免物体重叠
            Destroy(gameObject);
        }
    }
}
