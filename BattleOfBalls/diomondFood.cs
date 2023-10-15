using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class diomondFood : BallFood
{
    // Start is called before the first frame update
    void Start()
    {
        Vector3 localScale = transform.localScale;
        area = 0.5f*localScale.x * localScale.y; // 计算菱形的面积
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
