using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotatoMineNotReady : Plant
{
    public float waitTime;
    private GameObject PotatoMineReady;
    // Start is called before the first frame update
    void Start()
    {
        PotatoMineReady = transform.Find("PotatoMineReady").gameObject;
        Invoke("Ready", waitTime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Ready()
    {
        PotatoMineReady.SetActive(true);
    }
}
