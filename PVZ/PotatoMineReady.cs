using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotatoMineReady : Plant
{
    public float waitTime;
    private GameObject PotatoMineMashed;
    private GameObject p2;
    // Start is called before the first frame update
    void Start()
    {
        //PotatoMineMashed
        PotatoMineMashed = transform.Find("PotatoMineMashed").gameObject;
        p2 = transform.parent.gameObject;//��ȡ�丸����
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Zombie")
        {
            PotatoMineMashed.SetActive(true);
            Destroy(p2, 1);//1������ٸ�����
        }
    }
}
