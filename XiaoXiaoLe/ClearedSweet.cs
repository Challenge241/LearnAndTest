using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearedSweet : MonoBehaviour
{
    private bool isClearing; // 是否正在清除
    public bool IsClearing
    {
        get
        {
            return isClearing;
        }
    }
    protected GameSweet sweet; // 游戏中的方块
    private void Awake()
    {
        sweet= GetComponent<GameSweet>(); // 获取GameSweet组件
    }
    public virtual void Clear()
    {
        isClearing = true; // 设置正在清除为true
        Destroy(gameObject); // 销毁当前游戏对象
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
