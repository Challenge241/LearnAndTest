using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI; // ����UI�����ռ���ʹ��Dropdown���
using System;

public class LanguageManager : MonoBehaviour
{
    // ����һ��ö����������ʾ֧�ֵ�����
    public enum Language
    {
        English,
        Chinese
    }
    public Dropdown dropdown; // ��������Inspector�н������е�Dropdown�����ϵ�����

    // ����һ���ֵ����洢���ص������ļ��е��ı��������ı��ļ���ֵ�Ƕ�Ӧ�ı��ػ��ı�
    private Dictionary<string, string> localizedText;
    // ����һ����̬�¼��������Ըı�ʱ����
    public static event Action OnLanguageChanged;   
    // ����һ����̬ʵ����ʹ������Ϊһ������
    private static LanguageManager instance;

    // ����һ��������̬���ԣ��ⲿ����ͨ��������Ի�ȡ������ʵ��
    public static LanguageManager Instance
    {
        get
        {
            // ���ʵ��Ϊ�գ��򴴽�һ���µ�GameObject�������LanguageManager���
            if (instance == null)
            {
                instance = new GameObject("LanguageManager").AddComponent<LanguageManager>();
            }
            return instance;
        }
    }
    private void Awake()
    {
        instance = this;
    }
    // ����һ���������������������ļ�
    public void LoadLanguage(Language language)
    {

        // ��ʼ��localizedText�ֵ�
        localizedText = new Dictionary<string, string>();
        // ����ѡ�������ȷ�������ĸ��ļ�
        string fileName = language == Language.English ? "English" : "Chinese";

        // ��Resources�ļ��м����ı��ļ�
        TextAsset textAsset = Resources.Load<TextAsset>(fileName);

        // ���зָ��ı��ļ����ݣ�������ÿһ��
        string[] lines = textAsset.text.Split('\n');
        foreach (var line in lines)
        {
            // ��ÿһ�а�'='�ָ����Ǽ����ұ���ֵ
            string[] keyValue = line.Split('=');
            if (keyValue.Length == 2)
                // ��ӵ�localizedText�ֵ���
                localizedText.Add(keyValue[0], keyValue[1]);
        }
        // �����Լ��غ󴥷��¼�
        OnLanguageChanged?.Invoke();
    }

    // ����һ���������������ݼ���ȡ���ػ��ı�
    public string GetLocalizedText(string key)
    {
        // ����ֵ��а�������������ض�Ӧ��ֵ�����򷵻ؼ�����
        if (localizedText.ContainsKey(key))
        {  return localizedText[key]; }
        return key;
    }
    void OnDropdownValueChanged(int index)
    {
        // ��ȡѡ������ı�ֵ
        string selectedText = dropdown.options[index].text;

        // ����ѡ������ı�ֵ������Ӧ������
        if (selectedText == "Chinese")
        {
            ChangeToChinese();
        }
        else if (selectedText == "English")
        {
            ChangeToEnglish();
        }
        else
        {
            // ���������ѡ������������ﴦ��
            Debug.LogWarning("Unrecognized option selected: " + selectedText);
        }
    }
    void ChangeToChinese()
    {
        LanguageManager.Instance.LoadLanguage(LanguageManager.Language.Chinese);
    }
    void ChangeToEnglish()
    {
        LanguageManager.Instance.LoadLanguage(LanguageManager.Language.English);
    }
    void ShowWelcomeMessage()
    {
        string welcomeMessageKey = "NiHao";
        string localizedWelcomeMessage = LanguageManager.Instance.GetLocalizedText(welcomeMessageKey);
        Debug.Log(localizedWelcomeMessage);
    }
    void Start()
    {
        LanguageManager.Instance.LoadLanguage(LanguageManager.Language.Chinese);
        // ��Ӽ���������ѡ��仯ʱ����OnDropdownValueChanged����
        dropdown.onValueChanged.AddListener(OnDropdownValueChanged);
    }
}
