using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeBullet : MonoBehaviour
{
    public float lifespan = 5f; // 子弹的生存时间，单位为秒
    public float damage = 10f; // 子弹对敌人的伤害

    // 在开始时，设定一段时间后销毁子弹
    void Start()
    {
        Destroy(gameObject, lifespan);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 检查碰撞的游戏对象是否是敌人
        SnakeEnemy enemy = collision.gameObject.GetComponent<SnakeEnemy>();
        if (enemy != null)
        {
            // 如果是敌人，对其造成伤害
            enemy.TakeDamage(damage);

            // 销毁子弹
            Destroy(gameObject);
        }
    }
}

