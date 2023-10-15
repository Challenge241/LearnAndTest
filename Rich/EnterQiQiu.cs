using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterQiQiu : MonoBehaviour
{
    
    public GameObject MainCanvas;
    public GameObject MainCamera;
    public GameObject QiQiuCanvas;
    public GameObject QiQiuCamera;
    public GameObject MainGuangBiao;
    public GameObject QiQiuGuangBiao;
    public void GameStart(RichPlayer player)
    {
        MainCanvas.SetActive(false);
        MainCamera.SetActive(false);
        MainGuangBiao.SetActive(false);
        QiQiuCanvas.SetActive(true);
        QiQiuCamera.SetActive(true);
        QiQiuGuangBiao.SetActive(true);
        ScoreManager.instance.richPlayer = player;
    }
    public void GameEnd()
    {
        
        MainCanvas.SetActive(true);
        MainCamera.SetActive(true);
        MainGuangBiao.SetActive(true);
        QiQiuCanvas.SetActive(false);
        QiQiuCamera.SetActive(false);
        QiQiuGuangBiao.SetActive(false);
        ScoreManager.instance.richPlayer = null;
    }
}
