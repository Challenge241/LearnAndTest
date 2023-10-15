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
    public GameObject LevelUpPlantPrefab;//�������ֲ��
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
            //�õ�����Ԥ�Ƽ�
            LevelUpPlant = Instantiate(LevelUpPlantPrefab);
            //�õ���ǰ����ĸ���������
            p = transform.parent.gameObject;
            //�����ظ���������ֲ��ĸ�����
            LevelUpPlant.transform.parent = p.transform;
            LevelUpPlant.transform.localPosition = Vector3.zero;
            //����ԭ���壬���������ص�
            Destroy(gameObject);
        }
    }
}
