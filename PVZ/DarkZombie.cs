using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarkZombie : ZombieNormal
{    //private GameObject head;
    public GameObject prefab;
    public Transform prefabPos;
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
        if (isDie) { return; }
        Move();
    }
    //碰撞开始
    public new void OnTriggerEnter2D(Collider2D other)
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
    private new void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Plant")
        {
            damageTimer += Time.deltaTime;
            if (damageTimer >= damageInterval)
            {
                //Debug.LogWarning("A");
                damageTimer = 0;
                Plant plant = other.GetComponent<Plant>();
                float newHealth = plant.ChangeHealth(-damage);
                if (newHealth <= 0)
                {
                    //Debug.LogWarning("OK");
                    Instantiate(prefab, prefabPos.position, Quaternion.identity);
                    //击杀植物后，用植物的尸体在前方生成一个和自己一样的黑泥怪僵尸
                    isWalk = true;
                    animator.SetBool("Walk", true);
                }
            }
        }
    }
    //碰撞结束
    private new void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Plant")
        {
            isWalk = true;
            animator.SetBool("Walk", true);
        }
    }
}
