using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class card_Skill : MonoBehaviour
{
    //public GameObject objectPrefab;
    public GameObject objectjpg;
    private GameObject curGameObject;
    private GameObject darkBg;
    private GameObject progressBar;
    public float waitTime;
    public int useSun;
    private float timer = 0;
    // Start is called before the first frame update
    void Start()
    {
        darkBg = transform.Find("dark").gameObject;//transform.Find("")�����ҵ��ű����������������
        progressBar = transform.Find("progress").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        UpdateProgress();
        UpdateDarkBg();
    }
    void UpdateProgress()
    {
        float per = Mathf.Clamp(timer / waitTime, 0, 1);
        progressBar.GetComponent<Image>().fillAmount = 1 - per;
    }
    void UpdateDarkBg()
    {
        //wait to do ...useSun>sunNum
        if (progressBar.GetComponent<Image>().fillAmount == 0 && GameManager.instance.sunNum >= useSun)
        {
            //Debug.Log("GameManager.instance.sunNum="+ GameManager.instance.sunNum);
            darkBg.SetActive(false);
        }
        else
        {
            darkBg.SetActive(true);
        }
    }

    //��ק��ʼ�������µ���һ˲�䣩
    public void OnBeginDrag(BaseEventData data)
    {

        if (darkBg.activeSelf)
        {
            return;
        }
        Debug.Log("OnBeginDrag" + data.ToString());
        PointerEventData pointerEventData = data as PointerEventData;
        curGameObject = Instantiate(objectjpg);
        curGameObject.transform.position = TranlateScreenToWorld(pointerEventData.position);
    }
    //��ק���̣���ס��겻�ɣ�
    public void OnDrag(BaseEventData data)
    {
        //Debug.Log("OnDrag" + data.ToString());
        if (curGameObject == null)
        {
            return;
        }
        PointerEventData pointerEventData = data as PointerEventData;//???
        //�������λ���ƶ�����
        curGameObject.transform.position = TranlateScreenToWorld(pointerEventData.position);
    }
    //��ק�������ɿ���꣩
    public void OnEndDrag(BaseEventData data)
    {
        Debug.Log("OnEndDrag" + data.ToString());
        if (curGameObject == null)
        {
            return;
        }
        PointerEventData pointerEventData = data as PointerEventData;//???
        //�õ��������λ�õ���ײ��
        Collider2D[] col = Physics2D.OverlapPointAll(TranlateScreenToWorld(pointerEventData.position));
        //������ײ��
        foreach (Collider2D c in col)
        {
            //�ж�����Ϊֲ����Ҹ�ֲ����л�ģʽ
            if (c.tag == "Plant"&&c.GetComponent<Plant>().waitskill==false&& c.GetComponent<Plant>().canskill == true)
            {
                c.GetComponent<Plant>().haveskill= true;
                Destroy(curGameObject);
                curGameObject = null;
                GameManager.instance.ChangeSunNum(-useSun);
                //���ü�ʱ��
                timer = 0;
                break;
            }
            if (c.tag == "Spike" && c.GetComponent<Spikeweed>().waitskill == false && c.GetComponent<Spikeweed>().canskill == true)
            {
                c.GetComponent<Spikeweed>().haveskill = true;
                Destroy(curGameObject);
                curGameObject = null;
                GameManager.instance.ChangeSunNum(-useSun);
                //���ü�ʱ��
                timer = 0;
                break;
            }
            if (c.tag == "invisiblePea" && c.GetComponent<InvisiblePea>().waitskill == false && c.GetComponent<InvisiblePea>().canskill == true)
            {
                c.GetComponent<InvisiblePea>().haveskill = true;
                Destroy(curGameObject);
                curGameObject = null;
                GameManager.instance.ChangeSunNum(-useSun);
                //���ü�ʱ��
                timer = 0;
                break;
            }
        }
        if (curGameObject != null)
        {
            GameObject.Destroy(curGameObject);
            curGameObject = null;
        }
    }
    public static Vector3 TranlateScreenToWorld(Vector3 position)
    {
        Vector3 cameraTranslatePos = Camera.main.ScreenToWorldPoint(position);
        return new Vector3(cameraTranslatePos.x, cameraTranslatePos.y, 0);
    }
}
