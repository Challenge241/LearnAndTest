using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleManager : MonoBehaviour
{
    public static CircleManager Instance; // 静态实例
    public GameObject circlePrefab; // 圆形的预制体
    private void Awake()
    {
        // 设置静态实例为当前实例
        Instance = this;
    }
    private void Start()
    {
        
    }
    // 保存游戏数据到存档
    public void SaveGame()
    {
        // 清空之前的存档数据
        PlayerPrefs.DeleteAll();
        //print(circleDataList.Count);
        // 保存每个圆的数据
        for (int i = 0; i < CircleDataListWrapper.Instance.circleDataList.Count; i++)
        {
            CircleData circleData = CircleDataListWrapper.Instance.circleDataList[i];
            string key = "Circle" + i.ToString();

            // 保存圆的位置
            PlayerPrefs.SetFloat(key + "_PosX", circleData.position.x);
            PlayerPrefs.SetFloat(key + "_PosY", circleData.position.y);

            // 保存圆的大小
            PlayerPrefs.SetFloat(key + "_Size", circleData.size);

            // 保存圆的颜色
            PlayerPrefs.SetFloat(key + "_ColorR", circleData.color.r);
            PlayerPrefs.SetFloat(key + "_ColorG", circleData.color.g);
            PlayerPrefs.SetFloat(key + "_ColorB", circleData.color.b);
        }

        // 保存圆的数量
        PlayerPrefs.SetInt("CircleCount", CircleDataListWrapper.Instance.circleDataList.Count);

        // 保存存档
        PlayerPrefs.Save();
        // 添加调试信息
        //Debug.Log("Saved " + circleDataList.Count + " circles.");
    }

    // 从存档中加载游戏数据
    public void LoadGame()
    {
        // 销毁之前的圆形游戏物体
        //思路一：把所有该销毁的物体作为某个父物体的子对象，然后销毁其所有子对象
        /*foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }*/
        //思路二：给所有可销毁的物体添加一个代表可以销毁的脚本，销毁所有该脚本挂载的游戏对象
        //思路三：销毁圆
        // 找到并销毁之前的圆形游戏物体
        CircleData[] previousCircles = FindObjectsOfType<CircleData>();
        foreach (CircleData circleData in previousCircles)
        {
            Destroy(circleData.gameObject);
        }

        // 清空之前的圆形数据
        CircleDataListWrapper.Instance.circleDataList.Clear();

        // 获取圆的数量
        int circleCount = PlayerPrefs.GetInt("CircleCount", 0);
        // 添加调试信息
        //Debug.Log("Loaded " + circleCount + " circles.");
        // 加载每个圆的数据
        for (int i = 0; i < circleCount; i++)
        {
            string key = "Circle" + i.ToString();

            // 添加调试信息
            //Debug.Log("Loading data for circle " + i);

            // 读取圆的位置
            float posX = PlayerPrefs.GetFloat(key + "_PosX");
            float posY = PlayerPrefs.GetFloat(key + "_PosY");
            Vector3 position = new Vector3(posX, posY, 0f);

            // 读取圆的大小
            float size = PlayerPrefs.GetFloat(key + "_Size");

            // 读取圆的颜色
            float colorR = PlayerPrefs.GetFloat(key + "_ColorR");
            float colorG = PlayerPrefs.GetFloat(key + "_ColorG");
            float colorB = PlayerPrefs.GetFloat(key + "_ColorB");
            Color color = new Color(colorR, colorG, colorB);

            // 创建圆形对象并设置属性
            GameObject circle = Instantiate(circlePrefab, position, Quaternion.identity);
            SpriteRenderer circleRenderer = circle.GetComponent<SpriteRenderer>();
            circleRenderer.color = color;
            circle.transform.localScale = new Vector3(size, size, 1f);

            // 获取或添加CircleData脚本
            CircleData circleDataScript = circle.GetComponent<CircleData>();
            if (circleDataScript == null)
            {
                circleDataScript = circle.AddComponent<CircleData>();
            }

            // 给CircleData脚本赋值
            circleDataScript.position = position;
            circleDataScript.size = size;
            circleDataScript.color = color;

            // 添加到CircleManager的circleDataList列表中
            CircleDataListWrapper.Instance.circleDataList.Add(circleDataScript);
        }
    }

    // 清除存档数据
    public void ClearSaveData()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
    }
}