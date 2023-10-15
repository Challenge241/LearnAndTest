using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XieCheng : MonoBehaviour
{
    public Transform pos;
    public GameObject zombie;
    public int x;
    public float y;
    // Start is called before the first frame update
    void Start()
    {
        //协程的启动
        //StartCoroutine("ChangeState");
        //StartCoroutine(ChangeState());
        //IEnumerator ie = ChangeState();
        //StartCoroutine(ie);

        //协程的停止
        //StopCoroutine("ChangeState");//停止所有名为ChangeState的协程

        //用StopCoroutine(ChangeState())无法停止协程

        //StopCoroutine(ie);//停止对象ie所代表的协程
        //StopAllCoroutines();//停止所有协程
        StartCoroutine(CreateZombie(x));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator CreateZombie(int x)
    {
        for(int i = 1; i <= x; i++)
        {
            yield return new WaitForSeconds(2);//停2秒
            yield return new WaitForEndOfFrame();//等待本帧结束，例如：本帧过了一半，执行此语句会等到本帧结束再执行下一条语句
            yield return new WaitForSeconds(y);//停y秒
            yield return null;//停1帧
            Instantiate(zombie, pos.position, Quaternion.identity);
        }
    }
}
