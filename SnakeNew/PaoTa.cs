using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaoTa : MonoBehaviour
{
    public GameObject bulletPrefab; // 子弹预制体，这是将要发射的子弹的模板
    public float fireRate = 1f; // 发射频率，单位为每秒多少次
    private float nextFire = 0f; // 下一次发射子弹的时间
    private GameObject target; // 炮台当前的目标

    void Update()
    {
        // 在每帧中，寻找最近的敌人并将其设置为目标
        target = FindClosestEnemy();

        if (target != null)
        {
            // 如果有目标（即，目标不为空）

            // 计算目标和炮台之间的向量
            Vector2 direction = target.transform.position - transform.position;

            // 计算目标的角度
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, angle);

            // 如果当前时间超过了下一次发射子弹的时间，则发射子弹
            if (Time.time > nextFire)
            {
                // 更新下一次发射子弹的时间
                nextFire = Time.time + 1f / fireRate;
                // 发射子弹
                FireBullet(direction);
            }
        }
    }

    GameObject FindClosestEnemy()
    {
        // 查找场景中所有带有 "Enemy" 标签的游戏对象
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        // 初始化最近敌人为空和最近距离为无穷大
        GameObject closestEnemy = null;
        float closestDistanceSqr = Mathf.Infinity;
        Vector2 currentPosition = transform.position;

        // 遍历每一个敌人
        foreach (GameObject enemy in enemies)
        {
            // 计算敌人和炮台之间的向量
            Vector2 directionToEnemy = (Vector2)enemy.transform.position - currentPosition;
            //print((Vector2)enemy.transform.position);

            // 计算向量的平方长度（这比计算实际长度更快）
            float dSqrToEnemy = directionToEnemy.sqrMagnitude;

            // 如果这个敌人比当前最近的敌人更近，那么更新最近的敌人和最近的距离
            if (dSqrToEnemy < closestDistanceSqr)
            {
                closestDistanceSqr = dSqrToEnemy;
                closestEnemy = enemy;
            }
        }

        // 返回最近的敌人
        return closestEnemy;
    }

    void FireBullet(Vector2 direction)
    {
        // 实例化一个子弹对象
        GameObject bulletObject = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        // 使子弹的朝向指向敌人
        // Rotate the bullet to point towards the enemy
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        bulletObject.transform.rotation = Quaternion.Euler(0f, 0f, angle);

        // 获取子弹对象的刚体组件
        Rigidbody2D bulletRigidbody = bulletObject.GetComponent<Rigidbody2D>();

        // 如果子弹有刚体组件（也就是说，子弹受物理引擎控制）
        if (bulletRigidbody != null)
        {
            // 为子弹添加一个力，使其沿着目标方向飞去
            // 这个力的方向是目标的方向（已经归一化，也就是说，长度为1），大小是500（你可以根据需要调整这个值）
            bulletRigidbody.AddForce(direction.normalized * 500f);
        }
    }
}

