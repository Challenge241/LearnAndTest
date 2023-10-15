using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animationEnd : MonoBehaviour
{
    private Animator ani;
    public bool iswin=false;
    // Start is called before the first frame update
    void Start()
    {
        ani = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void animationDisabled()
    {
        ani.enabled = false;
        if (iswin == true)
        {
            AngryBirdManager._instance.showStars();
        }
    }
}
