using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YellowBird : Bird
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        LineControl();
        skill();
    }
    protected override void skill()
    {
        base.skill();
        if (isuse == false&&isfly==true)
        {
            if (Input.GetMouseButtonDown(0))
            {
                isuse = true;
                rg.velocity = rg.velocity * 2;//ËÙ¶È·­±¶
            }
        }
    }
}
