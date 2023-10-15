using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Card : MonoBehaviour
{
    public GameObject objectPrefab;
    private GameObject curGameObject;
    private GameObject darkBg;
    private GameObject progressBar;
    public float waitTime;
    public int useSun;
    public float timer=0;
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
        if (progressBar.GetComponent<Image>().fillAmount == 0&&GameManager.instance.sunNum>=useSun)
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
        curGameObject = Instantiate(objectPrefab);
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
            //判断物体为土地，并且土地上没有其他植物
            if (c.tag == "Land" && c.transform.childCount == 0)
            {
                //把当前物体添加为土地的子物体
                curGameObject.transform.parent = c.transform;
                curGameObject.transform.localPosition = Vector3.zero;
                //重置默认值，生成结束
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
