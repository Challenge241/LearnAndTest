using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkySun : Sun
{
    public Vector3 direction;
    private float mubiaoY;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        mubiaoY = Random.Range(-3.5f,2);
        GameObject.Destroy(gameObject, duration);
    }
  

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y >= mubiaoY)
        { transform.position += direction * speed * Time.deltaTime; }
    }
}
