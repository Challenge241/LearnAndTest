using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance; // ��̬��ʵ�������������ű�����ͨ�������������ScoreManager
    public int score = 0; // ��ǰ�ĵ÷�
    public Text scoreText; // ��ʾ�÷ֵ�Text���
    public float onEnableTimeRemaining = 10f;
    private float timeRemaining = 10f; // ʣ��ʱ��
    public Text timeText; // ��ʾʣ��ʱ���Text���
    public GameObject qiQiuTile;
    private bool isEnd=false;
    public RichPlayer richPlayer;
    private void Awake()
    {
        // ȷ��ֻ��һ��ScoreManagerʵ��
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
            // ����������ʱ����Ϸ�������
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

