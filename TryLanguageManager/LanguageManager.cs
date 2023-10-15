using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI; // 引入UI命名空间以使用Dropdown组件
using System;

public class LanguageManager : MonoBehaviour
{
    // 定义一个枚举类型来表示支持的语言
    public enum Language
    {
        English,
        Chinese
    }
    public Dropdown dropdown; // 您可以在Inspector中将场景中的Dropdown对象拖到这里

    // 定义一个字典来存储加载的语言文件中的文本，键是文本的键，值是对应的本地化文本
    private Dictionary<string, string> localizedText;
    // 定义一个静态事件，当语言改变时触发
    public static event Action OnLanguageChanged;   
    // 定义一个静态实例，使这个类成为一个单例
    private static LanguageManager instance;

    // 定义一个公共静态属性，外部可以通过这个属性获取这个类的实例
    public static LanguageManager Instance
    {
        get
        {
            // 如果实例为空，则创建一个新的GameObject，并添加LanguageManager组件
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
    // 定义一个公共方法来加载语言文件
    public void LoadLanguage(Language language)
    {

        // 初始化localizedText字典
        localizedText = new Dictionary<string, string>();
        // 根据选择的语言确定加载哪个文件
        string fileName = language == Language.English ? "English" : "Chinese";

        // 从Resources文件夹加载文本文件
        TextAsset textAsset = Resources.Load<TextAsset>(fileName);

        // 按行分割文本文件内容，并遍历每一行
        string[] lines = textAsset.text.Split('\n');
        foreach (var line in lines)
        {
            // 将每一行按'='分割，左边是键，右边是值
            string[] keyValue = line.Split('=');
            if (keyValue.Length == 2)
                // 添加到localizedText字典中
                localizedText.Add(keyValue[0], keyValue[1]);
        }
        // 在语言加载后触发事件
        OnLanguageChanged?.Invoke();
    }

    // 定义一个公共方法来根据键获取本地化文本
    public string GetLocalizedText(string key)
    {
        // 如果字典中包含这个键，返回对应的值，否则返回键本身
        if (localizedText.ContainsKey(key))
        {  return localizedText[key]; }
        return key;
    }
    void OnDropdownValueChanged(int index)
    {
        // 获取选中项的文本值
        string selectedText = dropdown.options[index].text;

        // 根据选中项的文本值加载相应的语言
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
            // 如果有其他选项，您可以在这里处理
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
        // 添加监听器，当选项变化时调用OnDropdownValueChanged方法
        dropdown.onValueChanged.AddListener(OnDropdownValueChanged);
    }
}
