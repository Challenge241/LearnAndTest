using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rectangleFood : BallFood
{
    // Start is called before the first frame update
    void Start()
    {
        Vector3 localScale = transform.localScale;
        area =  localScale.x * localScale.y; // 计算矩形的面积
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
