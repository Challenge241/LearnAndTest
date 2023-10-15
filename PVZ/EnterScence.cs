using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class EnterScence : MonoBehaviour
{
    public void EnterScenceGames()
    {
        SceneManager.LoadScene("Games");
    }
    public void EnterScenceTank()
    {
        SceneManager.LoadScene("tank");
    }
    public void EnterScencePVZGameBegin()
    {
        SceneManager.LoadScene("PVZGameBegin");
    }
    public void EnterScenceSnake()
    {
        SceneManager.LoadScene("Snake");
    }
    public void EnterScenceAngryBird()
    {
        SceneManager.LoadScene("BirdWaitScence");
    }
    public void EnterScenceShaoLeiNew()
    {
        SceneManager.LoadScene("ShaoLeiNew");
    }
    public void EnterScenceXiaoXiaoLe()
    {
        SceneManager.LoadScene("tryXiaoXiaoLe");
    }
    public void EnterScenceBattleOfBalls()
    {
        SceneManager.LoadScene("BattleOfBalls");
    }
    public void EnterScenceKnight()
    {
        SceneManager.LoadScene("Knight");
    }
    public void EnterScenceGal()
    {
        SceneManager.LoadScene("TryGal");
    }
    public void EnterScenceRicher()
    {
        SceneManager.LoadScene("Rich");
    }
    public void EnterScenceSnakeNew()
    {
        SceneManager.LoadScene("SnakeNew");
    }
    public void EnterScenceQiQiu()
    {
        SceneManager.LoadScene("QiQiu");
    }
    public void EnterScenceTrySave()
    {
        SceneManager.LoadScene("TrySave");
    }
    public void EnterScenceTryJsonSave()
    {
        SceneManager.LoadScene("TryJSON");
    }

    public void EnterScenceTryJsonSaveTWO()
    {
        SceneManager.LoadScene("TryJSON2");
    }
    public void EnterScenceTryJsonSaveThree()
    {
        SceneManager.LoadScene("TryJSON3");
    }
    public void EnterScenceTryNet()
    {
        SceneManager.LoadScene("TryNet");
    }
    public void ExitGame()
    {
        Application.Quit();
    }

}
