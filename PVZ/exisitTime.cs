using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class exisitTime : MonoBehaviour
{
    public float timer=0;
    public int inttimer=0;
    public int miao1;
    public int fen1;
    public Text miao;
    public Text fen;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        inttimer = (int)timer;
        miao1 = inttimer % 60;
        fen1=inttimer / 60;
        miao.text = miao1.ToString();
        fen.text = fen1.ToString();
    }
}
