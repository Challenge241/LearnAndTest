using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieHateLight : SuperInvisibleZombie
{
    //该隐形僵尸并且在隐形模式下无法死亡
    //因为普通的隐形僵尸没什么用，隐形时照样被豌豆打死
    //导致灯笼花的反隐功能没用
    // Start is called before the first frame update
    void Start()
    {
        damageTimer = 0;
        currentHealth = health;
        isWalk = true;
        isDie = false;
        isBoom = false;
        canDie = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isDie) { return; }
        Move();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isDie)
            return;
        if (other.tag == "Plant")
        {
            isWalk = false;
        }
        if (other.tag == "Light")
        {
            canDie = true;
            transform.Find("BlackShadow1").gameObject.SetActive(true);
            transform.Find("BlackShadow2").gameObject.SetActive(true);
            transform.Find("BlackShadow3").gameObject.SetActive(true);
        }
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Plant")
        {
            damageTimer += Time.deltaTime;
            if (damageTimer >= damageInterval)
            {
                damageTimer = 0;
                Plant plant = other.GetComponent<Plant>();
                float newHealth = plant.ChangeHealth(-damage);
                if (newHealth <= 0)
                {
                    isWalk = true;
                }
            }
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Plant")
        {
            isWalk = true;
        }
        if (other.tag == "Light")
        {
            canDie = false;
            transform.Find("BlackShadow1").gameObject.SetActive(false);
            transform.Find("BlackShadow2").gameObject.SetActive(false);
            transform.Find("BlackShadow3").gameObject.SetActive(false);
        }
    }
}
