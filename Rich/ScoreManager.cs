using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance; // 静态的实例变量，其他脚本可以通过这个变量访问ScoreManager
    public int score = 0; // 当前的得分
    public Text scoreText; // 显示得分的Text组件
    public float onEnableTimeRemaining = 10f;
    private float timeRemaining = 10f; // 剩余时间
    public Text timeText; // 显示剩余时间的Text组件
    public GameObject qiQiuTile;
    private bool isEnd=false;
    public RichPlayer richPlayer;
    private void Awake()
    {
        // 确保只有一个ScoreManager实例
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void OnEnable()
    {
        score = 0;
        scoreText.text = "Score: " + score;
        isEnd = false;
        timeRemaining = onEnableTimeRemaining;
    }
    private void Update()
    {
        if (timeRemaining>0 && isEnd == false)
        {
            // 新增：倒计时和游戏结束检查
            timeRemaining -= Time.deltaTime;
            timeText.text = "Time: " + Mathf.Max(timeRemaining, 0).ToString("0");
        }

        if (timeRemaining <= 0&&isEnd==false)
        {
            richPlayer.DianJuanNum = richPlayer.DianJuanNum + score;
            qiQiuTile.GetComponent<EnterQiQiu>().GameEnd();
            isEnd = true;
        }
    }
    public void IncreaseScore(int amount)
    {
        score += amount;
        scoreText.text = "Score: " + score;
    }
}

