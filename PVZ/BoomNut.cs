using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomNut : WallNut
{
    public float timer2;
    public GameObject boom;
    public Transform boomPos;
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
    private void OnDestroy()
    {
        Instantiate(boom, boomPos.position, Quaternion.identity);
    }
    void skillwaitjishiqi()
    {
        if (waitskill == true)
        {
            timer2 += Time.deltaTime;
            if (timer2 >= skillInterval)
            {
                waitskill = false;
                timer2 = 0;
                transform.Find("skillReady").gameObject.SetActive(true);
            }
        }
    }
    public new void TestSkill()
    {
        if (haveskill == true)
        {
            skill();
            haveskill = false;
            waitskill = true;
            transform.Find("skillReady").gameObject.SetActive(false);
        }
    }
    public new void skill()
    {
        currentHealth = health;
        Instantiate(boom, boomPos.position, Quaternion.identity);
    }
}































