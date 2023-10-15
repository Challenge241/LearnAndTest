using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public int sunNum = 50;
   // public int coinNum = 0;
    public GameObject bornParent;
    public GameObject zombiePrefab;
    public float createZombieTi;
   // private int zOrderIndex = 0;//僵尸的图层排序
    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        //CreateZombie();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void ChangeSunNum(int changeNum)
    {
        //Debug.Log("ChangeSunNum");
        sunNum += changeNum;
        //Debug.Log(sunNum);
        if (sunNum <= 0)
        {
            sunNum = 0;
        }
        //wait to do 阳光数值改变，通知卡片压黑等
        UIManager.instance.UpdateUI();
    }
    /*
    //协程法生成僵尸
    public void CreateZombie()
    {
        StartCoroutine(DalayCreateZombie());
    }
    IEnumerator DalayCreateZombie()//协程函数特点，其返回值要为IEnumerator
    {//看不懂协程，明天还是打算用计时器制作僵尸生成系统
        //等待
        yield return new WaitForSeconds(createZombieTi);//如何控制协程的等待时间？
        //生成
        GameObject zombie = Instantiate(zombiePrefab);
        int index=Random.Range(1, 6);
        Transform zombieLine = bornParent.transform.Find("born" + index.ToString());//明白了，这方法妙啊
        zombie.transform.parent = zombieLine;
        zombie.transform.localPosition = Vector3.zero;
        //使后面出现的僵尸显示在上层
        zombie.GetComponent<SpriteRenderer>().sortingOrder = zOrderIndex;
        zOrderIndex += 1;
        //再次启动定时器
        StartCoroutine(DalayCreateZombie());
    }*/
}