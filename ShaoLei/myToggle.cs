using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class myToggle : MonoBehaviour
{
    public GameObject SwitchON;
    public GameObject SwitchOFF;
    private Toggle switchToggle;
    // Start is called before the first frame update
    void Start()
    {
        switchToggle = GetComponent<Toggle>();
        OnValueChange(switchToggle.isOn);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnValueChange(bool isOn)
    {
        SwitchON.SetActive(isOn);
        SwitchOFF.SetActive(!isOn);
    }
}
