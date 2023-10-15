using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieVisibleAni : MonoBehaviour
{
    private GameObject parent;
    private bool lostHead;
    private GameObject head;
    // Start is called before the first frame update
    void Start()
    {
        parent = transform.parent.gameObject;
        head = transform.Find("Head").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void HeadAniBegin()
    {
        if (lostHead == false)
        {
            head.SetActive(true);
            lostHead = true;
        }
    }
    public void DieAniOver()
    {
        Destroy(parent);
    }
}
