using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XClose : MonoBehaviour
{
    public GameObject targetGameObject; // 指定的游戏对象，可以在Inspector中设置或通过代码设置
    public void Xone()
    {
        if (targetGameObject != null)
        {
            targetGameObject.SetActive(false); // 将指定的游戏对象设置为非活动状态
        }
        else
        {
            Debug.LogError("Target GameObject is not assigned.");
        }
    }
}
