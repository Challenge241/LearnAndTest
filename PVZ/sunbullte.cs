using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sunbullte : Bullet
{
    // Start is called before the first frame update
    void Start()
    {
        GameObject.Destroy(gameObject, 10);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += direction * speed * Time.deltaTime;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Zombie")
        {
            BaoJi();
            damage = damage * baojiBeilv;
            //Debug.Log(damage);
            other.GetComponent<ZombieNormal>().ChangeHealth(-damage);
            //GameObject.Destroy(gameObject);
        }
        if (other.tag == "SpecialZombie")
        {
            BaoJi();
            damage = damage * baojiBeilv;
            //Debug.Log(damage);
            other.GetComponent<SuperInvisibleZombie>().ChangeHealth(-damage);
            //GameObject.Destroy(gameObject);
        }
    }
}
