using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllCardPanel : MonoBehaviour
{
    public GameObject bg;
    public GameObject beforeCardPrefab;
    // Start is called before the first frame update
    void Start()
    {
        //����ѡ������41������
        for(int i = 1; i <= 41; i++)
        {
            GameObject beforeCard = Instantiate(beforeCardPrefab);
            beforeCard.transform.SetParent(bg.transform, false);
            beforeCard.name = "Card" + i.ToString();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
