
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro; // 使用TextMeshPro组件，如果你使用的是TextMesh，记得更改此处和后面的代码
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class SLGrid : MonoBehaviour, IPointerClickHandler
{
    public int numberToShow = 0; // 这是要显示的数字，可以按需修改
    public Text myText; 
    private GameObject SLGridUp;
    public Sprite Mine;
    public Sprite Flag;
    private Sprite temp;
    private Image image;
    private Image UpImage;
    public int x = -1;
    public int y = -1;
    bool isReveal = false;
    public bool isFlag = false;
    private void Awake()
    {

    }
    // 使用 Start 方法获取TextMeshPro组件
    void Start()
    {
        image = GetComponent<Image>();
        SLGridUp = transform.Find("SLGridUp").gameObject;
        UpImage = transform.Find("SLGridUp").GetComponent<Image>();
    }

    // 当鼠标点击事件发生时，此函数将被调用
    public void OnPointerClick(PointerEventData eventData)
    {
        // 检查游戏是否已经结束，如果已经结束，则不处理任何点击事件
        if (SLisDeadorWin.Instance.isDeadorWin == false)
        {
            // 如果用户点击了鼠标右键
            if (eventData.button == PointerEventData.InputButton.Right)
            {
                // 如果当前格子没有标记为旗帜并且还有剩余的旗帜可以使用
                if (isFlag == false && SLisDeadorWin.Instance.flagNum > 0)
                {
                    // 使用一个旗帜，将旗帜数量减1
                    SLisDeadorWin.Instance.flagNum -= 1;

                    // 更新显示的旗帜数量
                    SLisDeadorWin.Instance.num.text = SLisDeadorWin.Instance.flagNum.ToString();

                    // 标记当前格子为旗帜
                    isFlag = true;

                    // 保存当前显示的图片，以便后续恢复
                    temp = UpImage.sprite;

                    // 显示旗帜图片
                    UpImage.sprite = Flag;

                    // 如果当前格子是地雷
                    if (numberToShow == -1)
                    {
                        // 增加正确标记的地雷数量
                        SLisDeadorWin.Instance.scores += 1;

                        // 如果所有的地雷都被正确标记，那么玩家胜利
                        if (SLisDeadorWin.Instance.scores == MapGenerator.Instance.mineCount)
                        {
                            // 显示胜利文本
                            MapGenerator.Instance.gameObject.transform.Find("Text").gameObject.SetActive(true);

                            // 标记游戏已经结束
                            SLisDeadorWin.Instance.isDeadorWin = true;

                            // 调用胜利处理函数
                            SLtimer.Instance.GameWon();
                        }
                    }
                }
                // 如果当前格子已经标记为旗帜
                else if (isFlag == true)
                {
                    // 取消旗帜，将旗帜数量加1
                    SLisDeadorWin.Instance.flagNum += 1;

                    // 更新显示的旗帜数量
                    SLisDeadorWin.Instance.num.text = SLisDeadorWin.Instance.flagNum.ToString();

                    // 取消标记当前格子为旗帜
                    isFlag = false;

                    // 恢复原来的图片
                    UpImage.sprite = temp;

                    // 如果当前格子是地雷
                    if (numberToShow == -1)
                    {
                        // 减少正确标记的地雷数量
                        SLisDeadorWin.Instance.scores -= 1;
                    }
                }
                else { }  // 其他情况，不做任何处理
            }

            // 如果用户点击了鼠标左键
            if (eventData.button == PointerEventData.InputButton.Left)
            {
                // 调用Reveal函数，揭示当前格子
                Reveal();
            }
        }
    }

    // 这是一个名为Reveal的公共函数。它可以从其他脚本中调用。
    public void Reveal()
    {
        // 检查布尔变量'isReveal'和'isFlag'是否都为false。
        // 'isReveal'可能表示某个元素（比如游戏中的一个方块）是否被揭示，
        // 而'isFlag'可能表示该元素是否被标记（例如在扫雷游戏中）。
        if (isReveal == false && isFlag == false)
        {
            // 'SLGridUp'似乎是一个当前激活的对象。这里将其设置为非激活状态（隐藏）。
            SLGridUp.SetActive(false);

            // 将'isReveal'设置为true，表示此元素现已被揭示。
            isReveal = true;

            // 更新元素上的文字，显示存储在'numberToShow'中的数字。
            // 'myText'似乎是元素上的一个文本对象，而'numberToShow'可能是附近的地雷数量（在扫雷类游戏中）。
            myText.text = numberToShow.ToString();

            // 检查'numberToShow'是否等于-1。
            // 如果是，将'image'的精灵更改为'Mine'（可能表示存在地雷），
            // 并将'myText'上的文字设置为空字符串。
            // 同时，将'SLisDeadorWin'实例的'isDeadorWin'属性设置为true，
            // 这可能表示游戏结束（玩家已经输了或赢了）。
            if (numberToShow == -1)
            {
                image.sprite = Mine;
                myText.text = "";
                SLisDeadorWin.Instance.isDeadorWin = true;
            }
            MapGenerator.Instance.clickedTiles += 1;
            if (MapGenerator.Instance.clickedTiles == (MapGenerator.Instance.GetTotalTiles() - MapGenerator.Instance.mineCount))
            {
                // 显示胜利文本
                MapGenerator.Instance.gameObject.transform.Find("Text").gameObject.SetActive(true);

                // 标记游戏已经结束
                SLisDeadorWin.Instance.isDeadorWin = true;

                // 调用胜利处理函数
                SLtimer.Instance.GameWon();
            }
            // 检查'numberToShow'是否等于0。
            // 如果是，将'myText'上的文字设置为空字符串，
            // 并向层级结构上方发送一个消息，使用'sendAutoReveal'方法和'vector2'作为参数。
            // 如果当前元素附近没有地雷，这可能用于自动揭示网格中的相邻元素。
            if (numberToShow == 0)
            {
                myText.text = "";
                Vector2 vector2 = new Vector2(x, y);
                SendMessageUpwards("sendAutoReveal", vector2);
            }
        }
    }

    public void AutoReveal(Vector2 vector2)
    {
        if (isReveal == false)
        {
            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    // 跳过中心格子 (x, y)
                    if (i == 0 && j == 0)
                        continue;

                    int nx = x + i;
                    int ny = y + j;

                    // 检查nx和ny是否在数组的边界内，这里假设你的grid是一个二维数组，它的大小是width x height
                    if (nx >= 0 && nx < MapGenerator.Instance.mapWidth && ny >= 0 && ny < MapGenerator.Instance.mapHeight)
                    {
                        // 判断周围的格子是否是(v2x, v2y)
                        if (nx == vector2.x && ny == vector2.y)
                            Reveal();
                    }
                }
            }
            // print("");
        }
    }
}
