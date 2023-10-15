using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class levelUpRepeater : MonoBehaviour
{
    //这类的名字有问题，实际上其可用于各种升级，不只是豌豆射手升级为双发射手
    //因为第一个使用该脚本的是用豌豆射手升级为双发射手，当时没想到该脚本泛用性如此高
    private GameObject f;
    private GameObject p;
    private GameObject LevelUpPlant;
    public GameObject LevelUpPlantPrefab;//升级后的植物
    // Start is called before the first frame update
    void Start()
    {
        f = transform.parent.gameObject;//拿到父物体豌豆射手
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnMouseDown()
    {
            //拿到物体预制件
            LevelUpPlant = Instantiate(LevelUpPlantPrefab);
            //拿到豌豆射手的父物体土地
            p = f.transform.parent.gameObject;
            //将土地赋给升级版植物的父物体
            LevelUpPlant.transform.parent = p.transform;
            LevelUpPlant.transform.localPosition = Vector3.zero;
            //销毁豌豆射手，避免物体重叠
            Destroy(f.gameObject);
    }
}
