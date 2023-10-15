using UnityEngine;

public class BallFood : MonoBehaviour
{
    public float area;  // 食物的面积
    void Start()
    {
            // 计算食物的面积
            float sideLength = transform.localScale.x / 2;
            area = (3 * Mathf.Sqrt(3) / 2) * Mathf.Pow(sideLength, 2);  
    }
}
