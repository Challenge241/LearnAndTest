using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperInvisibleZombie : Zombie
{
    //private GameObject sh;
    // Start is called before the first frame update
    public bool canDie;
    void Start()
    {
        damageTimer = 0;
        currentHealth = health;
        isWalk = true;
        isDie = false;
        isBoom = false;
        canDie = true;
        //sh = transform.Find("shadow").gameObject;
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
    }
    public void ChangeHealth(float num)
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
        //Debug.Log("ChangeHealth");
        //Debug.Log(currentHealth);
        if (currentHealth <= 0 && isDie == false)
        {
            if (isBoom == true) { return; }
            isDie = true;
        }
        if (isDie == true&&canDie==true)
        {
            UIManager.instance.ChangeCoinNum(coin);
            if (transform.Find("DieAni").gameObject.activeSelf == false)
            { transform.Find("DieAni").gameObject.SetActive(true); }
            //Destroy(gameObject);
        }
        if (canDie == false)
        {
            currentHealth = health;
            isDie = false;
        }
    }
    public void ChangeHealthBoom(float num)
    {
        currentHealth = Mathf.Clamp(currentHealth + num, 0, health);
        if (currentHealth <= 0 && isDie == false)
        {
            //animator.SetTrigger("BoomDie");
            isBoom = true;
            isDie = true;
        }
        if (isDie == true && canDie == true)
        {
            UIManager.instance.ChangeCoinNum(coin);
            if (transform.Find("BoomDieAni").gameObject.activeSelf == false)
            { transform.Find("BoomDieAni").gameObject.SetActive(true); }
            //Destroy(gameObject);
        }
        if (canDie == false)
        {
            currentHealth = health;
            isDie = false;
        }
    }
    public void eatByPlant()
    {
        UIManager.instance.ChangeCoinNum(coin);
        Destroy(gameObject);
    }
    public void CanBeVisible()
    {
        //sh.SetActive(true);
        //另一种写法
        //private GameObject sh;//局部变量
        //sh = transform.Find("shadow").gameObject;
        //sh.SetActive(true);
        transform.Find("shadow").gameObject.SetActive(true);
    }
    public void NotBeVisible()
    {
        //sh.SetActive(false);
        transform.Find("shadow").gameObject.SetActive(false);
    }
}
