using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RichLand : MonoBehaviour
{
    public int price; // 土地的价格
    public RichPlayer owner; // 土地的拥有者
    public GameObject house; // 当前的房子
    public List<GameObject> housePrefabs; // 房子的预制体
    public int[] buildCost ;
    public int type = -1;
    public int level = 0;
    public int maxLevel = 2;
    // Start is called before the first frame update
    void Start()
    {
        price = 2000;
        buildCost = new int[] { 2000, 1500, 1000 };
        GameObject tempPrefab = Resources.Load("SimplyFactory") as GameObject;
        housePrefabs.Add(tempPrefab);
        tempPrefab = Resources.Load("SimplyStore") as GameObject;
        housePrefabs.Add(tempPrefab);
        tempPrefab = Resources.Load("SimplyHouse") as GameObject;
        housePrefabs.Add(tempPrefab);
        tempPrefab = Resources.Load("LevelTwoFactory") as GameObject;
        housePrefabs.Add(tempPrefab);
        tempPrefab = Resources.Load("LevelTwoStore") as GameObject;
        housePrefabs.Add(tempPrefab);
        tempPrefab = Resources.Load("LevelTwoHouse") as GameObject;
        housePrefabs.Add(tempPrefab);

        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            spriteRenderer.color = new Color(139 / 255f, 69 / 255f, 19 / 255f); // RGB for brown
        }
    }
    public void BuildHouse(int i)
    {
        if (house == null)
        {
            type = i;
            level = 1;
            //print("Land.BuildHouse" + i);
            house = Instantiate(housePrefabs[i], transform.position, Quaternion.identity);
            house.transform.parent = transform; // 将房子设为土地的子对象
        }
        else
        {
            Debug.Log("已经有房子了");
        }
    }
    public void UpgradeHouse(int i)
    {
        Destroy(house);
        type = i;
        //print("Land.BuildHouse" + i);
        house = Instantiate(housePrefabs[3 * level + i], transform.position, Quaternion.identity);
        level += 1;
        house.transform.parent = transform; // 将房子设为土地的子对象
    }
}
