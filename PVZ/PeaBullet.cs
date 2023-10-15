using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeaBullet : Bullet
{
    public GameObject effect;
    public Transform bulletPos;
    // Start is called before the first frame update
    void Start()
    {
        GameObject.Destroy(gameObject, 10);//ÑÓÊ±Ïú»Ù
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
            Instantiate(effect, bulletPos.position, Quaternion.identity);
            BaoJi();
            damage = damage * baojiBeilv;
            //Debug.Log(damage);
            other.GetComponent<ZombieNormal>().ChangeHealth(-damage);
            GameObject.Destroy(gameObject);
        }
        if (other.tag == "SpecialZombie")
        {
            Instantiate(effect, bulletPos.position, Quaternion.identity);
            BaoJi();
            damage = damage * baojiBeilv;
            //Debug.Log(damage);
            other.GetComponent<SuperInvisibleZombie>().ChangeHealth(-damage);
            GameObject.Destroy(gameObject);
        }
    }
}
