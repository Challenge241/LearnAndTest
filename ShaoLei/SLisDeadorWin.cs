using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SLisDeadorWin : MonoBehaviour
{
    public bool isDeadorWin=false;
    private static SLisDeadorWin instance;
    public int scores = 0;
    public int flagNum;
    public Text num;
    public static SLisDeadorWin Instance
    {
        get
        {
            return instance;
        }

        set
        {
            instance = value;
        }
    }
    private void Awake()
    {
        Instance = this;
    }
// Start is called before the first frame update
void Start()
    {
        flagNum = MapGenerator.Instance.mineCount;
        //print(flagNum);
        num.text = flagNum.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
