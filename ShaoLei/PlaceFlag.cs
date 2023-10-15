using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceFlag : MonoBehaviour
{
    // ����һ��������GameObject���ͱ���flagPrefab������������ڴ���������Ԥ����
    public GameObject flagPrefab;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //placeflag();
    }
    /*��Unity�У�ʹ��������UIԪ�أ�����Button��Image�ȣ��Ͳ���3D��2D��Ϸ����������ͬ��
     * ����UIԪ�أ�����ͨ��ʹ��EventSystem��IPointerClickHandler��IPointerEnterHandler��IPointerExitHandler�Ƚӿڣ�������ʹ������Ͷ�䡣*/
    void placeflag()
    {
        if (Input.GetMouseButtonDown(1))
        {
            print("1");
            // �����������һ�����ߣ����ߵķ����Ǵ�����������������Ļ�ϵ�λ��
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            // ����һ��RaycastHit����������������ᱻ���ڴ洢����Ͷ��Ľ��
            RaycastHit hit;

            // �������Ͷ��������һ������
            if (Physics.Raycast(ray, out hit))
            {
                print("y");
                // ������е�������û����Ϊ"Flag"�������壨Ҳ����˵���������û�б���ǣ�
                if (hit.transform.Find("Flag") == null)
                {
                    // �����е������λ��ʵ����һ������Ԥ����
                    GameObject flag = Instantiate(flagPrefab, hit.transform.position, Quaternion.identity);

                    // ��ʵ���������ĵ���������Ϊ"Flag"
                    flag.name = "Flag";

                    // ��ʵ��������������Ϊ���е������������
                    flag.transform.SetParent(hit.transform);
                    hit.transform.GetComponent<SLGrid>().isFlag = true;
                }
                // ������е��������Ѿ�����Ϊ"Flag"��������
                else
                {
                    // ������Ϊ"Flag"�������壨Ҳ����˵���Ƴ���ǣ�
                    Destroy(hit.transform.Find("Flag").gameObject);
                    hit.transform.GetComponent<SLGrid>().isFlag = false;
                }
            }
        }
    }
}
