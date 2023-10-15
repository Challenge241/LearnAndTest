using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System;

public class GreatJsonSaveManager : MonoBehaviour
{
    #region strList
    // ����һ���ַ������͵�List//����浵�����б�
    public List<string> stringList = new List<string>();
    // ����һ��DateTime���͵�List�����ڴ洢�浵ʱ��
    List<DateTime> saveDates = new List<DateTime>();
    public void ListAdd(string name, DateTime dateTime)
    {
        int index = stringList.IndexOf(name); // ���name�Ƿ��Ѵ�����stringList�У�����ȡ������
        if (index != -1) // �������
        {
            stringList.RemoveAt(index); // ɾ��stringList�ж�Ӧ������Ԫ��
            saveDates.RemoveAt(index); // ɾ��saveDates�ж�Ӧ������Ԫ��
        }

        // �������б��ĩβ�ֱ����name��dateTime
        stringList.Add(name);
        saveDates.Add(dateTime);
    }

    public void saveList()
    {
        // ��stringListת��Ϊ���ŷָ����ַ���
        string stringListStr = string.Join(",", stringList);
        PlayerPrefs.SetString("stringList", stringListStr);

        // ��saveDatesת��Ϊ���ŷָ����ַ���
        List<string> dateStringList = saveDates.ConvertAll(date => date.ToString("o"));
        string saveDatesStr = string.Join(",", dateStringList);
        PlayerPrefs.SetString("saveDates", saveDatesStr);

        PlayerPrefs.Save();
    }
    public void loadList()
    {
        // ��PlayerPrefs�м����ַ�������ʼ��stringList
        string stringListStr = PlayerPrefs.GetString("stringList", "");
        if (!string.IsNullOrEmpty(stringListStr))
        {
            stringList = new List<string>(stringListStr.Split(','));
            //print(stringList);

        }

        // ��PlayerPrefs�м����ַ�������ʼ��saveDates
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
    private List<GameObject> createdButtons = new List<GameObject>(); // �洢�Ѵ�����ť���б�
    public GameObject gridContent; // Grid GameObject�����ڴ�Ŵ����İ�ť
    public GameObject buttonPrefab; // ButtonԤ����
    public void OpenWindowSave()//��Ҫ����
    {
        // ����֮ǰ���������а�ť
        foreach (var button in createdButtons)
        {
            Destroy(button);
        }
        createdButtons.Clear(); // ����б�

        // ���ݴ浵���ʹ浵ʱ�䴴���µİ�ť
        //for (int i = 0; i < stringList.Count && i < saveDates.Count; i++)
        for (int i = stringList.Count - 1; i >= 0; i--)
        {
            // ��gridContent��ʵ�����µİ�ť
            var button = Instantiate(buttonPrefab, gridContent.transform);
            // ���ð�ť���ı���ʾΪ�浵���ʹ浵ʱ��
            button.GetComponentInChildren<Text>().text = $"{stringList[i]} - {saveDates[i].ToString()}";

            // ��ȡ��ť��Button�������Ϊ����ӵ���¼��ļ�����
            Button btnComponent = button.GetComponent<Button>();
            string saveName = stringList[i]; // ��ȡ��ǰ�浵��
            // Ϊ��ť�ĵ���¼���Ӽ������������ʱ�������SaveAllChildObjects�����������ݴ浵��Ϊ����
            //btnComponent.onClick.AddListener(() => SaveAllChildObjects(saveName));
            // Ϊ��ť�ĵ���¼���Ӽ���������������InputField���ı�
            btnComponent.onClick.AddListener(() => SetInputFieldText(saveName));
            // Ϊɾ����ť��Ӽ�����
            var xButton = button.transform.Find("X_Button").GetComponent<Button>();
            xButton.onClick.AddListener(() => DeleteSaveFileAndRemoveFromLists(saveName));

            // ���´����İ�ť��ӵ��б���
            createdButtons.Add(button);
        }
    }
    // ����InputField���ı�
    private void SetInputFieldText(string text)
    {
        inputField.text = text;
    }
    public void OpenWindowLoad()//����Ҫ�������浵���ã��浵ֱ�ӵ㱣������ˣ���浵��Ϊ�����ı��������ı�����һ�¶��Ǵ浵
    {
        // ����֮ǰ���������а�ť
        foreach (var button in createdButtons)
        {
            Destroy(button);
        }
        createdButtons.Clear(); // ����б�
        // ���ݴ浵���ʹ浵ʱ�䴴���µİ�ť
        //for (int i = 0; i < stringList.Count && i < saveDates.Count; i++)
        for (int i = stringList.Count - 1; i >= 0; i--)
        {
            // ��gridContent��ʵ�����µİ�ť
            var button = Instantiate(buttonPrefab, gridContent.transform);
            // ���ð�ť���ı���ʾΪ�浵���ʹ浵ʱ��
            button.GetComponentInChildren<Text>().text = $"{stringList[i]} - {saveDates[i].ToString()}";

            // ��ȡ��ť��Button�������Ϊ����ӵ���¼��ļ�����
            Button btnComponent = button.GetComponent<Button>();
            string saveName = stringList[i]; // ��ȡ��ǰ�浵��
            // Ϊ��ť�ĵ���¼���Ӽ������������ʱ�������SaveAllChildObjects�����������ݴ浵��Ϊ����
            btnComponent.onClick.AddListener(() => LoadGameObjects(saveName));
            // Ϊɾ����ť��Ӽ�����
            var xButton = button.transform.Find("X_Button").GetComponent<Button>();
            xButton.onClick.AddListener(() => DeleteSaveFileAndRemoveFromLists(saveName));
            // ���´����İ�ť��ӵ��б���
            createdButtons.Add(button);
        }
    }
    void DeleteSaveFileAndRemoveFromLists(string name)
    {
        // �ҵ� name ��Ӧ������
        int index = stringList.IndexOf(name);
        if (index != -1)
        {
            // ɾ����Ӧ��json�ļ�
            string filePath = GetSaveFilePath(name);
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }

            // �������б����Ƴ�Ԫ��
            stringList.RemoveAt(index);
            saveDates.RemoveAt(index);
            saveList();//����ɾ�����
            // �����Ҫ����������ˢ�°�ť�б�
            OpenWindowLoad(); // ���� OpenWindowSave();
        }
        else
        {
            Debug.LogError("DeleteError");
        }
    }

    public void CloseWindow()
    {
        // �رմ���ʱ�������д����İ�ť
        foreach (var button in createdButtons)
        {
            Destroy(button);
        }
        createdButtons.Clear(); // ����б�
    }

    public InputField inputField; // ����InputField���
    public void OnSaveButtonClick()
    {
        // ��ȡInputField�е��ı�
        string inputText = inputField.text;

        // ����SaveAllChildObjects�����������ı���Ϊ����
        SaveAllChildObjects(inputText);
        OpenWindowSave();//���´���
    }
    #endregion
    // ������Ϸ�������ݵ��б�
    public List<GameObjectDataJ> gameObjectsData = new List<GameObjectDataJ>();

    // ��ʾ��Ϣ��GameObject
    public GameObject InformationShow;

    // ��ʾ�ı���Ϣ��Text���
    public Text text_information;

    // ������Ϸ�����Transform
    Transform generatorTransform;
    #region Instance
    // ��̬ʵ��
    public static GreatJsonSaveManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            // ���ʵ�������ڣ��򽫵�ǰ������Ϊʵ��
            Instance = this;

            // ȷ���ڼ����³���ʱ���������������
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            // ���ʵ���Ѵ����Ҳ��ǵ�ǰʵ���������ٵ�ǰ����
            Destroy(gameObject);
        }
    }
    #endregion
    // ��Start�����У��ҵ���Ϊ"Gerenator"��GameObject������ȡ��Transform���
    private void Start()
    {
        generatorTransform = GameObject.Find("Gerenator").transform;
        //saveList();//�ȱ����ٶ�ȡ������մ浵����Ϊ��ʼʱû��Ϣ
        loadList();
    }

    /*
    private string GetSaveFilePath(string name)
    {//��������´浵��ֱ�ӱ�����My project�ļ�����
        return "saveFile_" + name + ".json";
    }*/
    
    private string GetSaveFilePath(string name)
    {//���浽C:\Users\<username>\AppData\LocalLow\<CompanyName>\<ProductName>
        string fileName = "saveFile_" + name + ".json";
        print(Path.Combine(Application.persistentDataPath, fileName));
        return Path.Combine(Application.persistentDataPath, fileName);
    }

    // ��gameObjectsData�б���ΪJSON�ļ�
    void SaveGameObjects(string name)
    {
        string json = JsonUtility.ToJson(new Wrapper { gameObjectsDataJ = gameObjectsData }, true);
        System.IO.File.WriteAllText(GetSaveFilePath(name), json);
    }

    // ����generatorTransform�����е��Ӷ���
    public void SaveAllChildObjects(string name)
    {
        //���б����Ԫ��
        ListAdd(name,DateTime.Now);
        saveList();
        Transform parentTransform = generatorTransform;
        // ����generatorTransform�������Ӷ��󣬲�����������ӵ�gameObjectsData�б���
        foreach (Transform childTransform in parentTransform)
        {
            GameObjectDataJ data = CreateGameObjectData(childTransform.gameObject);
            gameObjectsData.Add(data);
        }

        // ����gameObjectsData�б�ΪJSON�ļ�
        SaveGameObjects(name);

        // ��ʾ�浵�ɹ�����Ϣ
        InformationShow.SetActive(true);
        text_information.text = "�浵�ɹ�";
    }

    // ��һ��GameObject����һ��GameObjectDataJ����
    public GameObjectDataJ CreateGameObjectData(GameObject gameObject)
    {
        // ����һ���µ�GameObjectDataJ����
        GameObjectDataJ gameObjectData = new GameObjectDataJ();

        // ��¼Ԥ����
        if (gameObject.name.Contains("(Clone)"))
        {
            gameObjectData.prefabName = gameObject.name.Replace("(Clone)", "");
        }
        else
        {
            gameObjectData.prefabName = gameObject.name;
        }

        // ��¼λ�á���ת������
        gameObjectData.position = new float[] { gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z };
        gameObjectData.rotation = new float[] { gameObject.transform.rotation.eulerAngles.x, gameObject.transform.rotation.eulerAngles.y, gameObject.transform.rotation.eulerAngles.z };
        gameObjectData.scale = new float[] { gameObject.transform.localScale.x, gameObject.transform.localScale.y, gameObject.transform.localScale.z };

        // ���GameObject��SpriteRenderer���������sprite��Ϊ�գ����¼�����·��
        SpriteRenderer spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        if (spriteRenderer != null && spriteRenderer.sprite != null)
        {
            gameObjectData.spritePath = spriteRenderer.sprite.name;
        }

        return gameObjectData;
    }

    // ɾ��parent�µ������Ӷ���
    void ClearAllChildren(Transform parent)
    {
        for (int i = parent.childCount - 1; i >= 0; i--)
        {
            Destroy(parent.GetChild(i).gameObject);
        }
    }

    // ��JSON�ļ�������Ϸ����
    public void LoadGameObjects(string name)
    {
        // ���generatorTransform�µ������Ӷ���
        if (generatorTransform != null)
        {
            ClearAllChildren(generatorTransform);
        }
        else
        {
            Debug.LogError("Generator object not found!");
        }

        // ��ȡ�浵�ļ��������´�����Ϸ����
        if (System.IO.File.Exists(GetSaveFilePath(name)))
        {
            string json = System.IO.File.ReadAllText(GetSaveFilePath(name));
            Wrapper wrapper = JsonUtility.FromJson<Wrapper>(json);
            gameObjectsData = wrapper.gameObjectsDataJ;

            // ����gameObjectsData�б��������б��е��������´�����Ϸ����
            foreach (var gameObjectData in gameObjectsData)
            {
                // ����Ԥ�裬�������µ���Ϸ����
                GameObject prefab = Resources.Load<GameObject>(gameObjectData.prefabName);
                if (prefab != null)
                {
                    // �����µ���Ϸ���󣬲�����������
                    GameObject newObj = Instantiate(prefab,
                        new Vector3(gameObjectData.position[0], gameObjectData.position[1], 0f),
                        Quaternion.Euler(0f, 0f, gameObjectData.rotation[2]));
                    newObj.transform.localScale = new Vector3(gameObjectData.scale[0], gameObjectData.scale[1], 1f);

                    // �����´����Ķ���ΪgeneratorTransform���Ӷ���
                    newObj.transform.SetParent(generatorTransform);

                    // �����Ϸ����ʹ�õ��Ƿָ���ľ��飬�����С����
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
                        // ���غ����þ���,������ص��Ƕ�����С����Ļ�
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
                    text_information.text = "������";
                }
            }

            // ��ʾ������ϵ���Ϣ
            InformationShow.SetActive(true);
            text_information.text = "��ȡ���";
        }
        else
        {
            // ����浵�ļ������ڣ�����ʾ�浵�����ڵ���Ϣ
            InformationShow.SetActive(true);
            text_information.text = "�浵������";
        }
    }

    // Wrapper�࣬�����ڱ���ͼ���ʱ����gameObjectsData�б�ת��ΪJSON����
    [System.Serializable]
    private class Wrapper
    {
        public List<GameObjectDataJ> gameObjectsDataJ;
    }
}
