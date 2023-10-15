using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
public class BetterJsonSaveManager : MonoBehaviour
{
    public List<GameObjectDataJ> gameObjectsData = new List<GameObjectDataJ>();
    public GameObject InformationShow;
    public Text text_information;
    Transform generatorTransform ;
    private void Start()
    {
        generatorTransform = GameObject.Find("Gerenator").transform;
    }
    private string GetSaveFilePath(int slot)
    {
        return "saveFile_" + slot + ".json";
    }
     void SaveGameObjects(int slot)
    {
        string json = JsonUtility.ToJson(new Wrapper { gameObjectsDataJ = gameObjectsData }, true);
        System.IO.File.WriteAllText(GetSaveFilePath(slot), json);
    }
    public void SaveAllChildObjects(int slot)
    {
        Transform parentTransform = generatorTransform;
        foreach (Transform childTransform in parentTransform)
        {
            GameObjectDataJ data = CreateGameObjectData(childTransform.gameObject);
            gameObjectsData.Add(data);
        }
        // ����������ѡ��gameObjectsData����ΪJSON����֮ǰ��ʾ��
        SaveGameObjects(slot);
        InformationShow.SetActive(true);
        text_information.text = "�浵�ɹ�";
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

    public void LoadGameObjects(int slot)
    {
        
        if (generatorTransform != null)
        {
            ClearAllChildren(generatorTransform);
        }
        else
        {
            Debug.LogError("Generator object not found!");
        }
        //��ʼ��ȡ����
        if (System.IO.File.Exists(GetSaveFilePath(slot)))
        {
            string json = System.IO.File.ReadAllText(GetSaveFilePath(slot));
            Wrapper wrapper = JsonUtility.FromJson<Wrapper>(json);
            gameObjectsData = wrapper.gameObjectsDataJ;
            // (���µĴ��룬�������´��������...)
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
                }
                else
                {
                    InformationShow.SetActive(true);
                    text_information.text = "������";
                }
            }
            InformationShow.SetActive(true);
            text_information.text = "��ȡ���";
        }
        else
        {   //���·������������ʾ�浵������
            InformationShow.SetActive(true);
            text_information.text ="�浵������";
        }
        
    }

    [System.Serializable]
    private class Wrapper
    {
        public List<GameObjectDataJ> gameObjectsDataJ;
    }
}
