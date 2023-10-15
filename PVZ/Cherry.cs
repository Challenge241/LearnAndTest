using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cherry : MonoBehaviour
{
    private GameObject child;
    // Start is called before the first frame update
    void Start()
    {
        child = transform.Find("Boom").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void AniOver()
    {
        child.SetActive(true);
        Destroy(gameObject, 1);
    }
}
