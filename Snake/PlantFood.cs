using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantFood : MonoBehaviour
{
    public GameObject FoodPrefab;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void createFood()
    {
        float randomX;
        float randomY;
        GameObject foodNew = Instantiate(FoodPrefab);
        randomX = Random.Range(-150,-170);
        randomY = Random.Range(17, 29);
        //Debug.Log(randomX);
        foodNew.transform.position = new Vector2(randomX, randomY);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {//��ס��ֻ��������������һ���Ǹ���ʱ���Ż���ײ
        if (collision.tag == "SnakeHead")
        {
            collision.SendMessage("Grow");
            createFood();
            Destroy(gameObject);
        }
    }
}
