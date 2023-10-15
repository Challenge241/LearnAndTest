using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Defeat : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Zombie")
        {
            gameover();
        }
        if (other.tag == "SpecialZombie")
        {
             gameover();
        }
    }
    private void gameover()
    {
        transform.Find("ZombiesWon").gameObject.SetActive(true);
        transform.parent.gameObject.transform.Find("MainPanel").gameObject.SetActive(false);
        transform.parent.gameObject.transform.Find("coinBank").gameObject.SetActive(false);
        transform.parent.gameObject.transform.Find("card_LevelUp").gameObject.SetActive(false);
        transform.parent.gameObject.transform.Find("card_MoShiQieHuan").gameObject.SetActive(false);
    }
}
