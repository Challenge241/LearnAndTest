using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System;

public class GreatJsonSaveManager : MonoBehaviour
{
    #region strList
    // 创建一个字符串类型的List//保存存档名的列表
    public List<string> stringList = new List<string>();
    // 创建一个DateTime类型的List，用于存储存档时间
    List<DateTime> saveDates = new List<DateTime>();
    public void ListAdd(string name, DateTime dateTime)
    {
        int index = stringList.IndexOf(name); // 检查name是否已存在于stringList中，并获取其索引
        if (index != -1) // 如果存在
        {
            stringList.RemoveAt(index); // 删除stringList中对应索引的元素
            saveDates.RemoveAt(index); // 删除saveDates中对应索引的元素
        }

        // 在两个列表的末尾分别添加name和dateTime
        stringList.Add(name);
        saveDates.Add(dateTime);
    }

    public void saveList()
    {
        // 将stringList转换为逗号分隔的字符串
        string stringListStr = string.Join(",", stringList);
        PlayerPrefs.SetString("stringList", stringListStr);

        // 将saveDates转换为逗号分隔的字符串
        List<string> dateStringList = saveDates.ConvertAll(date => date.ToString("o"));
        string saveDatesStr = string.Join(",", dateStringList);
        PlayerPrefs.SetString("saveDates", saveDatesStr);

        PlayerPrefs.Save();
    }
    public void loadList()
    {
        // 从PlayerPrefs中加载字符串并初始化stringList
        string stringListStr = PlayerPrefs.GetString("stringList", "");
        if (!string.IsNullOrEmpty(stringListStr))
        {
            stringList = new List<string>(stringListStr.Split(','));
            //print(stringList);

        }

        // 从PlayerPrefs中加载字符串并初始化saveDates
        string saveDatesStr = PlayerPrefs.GetString("saveDates", "");
        if (!string.IsNullOrEmpty(saveDatesStr))
        {
            string[] dateStringArray = saveDatesStr.Split(',');
            saveDates = new List<DateTime>();
            foreach (string dateString in dateStringArray)
            {
                if (DateTime.TryParse(dateString, out DateTime parsedDate))
                {
                    print(parsedDate);
                    saveDates.Add(parsedDate);
                }
            }
        }
    }
    #endregion
    #region OpenWindow
    private List<GameObject> createdButtons = new List<GameObject>(); // 存储已创建按钮的列表
    public GameObject gridContent; // Grid GameObject，用于存放创建的按钮
    public GameObject buttonPrefab; // Button预制体
    public void OpenWindowSave()//需要完善
    {
        // 销毁之前创建的所有按钮
        foreach (var button in createdButtons)
        {
            Destroy(button);
        }
        createdButtons.Clear(); // 清空列表

        // 根据存档名和存档时间创建新的按钮
        //for (int i = 0; i < stringList.Count && i < saveDates.Count; i++)
        for (int i = stringList.Count - 1; i >= 0; i--)
        {
            // 在gridContent下实例化新的按钮
            var button = Instantiate(buttonPrefab, gridContent.transform);
            // 设置按钮的文本显示为存档名和存档时间
            button.GetComponentInChildren<Text>().text = $"{stringList[i]} - {saveDates[i].ToString()}";

            // 获取按钮的Button组件，并为其添加点击事件的监听器
            Button btnComponent = button.GetComponent<Button>();
            string saveName = stringList[i]; // 获取当前存档名
            // 为按钮的点击事件添加监听器，当点击时，会调用SaveAllChildObjects方法，并传递存档名为参数
            //btnComponent.onClick.AddListener(() => SaveAllChildObjects(saveName));
            // 为按钮的点击事件添加监听器，用于设置InputField的文本
            btnComponent.onClick.AddListener(() => SetInputFieldText(saveName));
            // 为删除按钮添加监听器
            var xButton = button.transform.Find("X_Button").GetComponent<Button>();
            xButton.onClick.AddListener(() => DeleteSaveFileAndRemoveFromLists(saveName));

            // 将新创建的按钮添加到列表中
            createdButtons.Add(button);
        }
    }
    // 设置InputField的文本
    private void SetInputFieldText(string text)
    {
        inputField.text = text;
    }
    public void OpenWindowLoad()//读档要监听，存档不用，存档直接点保存就行了，点存档是为了让文本输入框的文本与其一致而非存档
    {
        // 销毁之前创建的所有按钮
        foreach (var button in createdButtons)
        {
            Destroy(button);
        }
        createdButtons.Clear(); // 清空列表
        // 根据存档名和存档时间创建新的按钮
        //for (int i = 0; i < stringList.Count && i < saveDates.Count; i++)
        for (int i = stringList.Count - 1; i >= 0; i--)
        {
            // 在gridContent下实例化新的按钮
            var button = Instantiate(buttonPrefab, gridContent.transform);
            // 设置按钮的文本显示为存档名和存档时间
            button.GetComponentInChildren<Text>().text = $"{stringList[i]} - {saveDates[i].ToString()}";

            // 获取按钮的Button组件，并为其添加点击事件的监听器
            Button btnComponent = button.GetComponent<Button>();
            string saveName = stringList[i]; // 获取当前存档名
            // 为按钮的点击事件添加监听器，当点击时，会调用SaveAllChildObjects方法，并传递存档名为参数
            btnComponent.onClick.AddListener(() => LoadGameObjects(saveName));
            // 为删除按钮添加监听器
            var xButton = button.transform.Find("X_Button").GetComponent<Button>();
            xButton.onClick.AddListener(() => DeleteSaveFileAndRemoveFromLists(saveName));
            // 将新创建的按钮添加到列表中
            createdButtons.Add(button);
        }
    }
    void DeleteSaveFileAndRemoveFromLists(string name)
    {
        // 找到 name 对应的索引
        int index = stringList.IndexOf(name);
        if (index != -1)
        {
            // 删除对应的json文件
            string filePath = GetSaveFilePath(name);
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }

            // 从两个列表中移除元素
            stringList.RemoveAt(index);
            saveDates.RemoveAt(index);
            saveList();//保存删除结果
            // 如果需要，可以重新刷新按钮列表
            OpenWindowLoad(); // 或者 OpenWindowSave();
        }
        else
        {
            Debug.LogError("DeleteError");
        }
    }

    public void CloseWindow()
    {
        // 关闭窗口时销毁所有创建的按钮
        foreach (var button in createdButtons)
        {
            Destroy(button);
        }
        createdButtons.Clear(); // 清空列表
    }

    public InputField inputField; // 引用InputField组件
    public void OnSaveButtonClick()
    {
        // 获取InputField中的文本
        string inputText = inputField.text;

        // 调用SaveAllChildObjects函数，并将文本作为参数
        SaveAllChildObjects(inputText);
        OpenWindowSave();//更新窗口
    }
    #endregion
    // 保存游戏对象数据的列表
    public List<GameObjectDataJ> gameObjectsData = new List<GameObjectDataJ>();

    // 显示信息的GameObject
    public GameObject InformationShow;

    // 显示文本信息的Text组件
    public Text text_information;

    // 生成游戏对象的Transform
    Transform generatorTransform;
    #region Instance
    // 静态实例
    public static GreatJsonSaveManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            // 如果实例不存在，则将当前对象设为实例
            Instance = this;

            // 确保在加载新场景时不会销毁这个对象
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            // 如果实例已存在且不是当前实例，则销毁当前对象
            Destroy(gameObject);
        }
    }
    #endregion
    // 在Start方法中，找到名为"Gerenator"的GameObject，并获取其Transform组件
    private void Start()
    {
        generatorTransform = GameObject.Find("Gerenator").transform;
        //saveList();//先保存再读取等于清空存档，因为初始时没信息
        loadList();
    }

    /*
    private string GetSaveFilePath(string name)
    {//这种情况下存档会直接保存在My project文件夹下
        return "saveFile_" + name + ".json";
    }*/
    
    private string GetSaveFilePath(string name)
    {//保存到C:\Users\<username>\AppData\LocalLow\<CompanyName>\<ProductName>
        string fileName = "saveFile_" + name + ".json";
        print(Path.Combine(Application.persistentDataPath, fileName));
        return Path.Combine(Application.persistentDataPath, fileName);
    }

    // 将gameObjectsData列表保存为JSON文件
    void SaveGameObjects(string name)
    {
        string json = JsonUtility.ToJson(new Wrapper { gameObjectsDataJ = gameObjectsData }, true);
        System.IO.File.WriteAllText(GetSaveFilePath(name), json);
    }

    // 保存generatorTransform下所有的子对象
    public void SaveAllChildObjects(string name)
    {
        //给列表添加元素
        ListAdd(name,DateTime.Now);
        saveList();
        Transform parentTransform = generatorTransform;
        // 遍历generatorTransform的所有子对象，并将其数据添加到gameObjectsData列表中
        foreach (Transform childTransform in parentTransform)
        {
            GameObjectDataJ data = CreateGameObjectData(childTransform.gameObject);
            gameObjectsData.Add(data);
        }

        // 保存gameObjectsData列表为JSON文件
        SaveGameObjects(name);

        // 显示存档成功的信息
        InformationShow.SetActive(true);
        text_information.text = "存档成功";
    }

    // 从一个GameObject创建一个GameObjectDataJ对象
    public GameObjectDataJ CreateGameObjectData(GameObject gameObject)
    {
        // 创建一个新的GameObjectDataJ对象
        GameObjectDataJ gameObjectData = new GameObjectDataJ();

        // 记录预设名
        if (gameObject.name.Contains("(Clone)"))
        {
            gameObjectData.prefabName = gameObject.name.Replace("(Clone)", "");
        }
        else
        {
            gameObjectData.prefabName = gameObject.name;
        }

        // 记录位置、旋转和缩放
        gameObjectData.position = new float[] { gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z };
        gameObjectData.rotation = new float[] { gameObject.transform.rotation.eulerAngles.x, gameObject.transform.rotation.eulerAngles.y, gameObject.transform.rotation.eulerAngles.z };
        gameObjectData.scale = new float[] { gameObject.transform.localScale.x, gameObject.transform.localScale.y, gameObject.transform.localScale.z };

        // 如果GameObject有SpriteRenderer组件，并且sprite不为空，则记录精灵的路径
        SpriteRenderer spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        if (spriteRenderer != null && spriteRenderer.sprite != null)
        {
            gameObjectData.spritePath = spriteRenderer.sprite.name;
        }

        return gameObjectData;
    }

    // 删除parent下的所有子对象
    void ClearAllChildren(Transform parent)
    {
        for (int i = parent.childCount - 1; i >= 0; i--)
        {
            Destroy(parent.GetChild(i).gameObject);
        }
    }

    // 从JSON文件加载游戏对象
    public void LoadGameObjects(string name)
    {
        // 清除generatorTransform下的所有子对象
        if (generatorTransform != null)
        {
            ClearAllChildren(generatorTransform);
        }
        else
        {
            Debug.LogError("Generator object not found!");
        }

        // 读取存档文件，并重新创建游戏对象
        if (System.IO.File.Exists(GetSaveFilePath(name)))
        {
            string json = System.IO.File.ReadAllText(GetSaveFilePath(name));
            Wrapper wrapper = JsonUtility.FromJson<Wrapper>(json);
            gameObjectsData = wrapper.gameObjectsDataJ;

            // 遍历gameObjectsData列表，并根据列表中的数据重新创建游戏对象
            foreach (var gameObjectData in gameObjectsData)
            {
                // 加载预设，并创建新的游戏对象
                GameObject prefab = Resources.Load<GameObject>(gameObjectData.prefabName);
                if (prefab != null)
                {
                    // 创建新的游戏对象，并设置其属性
                    GameObject newObj = Instantiate(prefab,
                        new Vector3(gameObjectData.position[0], gameObjectData.position[1], 0f),
                        Quaternion.Euler(0f, 0f, gameObjectData.rotation[2]));
                    newObj.transform.localScale = new Vector3(gameObjectData.scale[0], gameObjectData.scale[1], 1f);

                    // 设置新创建的对象为generatorTransform的子对象
                    newObj.transform.SetParent(generatorTransform);

                    // 如果游戏对象使用的是分割过的精灵，则加载小精灵
                    Sprite[] sprites = Resources.LoadAll<Sprite>("StatusIcons");
                    Sprite smallSprite = null;
                    foreach (Sprite sprite in sprites)
                    {
                        if (sprite.name == gameObjectData.spritePath)
                        {
                            smallSprite = sprite;
                            break;
                        }
                    }
                    SpriteRenderer spriteRenderer = newObj.GetComponent<SpriteRenderer>();
                    if (smallSprite != null)
                    { spriteRenderer.sprite = smallSprite; }
                    else
                    {
                        // 加载和设置精灵,如果加载的是独立的小精灵的话
                        if (!string.IsNullOrEmpty(gameObjectData.spritePath))
                        {
                            Sprite sprite = Resources.Load<Sprite>(gameObjectData.spritePath);
                            print(sprite);
                            if (sprite != null)
                            {
                                if (spriteRenderer != null)
                                {
                                    spriteRenderer.sprite = sprite;
                                }
                            }
                            else
                            {
                                print(gameObjectData.spritePath+"?");
                            }
                        }
                    }
                }
                else
                {
                    InformationShow.SetActive(true);
                    text_information.text = "出错了";
                }
            }

            // 显示加载完毕的信息
            InformationShow.SetActive(true);
            text_information.text = "读取完毕";
        }
        else
        {
            // 如果存档文件不存在，则显示存档不存在的信息
            InformationShow.SetActive(true);
            text_information.text = "存档不存在";
        }
    }

    // Wrapper类，用于在保存和加载时，将gameObjectsData列表转换为JSON对象
    [System.Serializable]
    private class Wrapper
    {
        public List<GameObjectDataJ> gameObjectsDataJ;
    }
}
