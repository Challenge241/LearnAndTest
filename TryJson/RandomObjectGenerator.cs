using UnityEngine;

public class RandomObjectGenerator : MonoBehaviour
{
    public GameObject objectPrefab; // ��Ϸ����Ԥ�裬��Ҫ��Unity�༭����ָ��
    public Sprite[] sprites; // �������飬��Ҫ��Unity�༭����ָ��
    public int objectCount = 5; // Ҫ���ɵĶ�������
    public Vector2 spawnAreaSize = new Vector2(10f, 10f); // ��������Ĵ�С
    public Vector2 minMaxSize = new Vector2(0.5f, 2f); // �������С������С
    // Start is called before the first frame update
    void Start()
    {
        SpawnObjects();
    }

    void SpawnObjects()
    {
        for (int i = 0; i < objectCount; i++)
        {
            // ����һ�����λ��
            float randomX = Random.Range(-spawnAreaSize.x / 2, spawnAreaSize.x / 2);
            float randomY = Random.Range(-spawnAreaSize.y / 2, spawnAreaSize.y / 2);
            Vector3 spawnPosition = new Vector3(randomX, randomY, 0);

            // �����λ����ʵ����һ����Ϸ����Ԥ��
            GameObject newObj = Instantiate(objectPrefab, spawnPosition, Quaternion.identity);
            // ���¶�������ΪGenerator��Ϸ������Ӷ���
            newObj.transform.SetParent(this.transform);
            // ���ѡ��һ�����鲢������¶���
            SpriteRenderer spriteRenderer = newObj.GetComponent<SpriteRenderer>();
            if (spriteRenderer != null && sprites.Length > 0)
            {
                spriteRenderer.sprite = sprites[Random.Range(0, sprites.Length)];
            }

            // ����һ�������С�����ø��¶���
            float randomSize = Random.Range(minMaxSize.x, minMaxSize.y);
            newObj.transform.localScale = new Vector3(randomSize, randomSize, randomSize);
        }
    }
}
