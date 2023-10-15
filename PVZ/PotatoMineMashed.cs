using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotatoMineMashed : MonoBehaviour
{
    public float damage = 2000;
    // Start is called before the first frame update
    void Start()
    {
        
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
    }
}
