using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public List<GameObject> balls;  // ����һ���������б����ڴ����Ϸ�е�����С��
    public Camera myCamera;  // ����һ��������Camera���������ڴ�������������
    public float minGraphicSize = 5;
    public float transitionSpeed = 6.6f;
    public float speed = 0.1f;  // С���ƶ��ٶ�
    void Update()
    {
        controlCamera();
        combin();
    }
    private void controlCamera()
    {
        float minX = float.MaxValue;
        float maxX = float.MinValue;
        float minY = float.MaxValue;
        float maxY = float.MinValue;

        // ��������С��ı߽�
        foreach (var ball in balls)
        {
            Vector3 position = ball.transform.position;
            minX = Mathf.Min(minX, position.x);
            maxX = Mathf.Max(maxX, position.x);
            minY = Mathf.Min(minY, position.y);
            maxY = Mathf.Max(maxY, position.y);
        }

        // ����߽�����ģ��Ա��ƶ������
        Vector3 center = new Vector3((minX + maxX) / 2, (minY + maxY) / 2, transform.position.z);
        transform.position = center;
        // ʹ�� Lerp ʵ��ƽ���Ĺ���
        //transform.position = Vector3.Lerp(transform.position, center, transitionSpeed * Time.deltaTime);


        // �����µ������ߴ磬�Ա�����С������Ұ��
        float cameraHeight = Mathf.Max(maxX - minX, maxY - minY);
        if (cameraHeight > 5)
        {
            //Camera.main.orthographicSize = cameraHeight / 2;
            Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, cameraHeight, transitionSpeed * Time.deltaTime);
        }
        else
        {
            Camera.main.orthographicSize = minGraphicSize;
            //Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, minGraphicSize, transitionSpeed * Time.deltaTime);
        }
    }
    private void combin()
    {
        // ����û�û�����뷽��
        if (Input.GetAxis("Horizontal") == 0 && Input.GetAxis("Vertical") == 0)
        {
            Vector3 averagePosition = Vector3.zero;

            // ��������С���ƽ��λ��
            foreach (GameObject ball in balls)
            {
                averagePosition += ball.transform.position;
            }

            averagePosition /= balls.Count;

            // ��ÿ��С��ƽ��λ��ƽ���ƶ�
            foreach (GameObject ball in balls)
            {
                Rigidbody2D rb = ball.GetComponent<Rigidbody2D>();
                Vector3 direction = (averagePosition - ball.transform.position).normalized;
                rb.AddForce(direction * speed);
            }
        }
    }
}
