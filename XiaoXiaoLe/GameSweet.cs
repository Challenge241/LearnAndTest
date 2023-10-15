using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 定义一个名为GameSweet的公开类，它继承自MonoBehaviour类，MonoBehaviour是Unity3D中所有脚本的基类
public class GameSweet : MonoBehaviour
{
    // 定义两个私有整数变量x和y，可能用于表示游戏中糖果的位置
    private int x;
    private int y;

    // 定义两个公开属性X和Y，它们的get方法用于获取x和y的值
    // X的set方法在给x赋值前会检查糖果是否可以移动，如果可以，才进行赋值
    // Y的set方法直接对y进行赋值，不做任何检查
    public int X { get => x; set { if (CanMove()) { x = value; } } }
    public int Y { get => y; set { if (CanMove()) { y = value; } } }

    // 定义一个公开的SweetsType类型的属性Type，其get和set方法分别用于获取和设置type的值
    public LLKGameManager.SweetsType Type { get => type; set => type = value; }

    // 定义一个公开的MovedSweet类型的属性MovedComponent，其get方法用于获取movedComponent的值，没有定义set方法
   

    // 定义一个私有的SweetsType类型的变量type，可能用于表示糖果的类型
    private LLKGameManager.SweetsType type;

    // 使用HideInInspector属性标记llkGameManager，这将在Unity的Inspector面板中隐藏此字段
    // 定义一个公开的LLKGameManager类型的变量llkGameManager，可能用于管理游戏的各种状态
    [HideInInspector]
    public LLKGameManager llkGameManager;

    // 定义一个私有的MovedSweet类型的变量movedComponent，可能用于控制糖果的移动
    private MovedSweet movedComponent;
    public MovedSweet MovedComponent { get => movedComponent; }


    private ColorSweet coloredComponent;
    public ColorSweet ColoredComponent { get => coloredComponent; }
    private ClearedSweet clearedComponent;
    public ClearedSweet ClearedComponent
    {
        get
        {
            return clearedComponent;
        }
    }


    // 定义一个公开的CanMove方法，如果movedComponent不为null，表示糖果可以移动，返回true
    public bool CanMove()
    {
        return movedComponent != null;
    }
    public bool CanColor()
    {
        return coloredComponent != null;
    }
    public bool CanClear()
    {
        // 判断甜品是否具有 ClearedComponent 组件
        return clearedComponent != null;
    }
    // 定义一个公开的Init方法，用于初始化糖果的属性，包括位置、所属的游戏管理器和糖果类型
    public void Init(int _x, int _y, LLKGameManager _llkgameManager, LLKGameManager.SweetsType _type)
    {
        x = _x;
        y = _y;
        llkGameManager = _llkgameManager;
        type = _type;
    }
    private void Awake()
    {
        movedComponent = GetComponent<MovedSweet>();
        coloredComponent = GetComponent<ColorSweet>();
        clearedComponent = GetComponent<ClearedSweet>();
    }
    private void OnMouseEnter()
    {
        // 当鼠标进入甜品时，通知游戏管理器
        llkGameManager.EnterSweet(this);
    }

    private void OnMouseDown()
    {
        // 当鼠标按下甜品时，通知游戏管理器
        llkGameManager.PressSweet(this);
    }

    private void OnMouseUp()
    {
        // 当鼠标松开甜品时，通知游戏管理器
        llkGameManager.ReleaseSweet();
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
