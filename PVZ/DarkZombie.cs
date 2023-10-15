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
    //��ײ��ʼ
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
    //��ײ��
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
                    //��ɱֲ�����ֲ���ʬ����ǰ������һ�����Լ�һ���ĺ���ֽ�ʬ
                    isWalk = true;
                    animator.SetBool("Walk", true);
                }
            }
        }
    }
    //��ײ����
    private new void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Plant")
        {
            isWalk = true;
            animator.SetBool("Walk", true);
        }
    }
}
