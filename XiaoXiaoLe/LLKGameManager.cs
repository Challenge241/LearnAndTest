// 引入需要的命名空间
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 定义一个公开的LLKGameManager类，继承自Unity3D的基类MonoBehaviour
public class LLKGameManager : MonoBehaviour
{
    // 要交换的两个甜品对象
    private GameSweet pressedSweet;
    private GameSweet enteredSweet;
    private bool gameOver=false;
    // 定义一个公开的枚举类型SweetsType，表示甜品的种类
    public enum SweetsType
    {
        EMPTY,          // 空
        NORMAL,         // 普通
        BARRIER,        // 障碍
        ROW_CLEAR,      // 行消除
        COLUMN_CLEAR,   // 列消除
        COUNT           // 标记类型
    }
    // 甜品预制体字典，通过甜品种类来得到对应的甜品游戏物体
    public Dictionary<SweetsType, GameObject> sweetPrefabDict;

    // 结构体SweetPrefab，包含甜品类型和预制体
    [System.Serializable] // 使结构体可以序列化，这样在Unity编辑器中就可以看到并编辑这个结构体
    public struct SweetPrefab
    {
        public SweetsType type;   // 甜品类型
        public GameObject prefab; // 预制体
    }
    public SweetPrefab[] sweetPrefabs; // 甜品预制体数组

    // 单例实例化
    private static LLKGameManager _instance;
    public static LLKGameManager Instance
    {
        get { return _instance; }
        set { _instance = value; }
    }
    private void Awake()
    {
        _instance = this; // 在Awake方法中设置单例的实例
    }
    // 大网格的行列数
    public int xColumn;
    public int yRow;
    public GameObject gridPrefabs; // 网格预制体
    // 填充时间
    public float fillTime=0.1f;
    public float fillTimeMove = 0.39f;
    // 甜品数组，存储甜品脚本组件，不同甜品物体有数值不同的脚本
    private GameSweet[,] sweets;
    public GameSweet CreateNewSweet(int x, int y, SweetsType type)
    {
        GameObject newSweet = Instantiate(sweetPrefabDict[type], CorrectPositon(x, y), Quaternion.identity);
        newSweet.transform.parent = transform;

        sweets[x, y] = newSweet.GetComponent<GameSweet>();
        sweets[x, y].Init(x, y, this, type);

        return sweets[x, y];
    }
    // Start方法在游戏开始前的第一帧更新时调用
    void Start()
    {
        // 字典实例化
        sweetPrefabDict = new Dictionary<SweetsType, GameObject>();
        for (int i = 0; i < sweetPrefabs.Length; i++)
        {
            // 如果字典中不包含该甜品类型，则添加到字典中
            if (!sweetPrefabDict.ContainsKey(sweetPrefabs[i].type))
            {
                sweetPrefabDict.Add(sweetPrefabs[i].type, sweetPrefabs[i].prefab);
            }
        }

        // 地图实例化

        //法一：
        /*
        for (int x=-xColumn/2; x<xColumn/2;x++)
        {
            for (int y=-yRow/2;y<yRow/2;y++)
            {
                GameObject chocolate = Instantiate(gridPrefabs, new Vector2(x, y), Quaternion.identity);
                chocolate.transform.SetParent(transform);//把LLKGameManager作为其父物体
            }
        }*/
        // 方法二：
        for (int x = 0; x < xColumn; x++)
        {
            for (int y = 0; y < yRow; y++)
            {
                // 创建新的网格对象，并设置其位置和父对象
                GameObject chocolate = Instantiate(gridPrefabs, CorrectPositon(x, y), Quaternion.identity);
                chocolate.transform.SetParent(transform);
            }
        }
        //甜品实例化
        // 初始化一个二维数组来存储游戏中的甜品，数组的大小由xColumn和yRow决定
        sweets = new GameSweet[xColumn, yRow];
        // 使用嵌套循环来遍历这个二维数组
        for (int x = 0; x < xColumn; x++)
        {
            for (int y = 0; y < yRow; y++)
            {
                /*
                // 使用Instantiate函数创建一个新的甜品游戏对象，这个对象的模型是字典sweetPrefabDict中与SweetsType.NORMAL对应的游戏对象
                // 新甜品的位置是通过函数CorrectPositon计算出来的，这个函数会根据x和y的值来返回一个在游戏中正确的位置
                // Quaternion.identity表示没有任何旋转
                //CorrectPositon(x, y)
                GameObject newSweet = Instantiate(sweetPrefabDict[SweetsType.NORMAL],Vector3.zero , Quaternion.identity);
                // 设置新甜品的父对象为当前的游戏对象（LLKGameManager）
                newSweet.transform.SetParent(transform);
                // 获取新甜品上的GameSweet组件，并将其存储在二维数组sweets中对应的位置
                sweets[x, y] = newSweet.GetComponent<GameSweet>();
                // 使用Init函数初始化甜品的属性，包括其在数组中的位置(x, y)，其关联的LLKGameManager对象（this），以及其类型（SweetsType.NORMAL）
                sweets[x, y].Init(x, y, this, SweetsType.NORMAL);
                if (sweets[x, y].CanMove())
                {
                    sweets[x, y].MovedComponent.Move(x,y,fillTime);
                }
                if (sweets[x, y].CanColor())
                {
                    sweets[x, y].ColoredComponent.SetColor((ColorSweet.ColorType)(Random.Range(0,sweets[x,y].ColoredComponent.NumColors)));
                }*/
                CreateNewSweet(x, y, SweetsType.EMPTY);
            }
        }
        ClearAllMatchedSweet();
        StartCoroutine(AllFill());
    }
    // 定义一个公共方法CorrectPosition，接受两个整数作为参数（x, y）
    // 这个方法返回一个Vector2类型的对象，表示在二维空间中的一个位置
    public Vector2 CorrectPositon(int x, int y)
    {
        // 计算出新的x和y的位置
        // 通过从当前对象的x位置（transform.position.x）减去列数的一半（xColumn / 2f）再加上输入的x，得到新的x位置
        // 同样的，从当前对象的y位置（transform.position.y）减去行数的一半（yRow / 2f）再加上输入的y，得到新的y位置
        // 这样做的目的是将二维数组中的索引转换为游戏世界中的实际位置
        return new Vector2(transform.position.x - xColumn / 2f + x, transform.position.y + yRow / 2f - y);
    }
    // 甜品是否相邻的判断方法
    private bool IsFriend(GameSweet sweet1, GameSweet sweet2)
    {
        return (sweet1.X == sweet2.X && Mathf.Abs(sweet1.Y - sweet2.Y) == 1) || (sweet1.Y == sweet2.Y && Mathf.Abs(sweet1.X - sweet2.X) == 1);
    }

    public void PressSweet(GameSweet sweet)
    {
        if (gameOver)
        {
            return;
        }
        pressedSweet = sweet;
    }

    public void EnterSweet(GameSweet sweet)
    {
        if (gameOver)
        {
            return;
        }
        enteredSweet = sweet;
    }

    public void ReleaseSweet()
    {
        if (gameOver)
        {
            return;
        }
        if (IsFriend(pressedSweet, enteredSweet))
        {
            //print("IsFriend");
            ExchangeSweets(pressedSweet, enteredSweet);
        }
    }
    private void ExchangeSweets(GameSweet sweet1, GameSweet sweet2)
    {
        // 判断两个甜品是否可移动
        if (sweet1.CanMove() && sweet2.CanMove())
        {
            // 交换两个甜品在甜品数组中的位置
            sweets[sweet1.X, sweet1.Y] = sweet2;
            sweets[sweet2.X, sweet2.Y] = sweet1;

            // 判断交换后是否会有匹配的甜品
            if (MatchSweets(sweet1, sweet2.X, sweet2.Y) != null ||
                MatchSweets(sweet2, sweet1.X, sweet1.Y) != null)
            {

                // 记录甜品1的位置
                int tempX = sweet1.X;
                int tempY = sweet1.Y;

                // 执行甜品交换动画
                sweet1.MovedComponent.Move(sweet2.X, sweet2.Y, fillTimeMove);
                sweet2.MovedComponent.Move(tempX, tempY, fillTimeMove);
                // 清除所有匹配的甜品并填充
                ClearAllMatchedSweet();
                StartCoroutine(AllFill());
                // 重置按下的甜品和进入的甜品
                pressedSweet = null;
                enteredSweet = null;
            }
            else
            {
                print("甜品不匹配");
            }
            
        }
    }
    public List<GameSweet> MatchSweets(GameSweet sweet, int newX, int newY)
    {
        if (sweet.CanColor())
        {
            ColorSweet.ColorType color = sweet.ColoredComponent.Color;
            List<GameSweet> matchRowSweets = new List<GameSweet>();
            List<GameSweet> matchLineSweets = new List<GameSweet>();
            List<GameSweet> finishedMatchingSweets = new List<GameSweet>();
            //行匹配
            matchRowSweets.Add(sweet);
            //i=0代表往左，i=1代表往右
            for (int i = 0; i <= 1; i++)
            {
                for (int xDistance = 1; xDistance < xColumn; xDistance++)
                {
                    int x;
                    if (i == 0)
                    {
                        x = newX - xDistance;
                    }
                    else
                    {
                        x = newX + xDistance;
                    }
                    if (x < 0 || x >= xColumn)
                    {
                        break;
                    }
                    if (sweets[x, newY].CanColor() && sweets[x, newY].ColoredComponent.Color == color)
                    {
                        matchRowSweets.Add(sweets[x, newY]);
                    }
                    else
                    {
                        //print(xColumn);
                        break;
                    }
                }
            }
            if (matchRowSweets.Count >= 3)
            {
                for (int i = 0; i < matchRowSweets.Count; i++)
                {
                    finishedMatchingSweets.Add(matchRowSweets[i]);
                }
            }
            //列匹配
            matchLineSweets.Add(sweet);
            //i=0代表往左，i=1代表往右
            for (int i = 0; i <= 1; i++)
            {
                for (int yDistance = 1; yDistance < yRow; yDistance++)
                {
                    int y;
                    if (i == 0)
                    {
                        y = newY - yDistance;
                    }
                    else
                    {
                        y = newY + yDistance;
                    }
                    if (y < 0 || y >= yRow)
                    {
                        break;
                    }

                    if (sweets[newX, y].CanColor() && sweets[newX, y].ColoredComponent.Color == color)
                    {
                        matchLineSweets.Add(sweets[newX, y]);
                    }
                    else
                    {
                        break;
                    }
                }
            }
            if (matchLineSweets.Count >= 3)
            {
                for (int i = 0; i < matchLineSweets.Count; i++)
                {
                    finishedMatchingSweets.Add(matchLineSweets[i]);
                }
            }
            if (finishedMatchingSweets.Count >= 3)
            {
                return finishedMatchingSweets;
            }
        }
        return null;
    }
    //清除方法
    public bool ClearSweet(int x, int y)
    {

        if (sweets[x, y].CanClear() && !sweets[x, y].ClearedComponent.IsClearing)
        {
            sweets[x, y].ClearedComponent.Clear();
            CreateNewSweet(x, y, SweetsType.EMPTY);
            return true;
        }
        return false;
    }
    //定义一个方法，该方法用于清除所有匹配的甜品
    private bool ClearAllMatchedSweet()
    {
        bool needRefill = false; //一个标志，表示是否需要填充新的甜品

        //遍历整个游戏面板
        for (int y = 0; y < yRow; y++)
        {
            for (int x = 0; x < xColumn; x++)
            {
                //检查当前甜品是否可以清除
                if (sweets[x, y].CanClear())
                {
                    //如果可以清除，就获取和它匹配的甜品的列表
                    List<GameSweet> matchList = MatchSweets(sweets[x, y], x, y);

                    //如果获取到了匹配的甜品列表
                    if (matchList != null)
                    {
                        //遍历匹配的甜品列表，清除每一个匹配的甜品
                        for (int i = 0; i < matchList.Count; i++)
                        {
                            //如果成功清除一个甜品，就将 needRefill 设为 true
                            if (ClearSweet(matchList[i].X, matchList[i].Y))
                            {
                                needRefill = true;
                            }
                        }
                    }
                }
            }
        }
        //返回是否需要填充新的甜品的标志
        return needRefill;
    }
    public IEnumerator AllFill()
    {
        bool needRefill = true;

        while (needRefill)
        {
            //yield return new WaitForSeconds(fillTime);
            while (Fill())
            {
                yield return new WaitForSeconds(fillTime);
            }

            //清除所有我们已经匹配好的甜品
            needRefill = ClearAllMatchedSweet();
        }
    }
    public bool Fill()
    {
        bool filledNotFinished = false; // 判断本次填充是否完成

        // 垂直方向填充
        for (int y = yRow - 2; y >= 0; y--)
        {
            for (int x = 0; x < xColumn; x++)
            {
                GameSweet sweet = sweets[x, y]; // 得到当前元素位置的甜品对象

                if (sweet.CanMove()) // 如果无法移动，则无法往下填充
                {
                    GameSweet sweetBelow = sweets[x, y + 1];

                    if (sweetBelow.Type == SweetsType.EMPTY) // 垂直填充
                    {
                        Destroy(sweetBelow.gameObject);
                        sweet.MovedComponent.Move(x, y + 1, fillTime);
                        sweets[x, y + 1] = sweet;
                        CreateNewSweet(x, y, SweetsType.EMPTY);
                        filledNotFinished = true;
                    }
                }
            }
        }

        // 最上排的特殊情况，填充空缺的位置
        for (int x = 0; x < xColumn; x++)
        {
            GameSweet sweet = sweets[x, 0];

            if (sweet.Type == SweetsType.EMPTY)
            {
                GameObject newSweet = Instantiate(sweetPrefabDict[SweetsType.NORMAL], CorrectPositon(x, -1), Quaternion.identity);
                newSweet.transform.parent = transform;

                sweets[x, 0] = newSweet.GetComponent<GameSweet>();
                sweets[x, 0].Init(x, -1, this, SweetsType.NORMAL);
                sweets[x, 0].MovedComponent.Move(x, 0, fillTime);
                sweets[x, 0].ColoredComponent.SetColor((ColorSweet.ColorType)Random.Range(0, sweets[x, 0].ColoredComponent.NumColors));
                filledNotFinished = true;
            }
        }

        return filledNotFinished;
    }
}
