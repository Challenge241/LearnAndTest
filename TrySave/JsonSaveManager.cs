using UnityEngine;
using System.Collections.Generic;
using System.IO;

public class JsonSaveManager : MonoBehaviour
{
    public GameObject circlePrefab; // 圆形的预制体
    [SerializeField]
    private string saveFileName = "circle_save.json"; // 存档文件名

    public void SaveCircles(List<CircleData> circleDataList)
    {   /*JSON序列化列表后，不能序列化列表里的游戏对象
        string filePath = Path.Combine(Application.persistentDataPath, saveFileName);
        print("circleDataList.Count" + circleDataList.Count);
        // 序列化圆的数据列表为JSON字符串
        string jsonData = JsonUtility.ToJson(circleDataList);
        print("jsonData"+jsonData);
        // 将JSON字符串保存到文件中
        File.WriteAllText(filePath, jsonData);*/
        string filePath = Path.Combine(Application.persistentDataPath, saveFileName);
        CircleDataListWrapper wrapper = CircleDataListWrapper.Instance;
        Debug.Log("CircleDataListWrapper: " + wrapper);
        string jsonData = JsonUtility.ToJson(wrapper, true);
        Debug.Log("jsonData: " + jsonData);
        File.WriteAllText(filePath, jsonData);
    }
    public void SaveGame()
    {
        print(CircleDataListWrapper.Instance.circleDataList.Count);
        SaveCircles(CircleDataListWrapper.Instance.circleDataList);
    }
     public List<CircleData> LoadCircles()
    {
        string filePath = Path.Combine(Application.persistentDataPath, saveFileName);

        if (File.Exists(filePath))
        {
            // 从文件中读取JSON字符串
            string jsonData = File.ReadAllText(filePath);
            print(jsonData);
            // 反序列化JSON字符串为CircleSaveData列表
            List<CircleData> circleDataList = JsonUtility.FromJson<List<CircleData>>(jsonData);
            print("LoadCircles()"+circleDataList.Count);
            return circleDataList;
        }
        else
        {
            Debug.LogWarning("Save file not found.");
            return new List<CircleData>();
        }
    }
    public void loadGame()
    {
        /*
        CircleData[] previousCircles = FindObjectsOfType<CircleData>();
        foreach (CircleData circleData in previousCircles)
        {
            Destroy(circleData.gameObject);
        }
        // 清空之前的圆形数据
        CircleManager.Instance.circleDataList.Clear();*/
        //读取圆的数据列表
        CircleDataListWrapper.Instance.circleDataList = LoadCircles();
        foreach (CircleData circledata in CircleDataListWrapper.Instance.circleDataList)
        {
            // 创建圆形对象并设置属性
            GameObject circle = Instantiate(circlePrefab, circledata.position, Quaternion.identity);
            SpriteRenderer circleRenderer = circle.GetComponent<SpriteRenderer>();
            circleRenderer.color = circledata.color;
            circle.transform.localScale = new Vector3(circledata.size, circledata.size, 1f);
            // 获取或添加CircleData脚本
            CircleData circleDataScript = circle.GetComponent<CircleData>();
            if (circleDataScript == null)
            {
                circleDataScript = circle.AddComponent<CircleData>();
            }

            // 给CircleData脚本赋值
            circleDataScript.position = circledata.position;
            circleDataScript.size = circledata.size;
            circleDataScript.color = circledata.color;

            // 添加到CircleManager的circleDataList列表中
            CircleDataListWrapper.Instance.circleDataList.Add(circleDataScript);    
        }
    }
}