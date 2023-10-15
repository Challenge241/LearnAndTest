using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SLtimer : MonoBehaviour
{
    private float timeElapsed = 0f;  // �洢�Ѿ���ȥ��ʱ��
    public Text timerText;  // UI Text�����������ʾ��ʱ��
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
        // �����Ѿ���ȥ��ʱ��
        timeElapsed += Time.deltaTime;

        // ������Ӻ�����
        int minutes = (int)timeElapsed / 60;
        int seconds = (int)timeElapsed % 60;

        // ����UI Text���
        this.timerText.text = $"{minutes.ToString("00")}:{seconds.ToString("00")}";
        print(seconds);
    }
    public void GameWon()
    {
        // �����Ѿ������ʱ��
        string jsonString = PlayerPrefs.GetString("Times", "");
        TimeList timeList = JsonUtility.FromJson<TimeList>(jsonString);
        if (timeList == null)
        {
            timeList = new TimeList();
            timeList.times = new List<float>();
        }

        // ����ʱ����ӵ��б���
        timeList.times.Add(timeElapsed);

        // ����г���10��ʱ�䣬��ôɾ��������ʱ��
        if (timeList.times.Count > 10)
        {
            timeList.times.Sort();
            timeList.times.RemoveAt(timeList.times.Count - 1);
        }

        // ����ʱ��
        jsonString = JsonUtility.ToJson(timeList);
        PlayerPrefs.SetString("Times", jsonString);
        print("ʱ�䱣�����");
        foreach (float time in timeList.times)
        {
            int minutes = (int)time / 60;
            int seconds = (int)time % 60;
            Debug.Log($"{minutes.ToString("00")}:{seconds.ToString("00")}");
        }
    }
}
