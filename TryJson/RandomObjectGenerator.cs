using UnityEngine;

public class RandomObjectGenerator : MonoBehaviour
{
    public GameObject objectPrefab; // 游戏对象预设，需要在Unity编辑器中指定
    public Sprite[] sprites; // 精灵数组，需要在Unity编辑器中指定
    public int objectCount = 5; // 要生成的对象数量
    public Vector2 spawnAreaSize = new Vector2(10f, 10f); // 生成区域的大小
    public Vector2 minMaxSize = new Vector2(0.5f, 2f); // 对象的最小和最大大小
    // Start is called before the first frame update
    void Start()
    {
        SpawnObjects();
    }

    void SpawnObjects()
    {
        for (int i = 0; i < objectCount; i++)
        {
            // 生成一个随机位置
            float randomX = Random.Range(-spawnAreaSize.x / 2, spawnAreaSize.x / 2);
            float randomY = Random.Range(-spawnAreaSize.y / 2, spawnAreaSize.y / 2);
            Vector3 spawnPosition = new Vector3(randomX, randomY, 0);

            // 用随机位置来实例化一个游戏对象预设
            GameObject newObj = Instantiate(objectPrefab, spawnPosition, Quaternion.identity);
            // 将新对象设置为Generator游戏对象的子对象
            newObj.transform.SetParent(this.transform);
            // 随机选择一个精灵并分配给新对象
            SpriteRenderer spriteRenderer = newObj.GetComponent<SpriteRenderer>();
            if (spriteRenderer != null && sprites.Length > 0)
            {
                spriteRenderer.sprite = sprites[Random.Range(0, sprites.Length)];
            }

            // 生成一个随机大小并设置给新对象
            float randomSize = Random.Range(minMaxSize.x, minMaxSize.y);
            newObj.transform.localScale = new Vector3(randomSize, randomSize, randomSize);
        }
    }
}
