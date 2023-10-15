using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieNormal : Zombie
{
    public float lostHeadHealth=50;//僵尸多少血时头会掉
    private GameObject head;
    public bool lostHead;
    public bool canbeEat=true;
    public float timer = 0;
    // Start is called before the first frame update
    void Start()
    {
        isWalk = true;
        animator = GetComponent<Animator>();
        damageTimer = 0;
        currentHealth = health;
        isDie = false;
        isBoom = false;
        lostHead = false;

    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (isDie) { return; }
        Move();
    }
    //碰撞开始
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (isDie)
            return;
        if (other.tag == "Plant") 
        {
            isWalk = false;
            animator.SetBool("Walk", false);
        }
    }
    //碰撞中
    public void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Plant") 
        {
            damageTimer += Time.deltaTime;
            if (damageTimer >= damageInterval)
            {
                damageTimer = 0;
                Plant plant = other.GetComponent<Plant>();
                float newHealth=plant.ChangeHealth(-damage);
                if (newHealth <= 0)
                {
                    isWalk = true;
                    animator.SetBool("Walk", true);
                }
            }
        }
    }
    //碰撞结束
    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Plant") 
        {
            isWalk = true;
            animator.SetBool("Walk", true);
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
        if (currentHealth <= lostHeadHealth && lostHead==false)
        {
            animator.SetBool("LostHead", true);
           // head.SetActive(true);
        }
        if (currentHealth <= 0 && isDie == false)
        {
            if (isBoom==true) { return; }
            animator.SetTrigger("Die");
            isDie = true;
        }
    }
    public void ChangeHealthBoom(float num)
    {
        currentHealth = Mathf.Clamp(currentHealth + num, 0, health);
        if (currentHealth <= lostHeadHealth && lostHead == false)
        {
            animator.SetBool("LostHead", true);
            // head.SetActive(true);
        }
        if (currentHealth <= 0&&isDie==false)
        {
            animator.SetTrigger("BoomDie");
            isBoom = true;
            isDie = true;
        }
    }
    public bool eatByPlant(float damage)
    {
        if (canbeEat == true)
        {
            UIManager.instance.ChangeCoinNum(coin);
            Destroy(gameObject);
            return true;
        }
        return false;
    }
    public void HeadAniBegin()
    {
        if (lostHead == false)
        {
            transform.Find("Head").gameObject.SetActive(true);
            lostHead = true;
        }
    }
    public void DieAniOver()
    {
        animator.enabled = false;
        UIManager.instance.ChangeCoinNum(coin);
        GameObject.Destroy(gameObject);
    }
}
