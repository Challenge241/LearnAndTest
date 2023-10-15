using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ShuaXinSkill : MonoBehaviour
{
    //private GameObject curGameObject;
    private GameObject darkBg;
    private GameObject progressBar;
    public float waitTime;
    public int useSun;
    private float timer = 0;
    public Text useSunText; 
    // Start is called before the first frame update
    void Start()
    {
        darkBg = transform.Find("dark").gameObject;
        progressBar = transform.Find("progress").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        UpdateProgress();
        UpdateDarkBg();
    }
    void UpdateProgress()
    {
        float per = Mathf.Clamp(timer / waitTime, 0, 1);
        progressBar.GetComponent<Image>().fillAmount = 1 - per;
    }
    void UpdateDarkBg()
    {
        //wait to do ...useSun>sunNum
        if (progressBar.GetComponent<Image>().fillAmount == 0 && GameManager.instance.sunNum >= useSun)
        {
            //Debug.Log("GameManager.instance.sunNum="+ GameManager.instance.sunNum);
            darkBg.SetActive(false);
        }
        else
        {
            darkBg.SetActive(true);
        }
    }
    public void ShuaXin()
    {
        if (darkBg.activeSelf)
        {
            return;
        }
        //之前第一个G是小写的，报错了
        GameObject[] Plants= GameObject.FindGameObjectsWithTag("Plant");
        for(int i = 0; i < Plants.Length; i++)
        {
            if (Plants[i].GetComponent<Plant>().canskill == true)
            {
                Plants[i].GetComponent<Plant>().waitskill = false;
                Plants[i].transform.Find("skillReady").gameObject.SetActive(true);
            }
        }
        GameObject[] cards = GameObject.FindGameObjectsWithTag("card");
        for (int i = 0; i < cards.Length; i++)
        {
            //Debug.LogWarning("Test");
            cards[i].GetComponent<Card>().timer = cards[i].GetComponent<Card>().waitTime;//
        }
        
        GameObject[] newcards = GameObject.FindGameObjectsWithTag("cardnew");
        for (int i = 0; i < newcards.Length; i++)
        {
            newcards[i].GetComponent<CardNew>().timer = newcards[i].GetComponent<CardNew>().waitTime;
        }
        GameObject[] cardlevelup = GameObject.FindGameObjectsWithTag("cardlevelup");
        for (int i = 0; i < cardlevelup.Length; i++)
        {
            cardlevelup[i].GetComponent<CardLevelUp>().timer = cardlevelup[i].GetComponent<CardLevelUp>().waitTime;
        }
        GameManager.instance.ChangeSunNum(-useSun);
        useSun = useSun * 2;
        useSunText.text = useSun.ToString();
        timer = 0;
    }
}
