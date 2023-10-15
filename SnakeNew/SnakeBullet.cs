using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeBullet : MonoBehaviour
{
    public float lifespan = 5f; // �ӵ�������ʱ�䣬��λΪ��
    public float damage = 10f; // �ӵ��Ե��˵��˺�

    // �ڿ�ʼʱ���趨һ��ʱ��������ӵ�
    void Start()
    {
        Destroy(gameObject, lifespan);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // �����ײ����Ϸ�����Ƿ��ǵ���
        SnakeEnemy enemy = collision.gameObject.GetComponent<SnakeEnemy>();
        if (enemy != null)
        {
            // ����ǵ��ˣ���������˺�
            enemy.TakeDamage(damage);

            // �����ӵ�
            Destroy(gameObject);
        }
    }
}

