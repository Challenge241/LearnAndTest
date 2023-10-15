using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngryBirdManager : MonoBehaviour
{
    public List<Bird> birds;
    public List<pig> pigs;
    public static AngryBirdManager _instance;
    private Vector3 originPos;
    public GameObject win;
    public GameObject lose;

    private void Awake()
    {
        _instance = this;
        if (birds.Count > 0)
        {
            originPos = birds[0].transform.position;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        AngryBirdInitialized();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void AngryBirdInitialized()
    {
        for(int i = 0; i < birds.Count; i++)
        {
            if (i == 0)
            {
                birds[i].transform.position = originPos;
                birds[i].enabled = true;
                birds[i].sp.enabled = true;
            }
            else{
                birds[i].enabled = false;
                birds[i].sp.enabled = false;
            }
        }
    }
    public void NextBird()
    {
        if (pigs.Count>0)
        {
            if (birds.Count > 0)
            {
                //��һֻ��
                AngryBirdInitialized();
            }
            else
            {
                //����
                lose.SetActive(true);
            }
        }
        else
        {
            //Ӯ��
            win.SetActive(true);
        }
    }
    public void showStars()
    {

    }
}
