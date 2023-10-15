using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ButtonManager : MonoBehaviour
{
    public List<RichPlayer> players; // ����������ҵ��б�
    public int currentPlayerIndex = 0; // ��¼��ǰ�����ж�����ҵ�����
    RichPlayer currentPlayer;
    public Text nameText; 
    public Text moneyText; // ��ʾ��Ǯ����Text����
    public Text DianJuanText;
    public static ButtonManager instance; // ��̬��ButtonManagerʵ��
    public CameraFollow cameraF;  // ����ͷ������
    private void Awake()
    {
        instance = this;
        cameraF.target = players[currentPlayerIndex].transform;
    }
    // ����������ڴ�����ҵ��GO��ť���¼�  
    private void Start()
    {
        currentPlayer = players[currentPlayerIndex];
        // ��������ͷ��Ŀ��Ϊ��ǰ���  
        nameText.text = currentPlayer.RicherName;
        moneyText.text = currentPlayer.money.ToString();
        DianJuanText.text = currentPlayer.DianJuanNum.ToString();
    }
    public void OnGoButtonClicked()
    {
        // ��ȡ��ǰ�����
        currentPlayer = players[currentPlayerIndex];
        // �õ�ǰ�����Ͷ�����Ӳ��ƶ�
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
