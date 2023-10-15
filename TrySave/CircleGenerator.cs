using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleGenerator : MonoBehaviour
{
    public GameObject circlePrefab; // 圆形的预制体
    public int numberOfCircles = 10; // 要生成的圆形数量
    public float minSize = 1f; // 最小圆形大小
    public float maxSize = 3f; // 最大圆形大小
    private void Start()
    {
            GenerateCircles();
    }

    private void GenerateCircles()
    {
        for (int i = 0; i < numberOfCircles; i++)
        {
            // 生成随机位置
            Vector3 randomPosition = new Vector3(Random.Range(-5f, 5f), Random.Range(-5f, 5f), 0f);

            // 生成随机颜色
            Color randomColor = new Color(Random.value, Random.value, Random.value);

            // 生成随机大小
            float randomSize = Random.Range(minSize, maxSize);

            // 实例化圆形对象
            GameObject circle = Instantiate(circlePrefab, randomPosition, Quaternion.identity);
            // 设置圆形的颜色和大小
            SpriteRenderer circleRenderer = circle.GetComponent<SpriteRenderer>();
            circleRenderer.color = randomColor;
            circle.transform.localScale = new Vector3(randomSize, randomSize, 1f);

            // 获取游戏物体上的CircleData脚本
            CircleData circleDataScript = circle.GetComponent<CircleData>();

            // 如果CircleData脚本存在，赋值并添加到CircleManager的列表中
            if (circleDataScript != null)
            {
                circleDataScript.position = randomPosition;
                circleDataScript.size = randomSize;
                circleDataScript.color = randomColor;

                // 添加到CircleManager的circleDataList列表中
                CircleDataListWrapper.Instance.circleDataList.Add(circleDataScript);
            }
            else
            {
                Debug.LogError("CircleData script not found on the circle GameObject.");
            }
        }
    }
}
