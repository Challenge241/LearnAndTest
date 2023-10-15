using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelLoad : MonoBehaviour
{
    private void Awake()
    {
        Instantiate(Resources.Load("level"));
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
