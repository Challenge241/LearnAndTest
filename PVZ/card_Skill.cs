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
        darkBg = transform.Find("dark").gameObject;//transform.Find("")可以找到脚本所在物体的子物体
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

    //拖拽开始（鼠标点下的那一瞬间）
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
    //拖拽过程（按住鼠标不松）
    public void OnDrag(BaseEventData data)
    {
        //Debug.Log("OnDrag" + data.ToString());
        if (curGameObject == null)
        {
            return;
        }
        PointerEventData pointerEventData = data as PointerEventData;//???
        //根据鼠标位置移动物体
        curGameObject.transform.position = TranlateScreenToWorld(pointerEventData.position);
    }
    //拖拽结束（松开鼠标）
    public void OnEndDrag(BaseEventData data)
    {
        Debug.Log("OnEndDrag" + data.ToString());
        if (curGameObject == null)
        {
            return;
        }
        PointerEventData pointerEventData = data as PointerEventData;//???
        //拿到鼠标所在位置的碰撞体
        Collider2D[] col = Physics2D.OverlapPointAll(TranlateScreenToWorld(pointerEventData.position));
        //遍历碰撞体
        foreach (Collider2D c in col)
        {
            //判断物体为植物，并且该植物可切换模式
            if (c.tag == "Plant"&&c.GetComponent<Plant>().waitskill==false&& c.GetComponent<Plant>().canskill == true)
            {
                c.GetComponent<Plant>().haveskill= true;
                Destroy(curGameObject);
                curGameObject = null;
                GameManager.instance.ChangeSunNum(-useSun);
                //重置计时器
                timer = 0;
                break;
            }
            if (c.tag == "Spike" && c.GetComponent<Spikeweed>().waitskill == false && c.GetComponent<Spikeweed>().canskill == true)
            {
                c.GetComponent<Spikeweed>().haveskill = true;
                Destroy(curGameObject);
                curGameObject = null;
                GameManager.instance.ChangeSunNum(-useSun);
                //重置计时器
                timer = 0;
                break;
            }
            if (c.tag == "invisiblePea" && c.GetComponent<InvisiblePea>().waitskill == false && c.GetComponent<InvisiblePea>().canskill == true)
            {
                c.GetComponent<InvisiblePea>().haveskill = true;
                Destroy(curGameObject);
                curGameObject = null;
                GameManager.instance.ChangeSunNum(-useSun);
                //重置计时器
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
