using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torchwood : Plant
{
    public GameObject FireBulletPrefab;
    public Transform firebulletPos;
    public float timer;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = health;
        InvokeRepeating("TestSkill", 0.5f, 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        skillwaitjishiqi();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log("Enter");
        if (other.tag == "PeaBullet")
        {
            //Debug.Log("getTag");
            other.GetComponent<PeaBullet>().DestoryBullet();
            Instantiate(FireBulletPrefab, firebulletPos.position, Quaternion.identity);
        }
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
        transform.Find("fireAttack").gameObject.SetActive(true);
        Invoke("back", 1);
    }
    public void back()
    {
        transform.Find("fireAttack").gameObject.SetActive(false);
    }
}
