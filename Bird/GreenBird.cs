using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenBird : Bird
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
        if (isuse == false && isfly == true)
        {
            if (Input.GetMouseButtonDown(0))
            {
                isuse = true;
                //让其在x轴的速度反向
                Vector3 speed = rg.velocity;
                speed.x *= -1;
                rg.velocity = speed;
            }
        }
    }
}
