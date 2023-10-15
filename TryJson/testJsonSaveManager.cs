using System.Collections.Generic;
using UnityEngine;
using System;

public class testJsonSaveManager : MonoBehaviour
{
    public List<GameObjectDataJ> gameObjectsData = new List<GameObjectDataJ>();

    public void SaveGameObjects()
    {
        string json = JsonUtility.ToJson(new Wrapper { gameObjectsDataJ = gameObjectsData }, true);
        //print(json);
        System.IO.File.WriteAllText("saveFile.json", json);
    }
    public void SaveAllChildObjects(Transform parentTransform)
    {
        foreach (Transform childTransform in parentTransform)
        {
            GameObjectDataJ data = CreateGameObjectData(childTransform.gameObject);
            gameObjectsData.Add(data);
        }
        // ����������ѡ��gameObjectsData����ΪJSON����֮ǰ��ʾ��
        SaveGameObjects();
    }
    public GameObjectDataJ CreateGameObjectData(GameObject gameObject)
    {
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
        // ��¼λ��
        gameObjectData.position = new float[] { gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z };

        // ��¼��ת
        gameObjectData.rotation = new float[] { gameObject.transform.rotation.eulerAngles.x, gameObject.transform.rotation.eulerAngles.y, gameObject.transform.rotation.eulerAngles.z };

        // ��¼����
        gameObjectData.scale = new float[] { gameObject.transform.localScale.x, gameObject.transform.localScale.y, gameObject.transform.localScale.z };

        // ��¼����·����ע�⣺��������Ҫ��һ��������ȷ�������·����
        SpriteRenderer spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        if (spriteRenderer != null && spriteRenderer.sprite != null)
        {
            // ���辫������ƺ���Դ�ļ����е�·����һ�µ�
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

    public void LoadGameObjects()
    {
        //����ԭ����
        Transform generatorTransform = GameObject.Find("Gerenator").transform;
        if (generatorTransform != null)
        {
            ClearAllChildren(generatorTransform);
        }
        else
        {
            Debug.LogError("Generator object not found!");
        }
        //��ʼ��ȡ����
        string json = System.IO.File.ReadAllText("saveFile.json");
        Wrapper wrapper = JsonUtility.FromJson<Wrapper>(json);
        gameObjectsData = wrapper.gameObjectsDataJ;
        //print(gameObjectsData);
        // ����Ĵ���ʾ�������ʹ����Ϸ������ʵ������Ϸ����
        // ����Ҫ���������Ŀ�������ⲿ��
        foreach (var gameObjectData in gameObjectsData)
        {
            // ͨ��prefabName�ҵ���Ӧ��Ԥ��
            GameObject prefab = Resources.Load<GameObject>(gameObjectData.prefabName);
            if (prefab != null)
            {
                // ������Ϸ��������������
                GameObject newObj = Instantiate(prefab,
                    new Vector3(gameObjectData.position[0], gameObjectData.position[1], 0f),
                    Quaternion.Euler(0f, 0f, gameObjectData.rotation[2]));
                newObj.transform.localScale = new Vector3(gameObjectData.scale[0], gameObjectData.scale[1], 1f);
                // ʹ�´����Ķ����ΪGenerator���Ӷ���
                newObj.transform.SetParent(generatorTransform);
                //���ص��Ǵ���ָ�ɵ�С��������
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
                // ���غ����þ���,������ص��Ƕ�����С����Ļ�
                /*if (!string.IsNullOrEmpty(gameObjectData.spritePath))
                {
                    Sprite sprite = Resources.Load<Sprite>(gameObjectData.spritePath);
                    if (sprite != null)
                    {
                        SpriteRenderer spriteRenderer = newObj.GetComponent<SpriteRenderer>();
                        if (spriteRenderer != null)
                        {
                            spriteRenderer.sprite = sprite;
                        }
                    }
                    else
                    {
                        print(gameObjectData.spritePath+"?");
                    }
                }*/
            }
            else
            {
                print("else");
            }
        }
    }

    [System.Serializable]
    private class Wrapper
    {
        public List<GameObjectDataJ> gameObjectsDataJ;
    }
}
