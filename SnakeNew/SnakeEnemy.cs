using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeEnemy : MonoBehaviour
{
    public float health = 100f; // 敌人的生命值

    // 受到伤害
    public void TakeDamage(float damage)
    {
        health -= damage;

        // 如果生命值降到0或者以下，销毁这个敌人
        if (health <= 0f)
        {
            Destroy(gameObject);
        }
    }
}
