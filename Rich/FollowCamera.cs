using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public Transform target; // 跟随的目标对象
    public Vector3 offset; // 从目标对象到摄像头的偏移量

    private void LateUpdate() // 在每一帧的最后执行，以确保目标对象的移动已经完成
    {
        transform.position = target.position + offset; // 设置摄像头的位置为目标位置加上偏移量
    }
}

