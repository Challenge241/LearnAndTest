using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeEnemy : MonoBehaviour
{
    public float health = 100f; // ���˵�����ֵ

    // �ܵ��˺�
    public void TakeDamage(float damage)
    {
        health -= damage;

        // �������ֵ����0�������£������������
        if (health <= 0f)
        {
            Destroy(gameObject);
        }
    }
}
