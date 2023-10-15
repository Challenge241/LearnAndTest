using UnityEngine;

public class BallFoodGenerator : MonoBehaviour
{
    public GameObject[] foodPrefabs; // 预设物数组
    public int numberOfFoods = 100;  // 生成食物的数量
    public float spawnRange = 50.0f;  // 生成食物的范围
    public float spawnInterval = 1.0f;  // 生成食物的时间间隔（单位：秒）
    private void Awake()
    {

    }
    void Start()
    {
        // 在游戏开始时立即生成一些食物
        for (int i = 0; i < numberOfFoods; i++)
        {
            SpawnFood();
        }

        // 设置定时生成食物
        InvokeRepeating(nameof(SpawnFood), spawnInterval, spawnInterval);
    }

    void SpawnFood()
    {
        // 生成随机位置
        float spawnPosX = Random.Range(-spawnRange, spawnRange);
        float spawnPosY = Random.Range(-spawnRange, spawnRange);
        Vector3 spawnPos = new Vector3(spawnPosX, spawnPosY, 0);
        // 在预设物数组中随机选择一个预设物
        int randomIndex = Random.Range(0, foodPrefabs.Length);
        GameObject randomFoodPrefab = foodPrefabs[randomIndex];
        // 生成食物
        GameObject food = Instantiate(randomFoodPrefab, spawnPos, Quaternion.identity);

        // 生成随机颜色
        Color randomColor = new Color(Random.value, Random.value, Random.value);

        // 将随机颜色应用到食物
        Renderer renderer = food.GetComponent<Renderer>();
        if (renderer != null)
        {
            renderer.material.color = randomColor;
        }
    }
}
