using System.Collections.Generic;
using UnityEngine;
using System;
using System.Xml.Serialization;
using System.IO;

public class TestXmlSaveManager : MonoBehaviour
{
    public List<GameObjectDataJ> gameObjectsData = new List<GameObjectDataJ>();

    public void SaveGameObjects()
    {
        XmlSerializer serializer = new XmlSerializer(typeof(List<GameObjectDataJ>));
        using (FileStream file = new FileStream(Application.persistentDataPath + "/saveFile.xml", FileMode.Create))
        {
            serializer.Serialize(file, gameObjectsData);
        }
    }
    public void SaveAllChildObjects(Transform parentTransform)
    {
        foreach (Transform childTransform in parentTransform)
        {
            GameObjectDataJ data = CreateGameObjectData(childTransform.gameObject);
            gameObjectsData.Add(data);

            // 递归调用以保存子物体的子物体（如果有）
            SaveAllChildObjects(childTransform);
        }

        // 在这里，你可以选择将gameObjectsData保存为JSON（如之前所示）
        SaveGameObjects();
    }
    public void LoadGameObjects()
    {
        if (File.Exists(Application.persistentDataPath + "/saveFile.xml"))
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<GameObjectDataJ>));
            using (FileStream file = new FileStream(Application.persistentDataPath + "/saveFile.xml", FileMode.Open))
            {
                gameObjectsData = (List<GameObjectDataJ>)serializer.Deserialize(file);
            }

            //... 之后的加载代码（和之前的类似，只是读取的是XML文件）
            //清理原物体
            Transform generatorTransform = GameObject.Find("Gerenator").transform;
            if (generatorTransform != null)
            {
                ClearAllChildren(generatorTransform);
            }
            else
            {
                Debug.LogError("Generator object not found!");
            }
            // 下面的代码示例是如何使用游戏数据来实例化游戏对象
            // 你需要根据你的项目来调整这部分
            foreach (var gameObjectData in gameObjectsData)
            {
                // 通过prefabName找到对应的预设
                GameObject prefab = Resources.Load<GameObject>(gameObjectData.prefabName);
                if (prefab != null)
                {
                    // 创建游戏对象并设置其属性
                    GameObject newObj = Instantiate(prefab,
                        new Vector3(gameObjectData.position[0], gameObjectData.position[1], 0f),
                        Quaternion.Euler(0f, 0f, gameObjectData.rotation[2]));
                    newObj.transform.localScale = new Vector3(gameObjectData.scale[0], gameObjectData.scale[1], 1f);
                    // 使新创建的对象成为Generator的子对象
                    newObj.transform.SetParent(generatorTransform);
                    //加载的是大精灵分割成的小精灵的情况
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
                    spriteRenderer.sprite = smallSprite;
                }
                else
                {
                    print("else");
                }
            }
        }
        else
        {
            Debug.LogWarning("File Not Exists");
        }
    }

    // ... 其他方法（CreateGameObjectData, ClearAllChildren, SaveAllChildObjects 等）保持不变
    public GameObjectDataJ CreateGameObjectData(GameObject gameObject)
    {
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
        // 记录位置
        gameObjectData.position = new float[] { gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z };

        // 记录旋转
        gameObjectData.rotation = new float[] { gameObject.transform.rotation.eulerAngles.x, gameObject.transform.rotation.eulerAngles.y, gameObject.transform.rotation.eulerAngles.z };

        // 记录缩放
        gameObjectData.scale = new float[] { gameObject.transform.localScale.x, gameObject.transform.localScale.y, gameObject.transform.localScale.z };

        // 记录精灵路径（注意：这里你需要有一个方法来确定精灵的路径）
        SpriteRenderer spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        if (spriteRenderer != null && spriteRenderer.sprite != null)
        {
            // 假设精灵的名称和资源文件夹中的路径是一致的
            gameObjectData.spritePath = spriteRenderer.sprite.name;
        }

        return gameObjectData;
    }
    void ClearAllChildren(Transform parent)
    {
        for (int i = parent.childCount - 1; i >= 0; i--)
        {
            Destroy(parent.GetChild(i).gameObject);
        }
    }

}
