using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class car : MonoBehaviour
{
    public Vector3 direction;
    public float speed = 6;
    public float damage = 2000;
    bool haveRun = false;
   // public float timer;
    // Start is called before the first frame update
    void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {
        if (haveRun == true)
        { transform.position += direction * speed * Time.deltaTime; }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Zombie")
        {
            Destroy(gameObject, 10);
            haveRun = true;
            //Debug.Log(damage);
            other.GetComponent<ZombieNormal>().ChangeHealth(-damage);
        }
        if (other.tag == "SpecialZombie")
        {
            Destroy(gameObject, 10);
            haveRun = true;
            //Debug.Log(damage);
            other.GetComponent<SuperInvisibleZombie>().ChangeHealth(-damage);
        }
    }
}
