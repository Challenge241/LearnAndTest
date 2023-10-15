using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class littleTimeChiXv : MonoBehaviour
{
    private void OnEnable()
    {
        Invoke("DeactivateGameObject", 0.7f);
    }
    void DeactivateGameObject()
    {
        gameObject.SetActive(false);
        CancelInvoke("DeactivateGameObject");
    }
}
