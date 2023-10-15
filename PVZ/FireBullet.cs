using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBullet : Bullet
{
    // Start is called before the first frame update
    void Start()
    {
        GameObject.Destroy(gameObject, 10);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += direction * speed * Time.deltaTime;
    }
}
