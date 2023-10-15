using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaoTa : MonoBehaviour
{
    public GameObject bulletPrefab; // �ӵ�Ԥ���壬���ǽ�Ҫ������ӵ���ģ��
    public float fireRate = 1f; // ����Ƶ�ʣ���λΪÿ����ٴ�
    private float nextFire = 0f; // ��һ�η����ӵ���ʱ��
    private GameObject target; // ��̨��ǰ��Ŀ��

    void Update()
    {
        // ��ÿ֡�У�Ѱ������ĵ��˲���������ΪĿ��
        target = FindClosestEnemy();

        if (target != null)
        {
            // �����Ŀ�꣨����Ŀ�겻Ϊ�գ�

            // ����Ŀ�����̨֮�������
            Vector2 direction = target.transform.position - transform.position;

            // ����Ŀ��ĽǶ�
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, angle);

            // �����ǰʱ�䳬������һ�η����ӵ���ʱ�䣬�����ӵ�
            if (Time.time > nextFire)
            {
                // ������һ�η����ӵ���ʱ��
                nextFire = Time.time + 1f / fireRate;
                // �����ӵ�
                FireBullet(direction);
            }
        }
    }

    GameObject FindClosestEnemy()
    {
        // ���ҳ��������д��� "Enemy" ��ǩ����Ϸ����
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        // ��ʼ���������Ϊ�պ��������Ϊ�����
        GameObject closestEnemy = null;
        float closestDistanceSqr = Mathf.Infinity;
        Vector2 currentPosition = transform.position;

        // ����ÿһ������
        foreach (GameObject enemy in enemies)
        {
            // ������˺���̨֮�������
            Vector2 directionToEnemy = (Vector2)enemy.transform.position - currentPosition;
            //print((Vector2)enemy.transform.position);

            // ����������ƽ�����ȣ���ȼ���ʵ�ʳ��ȸ��죩
            float dSqrToEnemy = directionToEnemy.sqrMagnitude;

            // ���������˱ȵ�ǰ����ĵ��˸�������ô��������ĵ��˺�����ľ���
            if (dSqrToEnemy < closestDistanceSqr)
            {
                closestDistanceSqr = dSqrToEnemy;
                closestEnemy = enemy;
            }
        }

        // ��������ĵ���
        return closestEnemy;
    }

    void FireBullet(Vector2 direction)
    {
        // ʵ����һ���ӵ�����
        GameObject bulletObject = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        // ʹ�ӵ��ĳ���ָ�����
        // Rotate the bullet to point towards the enemy
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        bulletObject.transform.rotation = Quaternion.Euler(0f, 0f, angle);

        // ��ȡ�ӵ�����ĸ������
        Rigidbody2D bulletRigidbody = bulletObject.GetComponent<Rigidbody2D>();

        // ����ӵ��и��������Ҳ����˵���ӵ�������������ƣ�
        if (bulletRigidbody != null)
        {
            // Ϊ�ӵ����һ������ʹ������Ŀ�귽���ȥ
            // ������ķ�����Ŀ��ķ����Ѿ���һ����Ҳ����˵������Ϊ1������С��500������Ը�����Ҫ�������ֵ��
            bulletRigidbody.AddForce(direction.normalized * 500f);
        }
    }
}

