using UnityEngine;
using System.Collections.Generic;
using System.IO;

public class JsonSaveManager : MonoBehaviour
{
    public GameObject circlePrefab; // Բ�ε�Ԥ����
    [SerializeField]
    private string saveFileName = "circle_save.json"; // �浵�ļ���

    public void SaveCircles(List<CircleData> circleDataList)
    {   /*JSON���л��б�󣬲������л��б������Ϸ����
        string filePath = Path.Combine(Application.persistentDataPath, saveFileName);
        print("circleDataList.Count" + circleDataList.Count);
        // ���л�Բ�������б�ΪJSON�ַ���
        string jsonData = JsonUtility.ToJson(circleDataList);
        print("jsonData"+jsonData);
        // ��JSON�ַ������浽�ļ���
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
            // ���ļ��ж�ȡJSON�ַ���
            string jsonData = File.ReadAllText(filePath);
            print(jsonData);
            // �����л�JSON�ַ���ΪCircleSaveData�б�
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
        // ���֮ǰ��Բ������
        CircleManager.Instance.circleDataList.Clear();*/
        //��ȡԲ�������б�
        CircleDataListWrapper.Instance.circleDataList = LoadCircles();
        foreach (CircleData circledata in CircleDataListWrapper.Instance.circleDataList)
        {
            // ����Բ�ζ�����������
            GameObject circle = Instantiate(circlePrefab, circledata.position, Quaternion.identity);
            SpriteRenderer circleRenderer = circle.GetComponent<SpriteRenderer>();
            circleRenderer.color = circledata.color;
            circle.transform.localScale = new Vector3(circledata.size, circledata.size, 1f);
            // ��ȡ�����CircleData�ű�
            CircleData circleDataScript = circle.GetComponent<CircleData>();
            if (circleDataScript == null)
            {
                circleDataScript = circle.AddComponent<CircleData>();
            }

            // ��CircleData�ű���ֵ
            circleDataScript.position = circledata.position;
            circleDataScript.size = circledata.size;
            circleDataScript.color = circledata.color;

            // ��ӵ�CircleManager��circleDataList�б���
            CircleDataListWrapper.Instance.circleDataList.Add(circleDataScript);    
        }
    }
}