using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SkySunManager : MonoBehaviour
{
    public GameObject sunPrefab;
    public Transform sunPos;//阳光位置
    public float firstskysuntime=1;
    public float sunInterval=5;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("CreateSun", firstskysuntime, sunInterval);
    }

    // Update is called once per frame
    void Update()
    {

    }
    //从天空中生成阳光
    void CreateSun()
    {
        float randomX;
        GameObject sunNew = Instantiate(sunPrefab);
        randomX = Random.Range(transform.position.x - 6, transform.position.x +5);
        //Debug.Log(randomX);
        sunNew.transform.position = new Vector2(randomX, transform.position.y);     
    }
}