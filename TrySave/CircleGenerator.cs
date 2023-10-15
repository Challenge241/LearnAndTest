using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleGenerator : MonoBehaviour
{
    public GameObject circlePrefab; // Բ�ε�Ԥ����
    public int numberOfCircles = 10; // Ҫ���ɵ�Բ������
    public float minSize = 1f; // ��СԲ�δ�С
    public float maxSize = 3f; // ���Բ�δ�С
    private void Start()
    {
            GenerateCircles();
    }

    private void GenerateCircles()
    {
        for (int i = 0; i < numberOfCircles; i++)
        {
            // �������λ��
            Vector3 randomPosition = new Vector3(Random.Range(-5f, 5f), Random.Range(-5f, 5f), 0f);

            // ���������ɫ
            Color randomColor = new Color(Random.value, Random.value, Random.value);

            // ���������С
            float randomSize = Random.Range(minSize, maxSize);

            // ʵ����Բ�ζ���
            GameObject circle = Instantiate(circlePrefab, randomPosition, Quaternion.identity);
            // ����Բ�ε���ɫ�ʹ�С
            SpriteRenderer circleRenderer = circle.GetComponent<SpriteRenderer>();
            circleRenderer.color = randomColor;
            circle.transform.localScale = new Vector3(randomSize, randomSize, 1f);

            // ��ȡ��Ϸ�����ϵ�CircleData�ű�
            CircleData circleDataScript = circle.GetComponent<CircleData>();

            // ���CircleData�ű����ڣ���ֵ����ӵ�CircleManager���б���
            if (circleDataScript != null)
            {
                circleDataScript.position = randomPosition;
                circleDataScript.size = randomSize;
                circleDataScript.color = randomColor;

                // ��ӵ�CircleManager��circleDataList�б���
                CircleDataListWrapper.Instance.circleDataList.Add(circleDataScript);
            }
            else
            {
                Debug.LogError("CircleData script not found on the circle GameObject.");
            }
        }
    }
}
