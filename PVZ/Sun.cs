using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sun : MonoBehaviour
{
    public float duration;//阳光持续时间
    void destroySun()
    {
        GameObject.Destroy(gameObject, duration);
    }
    // Start is called before the first frame update
    void Start()
    {
        GameObject.Destroy(gameObject, duration);
        //InvokeRepeating("destroySun", duration, duration);
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnMouseDown()
    {
        //点击后，增加阳光数量
        GameManager.instance.ChangeSunNum(25);
        //Debug.Log("OnMouseDown");
        //WaitToDo飞到UI阳光所在位置然后销毁
        GameObject.Destroy(gameObject);
    }
}
