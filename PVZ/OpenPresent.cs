using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenPresent : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void openprensent()
    {
        transform.parent.gameObject.transform.Find("PresentOpen").gameObject.SetActive(true);
        transform.parent.gameObject.transform.Find("haha").gameObject.SetActive(true);
    }
    public void haveread()
    {
        transform.parent.gameObject.transform.Find("haha").gameObject.SetActive(false);
    }
}
