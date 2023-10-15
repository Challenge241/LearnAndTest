
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    public Text sunNumText;
    public Text CoinNumText;
    public int coinNum = 0;
    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        InitUI();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void InitUI()
    {
        sunNumText.text = GameManager.instance.sunNum.ToString();
        CoinNumText.text = coinNum.ToString();
    }
    public void UpdateUI()
    {
        sunNumText.text = GameManager.instance.sunNum.ToString();
       CoinNumText.text = coinNum.ToString();
    }
    public void ChangeCoinNum(int changeNum)
    {
        //Debug.Log("ChangeSunNum");
        coinNum += changeNum;
        //Debug.Log(sunNum);
        if (coinNum <= 0)
        {
            coinNum = 0;
        }
        //wait to do 阳光数值改变，通知卡片压黑等
        UIManager.instance.UpdateUI();
    }
}
