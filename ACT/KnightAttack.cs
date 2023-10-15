using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class KnightAttack : MonoBehaviour
{
    public int damage=20; // �˺�
    public float knockbackForce=10; // ��.

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // �����ײ�Ķ����Ƿ��ǵ���
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // ����ǵ��ˣ����������˺�
            collision.gameObject.GetComponent<KnightEnemy>().TakeDamage(damage);

            // ������˵ķ��򣨴���ʿ�����˵ķ���
            Vector2 knockbackDirection = collision.transform.position - transform.position;

            // ��һ������������ʹ�䳤��Ϊ1
            knockbackDirection.Normalize();

            // �ڵ����ϻ�ȡRigidbody2D�������������Ի��˵���
            Rigidbody2D enemyRb = collision.gameObject.GetComponent<Rigidbody2D>();
            enemyRb.AddForce(knockbackDirection * knockbackForce, ForceMode2D.Impulse);
            print(knockbackDirection * knockbackForce);
        }
    }
}

