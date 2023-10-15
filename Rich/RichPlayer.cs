using System.Collections;
using UnityEngine;
using UnityEngine.UI;


// 这是一个管理玩家控制和行为的类
public class RichPlayer : MonoBehaviour
{
    public Transform[] tiles; // 这个数组存储了游戏板上所有的格子的Transform
    public int currentTileIndex = 0; // 这个变量记录了玩家当前所在的格子在数组中的索引
    public float speed = 2f; // 这个变量决定了玩家的移动速度
    public int diceValue;
    public int money; // 玩家的钱
    public int DianJuanNum=100;
    public GameObject buyLandDialog; // 购买土地的对话框
    public GameObject GO;
    public GameObject rollingAni;
    public GameObject[] rollResults;
    public UnityEngine.UI.Text feedbackText;
    public GameObject textBG;
    public UnityEngine.UI.Text LandmoneyText;
    public string RicherName;
    public GameObject buildHouseDialog;
    public GameObject upgradeHouseDialog;
    void Start()
    {
        GameObject Roads = GameObject.Find("Roads"); // 找到名为 "Roads" 的物体
        tiles = new Transform[Roads.transform.childCount]; // 初始化 tiles 数组

        for (int i = 0; i < Roads.transform.childCount; i++)
        {
            tiles[i] = Roads.transform.GetChild(i); // 获取每个子物体的 Transform，并存入 tiles 数组
        }
    }


    // 当玩家投掷骰子时调用这个方法
    public void ThrowDice()
    {
        diceValue = Random.Range(1, 7); // 生成一个1到6的随机数，代表骰子的结果
        //print(diceValue);
        rollingAni.GetComponent<SpriteRenderer>().enabled=false;
        rollResults[diceValue - 1].SetActive(true);
        MovePlayer(diceValue); // 根据骰子的结果移动玩家
    }

    // 这个方法根据指定的格子数移动玩家
    private void MovePlayer(int numTiles)
    {
        // 计算目标格子的索引。如果目标格子超过了格子数组的长度，则回到第一格
        int targetTileIndex = (currentTileIndex + numTiles) % tiles.Length;
        // 开始一个协程，把玩家移动到目标格子
        StartCoroutine(MoveToTargetTile(targetTileIndex));
    }

    // 这是一个协程，用于移动玩家到目标格子

    private IEnumerator MoveToTargetTile(int targetTileIndex)
    {
        GO.SetActive(false);
        // 当玩家还没有到达目标格子时，持续移动玩家
        while (currentTileIndex != targetTileIndex)
        {
            // 将当前格子索引加一，表示玩家要移动到下一个格子
            currentTileIndex = (currentTileIndex + 1) % tiles.Length;
            // 获取目标格子的位置
            Vector3 targetPosition = tiles[currentTileIndex].position;
            // 当玩家还没有到达目标格子时，每一帧都向目标格子移动一定的距离
            while (transform.position != targetPosition)
            {
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
                yield return null; // 等待下一帧
            }
        }

        richRoad tile = tiles[currentTileIndex].GetComponent<richRoad>();

        if (tile.land != null)
        {
            if (tile.land.owner == null)
            {
                // 如果这个格子上有土地，并且土地没有被购买，显示购买土地的对话框
                buyLandDialog.SetActive(true);
                LandmoneyText.text = "BuyThisLandNeed" + tile.land.price;
            }
            else if (tile.land.owner != this)
            {
                // 如果这个格子上有土地，并且土地被别人购买，向别人交过路费
                int tollFee = CalculateTollFee(tile.land); // CalculateTollFee方法需要你自己实现，计算过路费的金额
                if (money >= tollFee)
                {
                    money -= tollFee;
                    textBG.SetActive(true);
                    feedbackText.text = RicherName + "Pay" + tollFee + "RunFee"; // 修改文本框中的文本
                    tile.land.owner.money += tollFee;
                    ButtonManager.instance.moneyText.text=money.ToString();
                    yield return new WaitForSeconds(1.0f);
                    textBG.SetActive(false);

                    turnEnd();
                }
                else
                {
                    textBG.SetActive(true);
                    feedbackText.text = RicherName + "have not enough money to pay for tolls"; // 修改文本框中的文本
                    // 如果玩家的钱不够支付过路费，你可能需要做一些处理，例如破产或者卖掉一些土地
                    // 这个逻辑需要你根据你的游戏规则来实现
                    yield return new WaitForSeconds(2.0f);
                    textBG.SetActive(false);

                    turnEnd();
                }
            }
            else
            {//走到了自己的地块上

                if (tile.land.house == null)
                {
                    // 如果这个地块上没有房子，显示建造房子的对话框
                    buildHouseDialog.SetActive(true);
                }
                else if (tile.land.level < tile.land.maxLevel)
                {
                    upgradeHouseDialog.SetActive(true);
                    // 根据名字查找子对象moneyText并赋值
                    Transform moneyTextTransform = upgradeHouseDialog.transform.Find("MoneyText");
                    int a=tile.land.type;
                    // 根据房屋的类型，给moneyText子对象的文本赋值
                    Text moneyText = moneyTextTransform.GetComponent<Text>();
                    if (moneyText != null)
                    {
                        moneyText.text = tile.land.buildCost[a].ToString();
                    }
                }
                else
                {
                    turnEnd();
                }
                
            }
        }
        else if(tile.TitleDianJuan!=0)
        {
            //print(tile.TitleDianJuan);
            DianJuanNum = DianJuanNum +tile.TitleDianJuan;
            ButtonManager.instance.DianJuanText.text = DianJuanNum.ToString();
            yield return new WaitForSeconds(1.0f);
            turnEnd();
        }else if (tile.qiQiu!=null)
        {
            //print("tile.qiQiu");
            tile.qiQiu.GameStart(this);
            turnEnd();
        }
    }
    private int CalculateTollFee(RichLand land)
    {
        if (land.type == -1)
        {
            return (int)(land.price * 0.15f); // Calculate 15% of the land's price
        }
        else if (land.type == 0)
        {
            return (int)(land.price *land.level* 1.5f); // Calculate 150% of the land's price
        }
        else if (land.type == 1)
        {
            return land.price * land.level; // Return the land's price
        }
        else if (land.type == 2)
        {
            return (int)(land.price * land.level * 0.5f); // Calculate 50% of the land's price
        }
        else
        {
            throw new System.ArgumentException("Invalid land type.");
        }
    }



    public void BuyLand()
    {
        // 玩家选择了购买土地
        richRoad tile = tiles[currentTileIndex].GetComponent<richRoad>();
        if (money >= tile.land.price)
        {
            // 如果玩家的钱足够，购买这块土地
            tile.land.owner = this;
            money -= tile.land.price;

            // 获取土地上的SpriteRenderer组件并修改颜色为玩家颜色
            SpriteRenderer landSpriteRenderer = tile.land.GetComponent<SpriteRenderer>();
            if (landSpriteRenderer != null)
            {
                SpriteRenderer playerSpriteRenderer = this.GetComponent<SpriteRenderer>();
                if (playerSpriteRenderer != null)
                {
                    landSpriteRenderer.color = playerSpriteRenderer.color;
                }
            }
        }
        else
        {
            //feedbackText.text = "钱不够";
        }
        // 关闭购买土地的对话框
        buyLandDialog.SetActive(false);

        turnEnd();
    }


    public void DeclineBuyLand()
    {
        // 玩家选择了不购买土地，关闭购买土地的对话框,开启行动框
        buyLandDialog.SetActive(false);

        turnEnd();
    }
    private void turnEnd()//一轮结束，新的一轮开始
    {
        GO.SetActive(true);
        rollingAni.GetComponent<SpriteRenderer>().enabled = true;
        rollResults[diceValue - 1].SetActive(false);
        ButtonManager.instance.currentPlayerIndex = (ButtonManager.instance.currentPlayerIndex + 1) % ButtonManager.instance.players.Count;
        ButtonManager.instance.nameText.text = ButtonManager.instance.players[ButtonManager.instance.currentPlayerIndex].RicherName;
        ButtonManager.instance.moneyText.text = ButtonManager.instance.players[ButtonManager.instance.currentPlayerIndex].money.ToString();
        ButtonManager.instance.DianJuanText.text = ButtonManager.instance.players[ButtonManager.instance.currentPlayerIndex].DianJuanNum.ToString();
        // 设置摄像头的目标为当前玩家
        ButtonManager.instance.cameraF.target = ButtonManager.instance.players[ButtonManager.instance.currentPlayerIndex].transform;
    }
    public void BuildHouse(int i)
    {
        // 获取当前地块的信息
        richRoad tile = tiles[currentTileIndex].GetComponent<richRoad>();
        //print("PlayerBuildHouse" + i);
        // 检查玩家是否有足够的钱来建造房子
        if (money >= tile.land.buildCost[i])
        {
            // 扣除建造房子的费用
            money -= tile.land.buildCost[i];

            // 在地块上建造房子
            tile.land.BuildHouse(i);

            // 更新UI显示
            //HousemoneyText.text = "已经建造房子。剩余金钱：" + money;
        }
        else
        {
            // 如果玩家没有足够的钱，显示一条消息
            //HousemoneyText.text = "你没有足够的钱来建造房子。";
        }

        // 关闭建造房子的对话框
        buildHouseDialog.SetActive(false);
        turnEnd();
    }
    public void DeclineBuildHouse()
    {
        buildHouseDialog.SetActive(false);
        turnEnd();
    }
    public void UpgradeHouse()
    {
        richRoad tile = tiles[currentTileIndex].GetComponent<richRoad>();
        int a = tile.land.type;
        if (money >= tile.land.buildCost[a])
        {
            money -= tile.land.buildCost[a];
            tile.land.UpgradeHouse(a);
        }
        else
        {
            
        }
        upgradeHouseDialog.SetActive(false);
        turnEnd();
    }
    public void DeclineUpgradeHouse()
    {
        upgradeHouseDialog.SetActive(false);
        turnEnd();
    }
}
