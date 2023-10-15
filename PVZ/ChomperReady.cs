using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChomperReady : MonoBehaviour
{
    public float waitTime;
    private GameObject champing;
    private GameObject champReady;
    private GameObject p;
    public bool haveEat;
    bool ready;
    public float damage = 70;
    public float damageTimer=0;
    public float damageInterval = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        ready = true;
        p = transform.parent.gameObject;
        champReady = p.transform.Find("ChomperReady").gameObject;
        champing = p.transform.Find("ChomperChomping").gameObject;
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void Ready()
    {
        ready = true;
        champReady.SetActive(true);
        champing.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D other)//进入时调用一次
    {
        if (ready)
        {
            if (other.tag == "Zombie")
            {
                haveEat=other.GetComponent<ZombieNormal>().eatByPlant(damage);
                if (haveEat == true)
                {
                    ready = false;
                    champing.SetActive(true);
                    champReady.SetActive(false);
                    Invoke("Ready", waitTime);
                }
            }
            if (other.tag == "SpecialZombie")
            {
                other.GetComponent<SuperInvisibleZombie>().eatByPlant();
                ready = false;
                champing.SetActive(true);
                champReady.SetActive(false);
                Invoke("Ready", waitTime);
            }
        }
    }
    public void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Zombie")
        {
            damageTimer += Time.deltaTime;
            if (damageTimer >= damageInterval)
            {
                damageTimer = 0;
                other.GetComponent<ZombieNormal>().ChangeHealth(-damage);
            }
        }
        if (other.tag == "SpecialZombie")
        {
            damageTimer += Time.deltaTime;
            if (damageTimer >= damageInterval)
            {
                damageTimer = 0;
                other.GetComponent<SuperInvisibleZombie>().ChangeHealth(-damage);
            }
        }
    }
}
