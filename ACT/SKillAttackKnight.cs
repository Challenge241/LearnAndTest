using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SKillAttackKnight : MonoBehaviour
{
    public int damage = 30; // 伤害

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 检测碰撞的对象是否是敌人
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // 如果是敌人，则对其造成伤害
            collision.gameObject.GetComponent<KnightEnemy>().TakeDamage(damage);
        }
    }
    public void SkillEnd()
    {
        transform.gameObject.SetActive(false);
    }
}
