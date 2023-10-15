using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightAttackU : KnightAttack
{
    public float lifespan = 5f; // 子弹的生存时间，单位为秒
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, lifespan);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 检测碰撞的对象是否是敌人
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // 如果是敌人，则对其造成伤害
            collision.gameObject.GetComponent<KnightEnemy>().TakeDamage(damage);

            // 计算击退的方向（从骑士到敌人的方向）
            Vector2 knockbackDirection = collision.transform.position - transform.position;

            // 归一化方向向量，使其长度为1
            knockbackDirection.Normalize();

            // 在敌人上获取Rigidbody2D组件，并添加力以击退敌人
            Rigidbody2D enemyRb = collision.gameObject.GetComponent<Rigidbody2D>();
            enemyRb.AddForce(knockbackDirection * knockbackForce, ForceMode2D.Impulse);

            Destroy(gameObject);
        }
    }

}
