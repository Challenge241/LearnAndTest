using System.Collections;
using UnityEngine;
public class SnakeEnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab; // 敌人预制体
    public float spawnRate = 1f; // 生成敌人的频率，每秒多少个
    public Vector2 spawnArea = new Vector2(10f, 10f); // 敌人生成区域的大小
    public int initialSpawnCount = 10; // 初始一次性生成的敌人数量

    void Start()
    {
        // 初始时一次性生成一定数量的敌人
        for (int i = 0; i < initialSpawnCount; i++)
        {
            SpawnEnemy();
        }

        // 开始周期性地生成新的敌人
        StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies()
    {
        while (true)
        {
            // 等待一段时间
            yield return new WaitForSeconds(1f / spawnRate);

            // 生成一个新的敌人
            SpawnEnemy();
        }
    }

    void SpawnEnemy()
    {
        Vector2 spawnPosition = new Vector2(
            Random.Range(-spawnArea.x / 2f, spawnArea.x / 2f),
            Random.Range(-spawnArea.y / 2f, spawnArea.y / 2f)
        );
        Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
    }
}
