using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JalapenoAttack : MonoBehaviour
{
    public float damage = 200;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void AniOver()
    {
        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Zombie")
        {
            other.GetComponent<ZombieNormal>().ChangeHealthBoom(-damage);
        }
        if (other.tag =="SpecialZombie")
        {
            other.GetComponent<SuperInvisibleZombie>().ChangeHealthBoom(-damage);
        }
    }
}
