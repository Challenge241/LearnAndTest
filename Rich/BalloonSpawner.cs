using System.Collections;
using UnityEngine;

public class BalloonSpawner : MonoBehaviour
{
    public GameObject balloonPrefab; // Balloon预制体
    public float spawnRate = 1.0f; // 每秒生成气球的数量
    public Vector2 spawnRangeX = new Vector2(-10.0f, 10.0f); // X轴的生成范围
    public float spawnHeight = -5.0f; // Y轴的生成位置

    private void OnEnable()
    {
        StartCoroutine(SpawnBalloons());
    }

    private IEnumerator SpawnBalloons()
    {
        while (true)
        {
            SpawnBalloon();
            yield return new WaitForSeconds(1.0f / spawnRate);
        }
    }

    private void SpawnBalloon()
    {
        float spawnX = Random.Range(spawnRangeX.x, spawnRangeX.y);
        Vector2 spawnPosition = new Vector2(spawnX, spawnHeight);
        Instantiate(balloonPrefab, spawnPosition, Quaternion.identity);
    }
}

