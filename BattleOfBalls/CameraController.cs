using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public List<GameObject> balls;  // 创建一个公开的列表，用于存放游戏中的所有小球
    public Camera myCamera;  // 创建一个公开的Camera变量，用于存放摄像机的引用
    public float minGraphicSize = 5;
    public float transitionSpeed = 6.6f;
    public float speed = 0.1f;  // 小球移动速度
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

        // 计算所有小球的边界
        foreach (var ball in balls)
        {
            Vector3 position = ball.transform.position;
            minX = Mathf.Min(minX, position.x);
            maxX = Mathf.Max(maxX, position.x);
            minY = Mathf.Min(minY, position.y);
            maxY = Mathf.Max(maxY, position.y);
        }

        // 计算边界的中心，以便移动摄像机
        Vector3 center = new Vector3((minX + maxX) / 2, (minY + maxY) / 2, transform.position.z);
        transform.position = center;
        // 使用 Lerp 实现平滑的过渡
        //transform.position = Vector3.Lerp(transform.position, center, transitionSpeed * Time.deltaTime);


        // 计算新的正交尺寸，以便所有小球都在视野内
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
        // 如果用户没有输入方向
        if (Input.GetAxis("Horizontal") == 0 && Input.GetAxis("Vertical") == 0)
        {
            Vector3 averagePosition = Vector3.zero;

            // 计算所有小球的平均位置
            foreach (GameObject ball in balls)
            {
                averagePosition += ball.transform.position;
            }

            averagePosition /= balls.Count;

            // 让每个小球朝平均位置平缓移动
            foreach (GameObject ball in balls)
            {
                Rigidbody2D rb = ball.GetComponent<Rigidbody2D>();
                Vector3 direction = (averagePosition - ball.transform.position).normalized;
                rb.AddForce(direction * speed);
            }
        }
    }
}
