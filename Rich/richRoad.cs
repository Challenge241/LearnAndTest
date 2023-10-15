using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class richRoad : MonoBehaviour
{
    public RichLand land; // 这个格子上的土地
    public EnterQiQiu qiQiu;
    public int TitleDianJuan;
                          // Start is called before the first frame update
    private void Start()
    {
        land = GetComponentInChildren<RichLand>();
        if (land == null)
        {
            //print("land == null");
        }
        qiQiu = GetComponent<EnterQiQiu>();
        if (qiQiu==null)
        {

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
