using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class killPigNumManager : MonoBehaviour
{
    public static killPigNumManager instance;
    public int killPigNum;
    public Text killPigNumText;
    private void Awake()
    {
        instance = this ;
    }
    // Start is called before the first frame update
    void Start()
    {
        killPigNum=PlayerPrefs.GetInt("killPigNum", 0);
        UpdateText();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void UpdateText()
    {
        killPigNumText.text = killPigNum.ToString();
    }
    public void swapDate()
    {
        PlayerPrefs.DeleteAll();
        killPigNum = PlayerPrefs.GetInt("killPigNum", 0);
        UpdateText();
    }
}
