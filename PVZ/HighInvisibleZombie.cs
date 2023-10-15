using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighInvisibleZombie : SuperInvisibleZombie
{
    //private GameObject sh;
    // Start is called before the first frame update
    void Start()
    {
        damageTimer = 0;
        currentHealth = health;
        isWalk = true;
        isDie = false;
        isBoom = false;
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
}
