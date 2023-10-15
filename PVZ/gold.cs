using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gold : MonoBehaviour
{
    public int coin=100;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject,12);
    }
    // Update is called once per frame
    void Update()
    {

    }
    public void OnMouseDown()
    {
       //Debug.LogWarning("Good");
        UIManager.instance.ChangeCoinNum(coin);
        Destroy(gameObject);
    }
}
