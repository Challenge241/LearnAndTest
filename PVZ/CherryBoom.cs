using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CherryBoom : MonoBehaviour
{
    public float damage = 2000;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Zombie")
        {
            other.GetComponent<ZombieNormal>().ChangeHealthBoom(-damage);
        }
        if (other.tag == "SpecialZombie")
        {
            other.GetComponent<SuperInvisibleZombie>().ChangeHealthBoom(-damage);
        }
    }
}
