using UnityEngine;

public class BallFood : MonoBehaviour
{
    public float area;  // ʳ������
    void Start()
    {
            // ����ʳ������
            float sideLength = transform.localScale.x / 2;
            area = (3 * Mathf.Sqrt(3) / 2) * Mathf.Pow(sideLength, 2);  
    }
}
