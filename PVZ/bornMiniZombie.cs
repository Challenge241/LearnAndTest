using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bornMiniZombie : MonoBehaviour
{
    public ZombieNormal api;
    public Transform PosA;
    public Transform PosB;
    public GameObject miniZ;
    public bool have=false;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("healthTestandBorn", 0.7f, 0.7f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void healthTestandBorn()
    {
        if (have == false)
        {
            if (api.currentHealth <= 0)
            {
                Instantiate(miniZ, PosA.position, Quaternion.identity);
                Instantiate(miniZ, PosB.position, Quaternion.identity);
                have = true;
            }
        }
    }
}
