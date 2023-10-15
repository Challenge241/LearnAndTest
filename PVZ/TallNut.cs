using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TallNut : Plant
{
    public float timer;
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = health;
        animator = GetComponent<Animator>();//不写这个会报空指针异常
        InvokeRepeating("healthTest", 2, 2);
        InvokeRepeating("TestSkill", 0.5f, 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        skillwaitjishiqi();
    }
    public void healthTest()
    {
        //Debug.Log("healthTest");
        //Debug.Log(currentHealth);
        //Debug.Log(health);
        if (currentHealth <= 0.6 * health)
        {
            Debug.Log("<=0.6*health");
            animator.SetBool("Unhealthy", true);
        }
        if (currentHealth <= 0.3 * health)
        {
            Debug.Log("<=0.3*health");
            animator.SetBool("Cracked", true);
        }
        else { }
    }
    void skillwaitjishiqi()
    {
        if (waitskill == true)
        {
            timer += Time.deltaTime;
            if (timer >= skillInterval)
            {
                waitskill = false;
                timer = 0;
                transform.Find("skillReady").gameObject.SetActive(true);
            }
        }
    }
    public void TestSkill()
    {
        if (haveskill == true)
        {
            skill();
            haveskill = false;
            waitskill = true;
            transform.Find("skillReady").gameObject.SetActive(false);
        }
    }
    public void skill()
    {
        currentHealth = health;
        animator.SetBool("Cracked", false);
        animator.SetBool("Unhealthy", false);
        def += 3;
        defBuff();
    }
    public void defBuff()
    {
        def += 20;
        Invoke("defBack", 20);
    }
    public void defBack()
    {
        def -= 20;
    }
}
