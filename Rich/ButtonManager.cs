using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ButtonManager : MonoBehaviour
{
    public List<RichPlayer> players; // 包含所有玩家的列表
    public int currentPlayerIndex = 0; // 记录当前正在行动的玩家的索引
    RichPlayer currentPlayer;
    public Text nameText; 
    public Text moneyText; // 显示金钱数的Text对象
    public Text DianJuanText;
    public static ButtonManager instance; // 静态的ButtonManager实例
    public CameraFollow cameraF;  // 摄像头控制器
    private void Awake()
    {
        instance = this;
        cameraF.target = players[currentPlayerIndex].transform;
    }
    // 这个方法用于处理玩家点击GO按钮的事件  
    private void Start()
    {
        currentPlayer = players[currentPlayerIndex];
        // 设置摄像头的目标为当前玩家  
        nameText.text = currentPlayer.RicherName;
        moneyText.text = currentPlayer.money.ToString();
        DianJuanText.text = currentPlayer.DianJuanNum.ToString();
    }
    public void OnGoButtonClicked()
    {
        // 获取当前的玩家
        currentPlayer = players[currentPlayerIndex];
        // 让当前的玩家投掷骰子并移动
        currentPlayer.ThrowDice();
    }   
    public void OnBuyClicked()
    {
        currentPlayer.BuyLand();
    }
    public void OnDeclineBuyClicked()
    {
        currentPlayer.DeclineBuyLand();
    }
    public void OnBuildClicked(int i)
    {
        currentPlayer.BuildHouse(i);
    }
    public void OnDeclineBuildClicked()
    {
        currentPlayer.DeclineBuildHouse();
    }
    public void OnUpgradeHouseClicked()
    {
        currentPlayer.UpgradeHouse();
    }
    public void OnDeclineUpgradeClicked()
    {
        currentPlayer.DeclineUpgradeHouse();
    }
}
