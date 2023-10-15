using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Vector3 direction;
    public float speed;
    public float damage;
    public float suijishu;
    public float baojiBeilv=1;
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
            GameObject.Destroy(gameObject);
        }
        if (other.tag == "SpecialZombie")
        {
            BaoJi();
            damage = damage * baojiBeilv;
            //Debug.Log(damage);
            other.GetComponent<SuperInvisibleZombie>().ChangeHealth(-damage);
            GameObject.Destroy(gameObject);
        }
    }
    public void BaoJi()
    {
        suijishu = Random.Range(1, 1001);
        if (suijishu == 1)
        {
            baojiBeilv = 30;
        }else if (1 < suijishu && suijishu <= 6)
        {
            baojiBeilv = 10;
        }else if(10< suijishu && suijishu <= 20)
        {
            baojiBeilv = 5;
        }else if(50< suijishu && suijishu <= 100)
        {
            baojiBeilv = 3;
        }else if(200< suijishu && suijishu <= 300)
        {
            baojiBeilv = 2;
        }
        else if (400 < suijishu && suijishu <= 550)
        {
            baojiBeilv = 1.5f;
        }else if (600 < suijishu && suijishu <= 617)
        {
            baojiBeilv = 4;
        }
        else
        {
            baojiBeilv = 1;
        }
    }
    public void DestoryBullet()
    {
        Destroy(gameObject);
    }
}
