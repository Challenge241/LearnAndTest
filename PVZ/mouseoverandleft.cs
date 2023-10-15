using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mouseoverandleft : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void enterAndShow()
    {
        //Debug.LogWarning("OnMouseEnter");
        if(transform.Find("Image").gameObject.activeSelf==false)
        {
            transform.Find("Image").gameObject.SetActive(true);
        }
    }
    public void ExitAndNotShow()
    {
        transform.Find("Image").gameObject.SetActive(false);
    }
}
