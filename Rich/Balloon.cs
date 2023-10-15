using UnityEngine;

public class Balloon : MonoBehaviour
{
    public float speed = 2.0f; // 气球上升的速度
    public float lifetime = 10.0f; // 气球存在的时间
    public int scoreValue = 2; // 点击气球得到的分数
    private void Start()
    {
        Destroy(gameObject, lifetime);
    }
    private void Update()
    {
        transform.Translate(Vector3.up * speed * Time.deltaTime);
    }
    private void OnMouseDown()
    {
        if (ScoreManager.instance != null)
        {
            ScoreManager.instance.IncreaseScore(scoreValue);
        }
        else if (OnlyQiQiuManager.instance != null)
        {
            OnlyQiQiuManager.instance.IncreaseScore(scoreValue);
        }

        // 销毁游戏物体
        Destroy(gameObject);
    }

}
