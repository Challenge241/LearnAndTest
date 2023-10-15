using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieIron : ZombieNormal
{

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
        InvokeRepeating("healthTest", 1,1);
    }

    // Update is called once per frame
    void Update()
    {
        if (isDie) { return; }
        Move();
    }
    private void healthTest()
    {
        if (currentHealth >= health * 0.8)
        {
            transform.Find("bucket1").gameObject.SetActive(true);
        }else if (currentHealth >= health * 0.6)
        {
            transform.Find("bucket1").gameObject.SetActive(false);
            transform.Find("bucket2").gameObject.SetActive(true);
        }
        else if (currentHealth >= health * 0.4)
        {
            transform.Find("bucket2").gameObject.SetActive(false);
            transform.Find("bucket3").gameObject.SetActive(true);
        }
        else
        {
            transform.Find("bucket3").gameObject.SetActive(false);
        }
    }
}
