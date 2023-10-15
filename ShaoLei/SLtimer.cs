using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SLtimer : MonoBehaviour
{
    private float timeElapsed = 0f;  // 存储已经过去的时间
    public Text timerText;  // UI Text组件，用来显示计时器
    private static SLtimer instance;
    public static SLtimer Instance
    {
        get
        {
            return instance;
        }

        set
        {
            instance = value;
        }
    }
    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        
    }

    void Update()
    {
        // 增加已经过去的时间
        timeElapsed += Time.deltaTime;

        // 计算分钟和秒数
        int minutes = (int)timeElapsed / 60;
        int seconds = (int)timeElapsed % 60;

        // 更新UI Text组件
        this.timerText.text = $"{minutes.ToString("00")}:{seconds.ToString("00")}";
        print(seconds);
    }
    public void GameWon()
    {
        // 加载已经保存的时间
        string jsonString = PlayerPrefs.GetString("Times", "");
        TimeList timeList = JsonUtility.FromJson<TimeList>(jsonString);
        if (timeList == null)
        {
            timeList = new TimeList();
            timeList.times = new List<float>();
        }

        // 将新时间添加到列表中
        timeList.times.Add(timeElapsed);

        // 如果有超过10个时间，那么删除最慢的时间
        if (timeList.times.Count > 10)
        {
            timeList.times.Sort();
            timeList.times.RemoveAt(timeList.times.Count - 1);
        }

        // 保存时间
        jsonString = JsonUtility.ToJson(timeList);
        PlayerPrefs.SetString("Times", jsonString);
        print("时间保存完毕");
        foreach (float time in timeList.times)
        {
            int minutes = (int)time / 60;
            int seconds = (int)time % 60;
            Debug.Log($"{minutes.ToString("00")}:{seconds.ToString("00")}");
        }
    }
}
