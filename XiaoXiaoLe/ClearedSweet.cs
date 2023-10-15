using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearedSweet : MonoBehaviour
{
    private bool isClearing; // �Ƿ��������
    public bool IsClearing
    {
        get
        {
            return isClearing;
        }
    }
    protected GameSweet sweet; // ��Ϸ�еķ���
    private void Awake()
    {
        sweet= GetComponent<GameSweet>(); // ��ȡGameSweet���
    }
    public virtual void Clear()
    {
        isClearing = true; // �����������Ϊtrue
        Destroy(gameObject); // ���ٵ�ǰ��Ϸ����
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
