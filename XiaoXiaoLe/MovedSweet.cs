// 引入需要的命名空间
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 定义一个公开的MovedSweet类，继承自Unity3D的基类MonoBehaviour
public class MovedSweet : MonoBehaviour
{
    //将甜品根据其是第几行第几列移动到指定位置
    // 定义一个私有的GameSweet类型的变量sweet，用于存储糖果的信息
    private GameSweet sweet;
    private IEnumerator moveCoroutine; // 移动的协程
    // Awake方法在对象被初始化时调用，用于设置sweet变量的值
    private void Awake()
    {
        // GetComponent<GameSweet>()是从当前对象的组件中获取类型为GameSweet的组件
        // 这里是获取当前游戏对象的GameSweet组件，并赋值给sweet变量
        sweet = GetComponent<GameSweet>();
    }

    // 定义一个公开的Move方法，用于移动糖果到新的位置
    public void Move(int newX, int newY, float time)
    {

        if (moveCoroutine != null)
        {
            StopCoroutine(moveCoroutine); // 如果之前有正在进行的移动协程，停止它
        }

        moveCoroutine = MoveCoroutine(newX, newY, time); // 创建新的移动协程
        StartCoroutine(moveCoroutine); // 启动移动协程
    }
    // 移动甜品的协程
    private IEnumerator MoveCoroutine(int newX, int newY, float time)
    {
        //print("move");
        // 将新的位置值赋给sweet的X和Y属性
        sweet.X = newX;
        sweet.Y = newY;

        Vector3 startPos = transform.position; // 起始位置
        // 调用sweet的llkGameManager的CorrectPositon方法获取正确的位置，然后将此位置赋给sweet的本地位置
        // llkGameManager的CorrectPositon方法可能是用于根据游戏规则调整位置的方法
        Vector3 endPos = sweet.llkGameManager.CorrectPositon(newX, newY); // 目标位置

        for (float t = 0; t < time; t += Time.deltaTime)
        {
            sweet.transform.position = Vector3.Lerp(startPos, endPos, t / time); // 插值计算当前位置
            yield return 0; // 等待一帧
        }

        sweet.transform.position = endPos; // 移动结束，将甜品置于目标位置
    }
    // Start方法在游戏开始前的第一帧更新时调用，现在为空，需要在实现时填写具体的代码
    void Start()
    {

    }

    // Update方法在每一帧更新时调用，现在为空，需要在实现时填写具体的代码
    void Update()
    {

    }
}
