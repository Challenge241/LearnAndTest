using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : MonoBehaviour
{
    public float MoShi = 0;
    public float health = 100;
    public float currentHealth;
    public float def=0;
    public bool canLevelUp = true;
    public bool haveLevelUp = false;
    public bool canskill = false;
    public bool haveskill = false;
    public bool waitskill = true;
    public float skillInterval = 30;
    private GameObject p;
    private GameObject LevelUpPlant;
    public GameObject LevelUpPlantPrefab;//升级后的植物
    // Start is called before the first frame update
    void Start()
    {
        currentHealth=health;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public float ChangeHealth(float num)
    {
        if (num < 0)
        {
            num = num + def;
            if (num > 0)
            {
                num = 0;
            }
        }
        currentHealth = Mathf.Clamp(currentHealth + num, 0, health);
        if (currentHealth <= 0)
        {
            GameObject.Destroy(gameObject);
        }
        return currentHealth;
    }
    public void TestLeveUp()
    {
        if (haveLevelUp == true)
        {
            //拿到物体预制件
            LevelUpPlant = Instantiate(LevelUpPlantPrefab);
            //拿到当前物体的父物体土地
            p = transform.parent.gameObject;
            //将土地赋给升级版植物的父物体
            LevelUpPlant.transform.parent = p.transform;
            LevelUpPlant.transform.localPosition = Vector3.zero;
            //销毁原物体，避免物体重叠
            Destroy(gameObject);
        }
    }
}
