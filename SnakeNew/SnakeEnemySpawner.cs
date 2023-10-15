using System.Collections;
using UnityEngine;
public class SnakeEnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab; // ����Ԥ����
    public float spawnRate = 1f; // ���ɵ��˵�Ƶ�ʣ�ÿ����ٸ�
    public Vector2 spawnArea = new Vector2(10f, 10f); // ������������Ĵ�С
    public int initialSpawnCount = 10; // ��ʼһ�������ɵĵ�������

    void Start()
    {
        // ��ʼʱһ��������һ�������ĵ���
        for (int i = 0; i < initialSpawnCount; i++)
        {
            SpawnEnemy();
        }

        // ��ʼ�����Ե������µĵ���
        StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies()
    {
        while (true)
        {
            // �ȴ�һ��ʱ��
            yield return new WaitForSeconds(1f / spawnRate);

            // ����һ���µĵ���
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
