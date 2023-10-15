using UnityEngine;

public class BallFoodGenerator : MonoBehaviour
{
    public GameObject[] foodPrefabs; // Ԥ��������
    public int numberOfFoods = 100;  // ����ʳ�������
    public float spawnRange = 50.0f;  // ����ʳ��ķ�Χ
    public float spawnInterval = 1.0f;  // ����ʳ���ʱ��������λ���룩
    private void Awake()
    {

    }
    void Start()
    {
        // ����Ϸ��ʼʱ��������һЩʳ��
        for (int i = 0; i < numberOfFoods; i++)
        {
            SpawnFood();
        }

        // ���ö�ʱ����ʳ��
        InvokeRepeating(nameof(SpawnFood), spawnInterval, spawnInterval);
    }

    void SpawnFood()
    {
        // �������λ��
        float spawnPosX = Random.Range(-spawnRange, spawnRange);
        float spawnPosY = Random.Range(-spawnRange, spawnRange);
        Vector3 spawnPos = new Vector3(spawnPosX, spawnPosY, 0);
        // ��Ԥ�������������ѡ��һ��Ԥ����
        int randomIndex = Random.Range(0, foodPrefabs.Length);
        GameObject randomFoodPrefab = foodPrefabs[randomIndex];
        // ����ʳ��
        GameObject food = Instantiate(randomFoodPrefab, spawnPos, Quaternion.identity);

        // ���������ɫ
        Color randomColor = new Color(Random.value, Random.value, Random.value);

        // �������ɫӦ�õ�ʳ��
        Renderer renderer = food.GetComponent<Renderer>();
        if (renderer != null)
        {
            renderer.material.color = randomColor;
        }
    }
}
