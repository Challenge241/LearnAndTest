using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightAttackU : KnightAttack
{
    public float lifespan = 5f; // �ӵ�������ʱ�䣬��λΪ��
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, lifespan);
    }
    private void OnTriggerEnter2D(Collider2D collision)
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

            Destroy(gameObject);
        }
    }

}
