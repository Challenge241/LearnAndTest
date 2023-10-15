
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro; // ʹ��TextMeshPro����������ʹ�õ���TextMesh���ǵø��Ĵ˴��ͺ���Ĵ���
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class SLGrid : MonoBehaviour, IPointerClickHandler
{
    public int numberToShow = 0; // ����Ҫ��ʾ�����֣����԰����޸�
    public Text myText; 
    private GameObject SLGridUp;
    public Sprite Mine;
    public Sprite Flag;
    private Sprite temp;
    private Image image;
    private Image UpImage;
    public int x = -1;
    public int y = -1;
    bool isReveal = false;
    public bool isFlag = false;
    private void Awake()
    {

    }
    // ʹ�� Start ������ȡTextMeshPro���
    void Start()
    {
        image = GetComponent<Image>();
        SLGridUp = transform.Find("SLGridUp").gameObject;
        UpImage = transform.Find("SLGridUp").GetComponent<Image>();
    }

    // ��������¼�����ʱ���˺�����������
    public void OnPointerClick(PointerEventData eventData)
    {
        // �����Ϸ�Ƿ��Ѿ�����������Ѿ��������򲻴����κε���¼�
        if (SLisDeadorWin.Instance.isDeadorWin == false)
        {
            // ����û����������Ҽ�
            if (eventData.button == PointerEventData.InputButton.Right)
            {
                // �����ǰ����û�б��Ϊ���Ĳ��һ���ʣ������Ŀ���ʹ��
                if (isFlag == false && SLisDeadorWin.Instance.flagNum > 0)
                {
                    // ʹ��һ�����ģ�������������1
                    SLisDeadorWin.Instance.flagNum -= 1;

                    // ������ʾ����������
                    SLisDeadorWin.Instance.num.text = SLisDeadorWin.Instance.flagNum.ToString();

                    // ��ǵ�ǰ����Ϊ����
                    isFlag = true;

                    // ���浱ǰ��ʾ��ͼƬ���Ա�����ָ�
                    temp = UpImage.sprite;

                    // ��ʾ����ͼƬ
                    UpImage.sprite = Flag;

                    // �����ǰ�����ǵ���
                    if (numberToShow == -1)
                    {
                        // ������ȷ��ǵĵ�������
                        SLisDeadorWin.Instance.scores += 1;

                        // ������еĵ��׶�����ȷ��ǣ���ô���ʤ��
                        if (SLisDeadorWin.Instance.scores == MapGenerator.Instance.mineCount)
                        {
                            // ��ʾʤ���ı�
                            MapGenerator.Instance.gameObject.transform.Find("Text").gameObject.SetActive(true);

                            // �����Ϸ�Ѿ�����
                            SLisDeadorWin.Instance.isDeadorWin = true;

                            // ����ʤ��������
                            SLtimer.Instance.GameWon();
                        }
                    }
                }
                // �����ǰ�����Ѿ����Ϊ����
                else if (isFlag == true)
                {
                    // ȡ�����ģ�������������1
                    SLisDeadorWin.Instance.flagNum += 1;

                    // ������ʾ����������
                    SLisDeadorWin.Instance.num.text = SLisDeadorWin.Instance.flagNum.ToString();

                    // ȡ����ǵ�ǰ����Ϊ����
                    isFlag = false;

                    // �ָ�ԭ����ͼƬ
                    UpImage.sprite = temp;

                    // �����ǰ�����ǵ���
                    if (numberToShow == -1)
                    {
                        // ������ȷ��ǵĵ�������
                        SLisDeadorWin.Instance.scores -= 1;
                    }
                }
                else { }  // ��������������κδ���
            }

            // ����û������������
            if (eventData.button == PointerEventData.InputButton.Left)
            {
                // ����Reveal��������ʾ��ǰ����
                Reveal();
            }
        }
    }

    // ����һ����ΪReveal�Ĺ��������������Դ������ű��е��á�
    public void Reveal()
    {
        // ��鲼������'isReveal'��'isFlag'�Ƿ�Ϊfalse��
        // 'isReveal'���ܱ�ʾĳ��Ԫ�أ�������Ϸ�е�һ�����飩�Ƿ񱻽�ʾ��
        // ��'isFlag'���ܱ�ʾ��Ԫ���Ƿ񱻱�ǣ�������ɨ����Ϸ�У���
        if (isReveal == false && isFlag == false)
        {
            // 'SLGridUp'�ƺ���һ����ǰ����Ķ������ｫ������Ϊ�Ǽ���״̬�����أ���
            SLGridUp.SetActive(false);

            // ��'isReveal'����Ϊtrue����ʾ��Ԫ�����ѱ���ʾ��
            isReveal = true;

            // ����Ԫ���ϵ����֣���ʾ�洢��'numberToShow'�е����֡�
            // 'myText'�ƺ���Ԫ���ϵ�һ���ı����󣬶�'numberToShow'�����Ǹ����ĵ�����������ɨ������Ϸ�У���
            myText.text = numberToShow.ToString();

            // ���'numberToShow'�Ƿ����-1��
            // ����ǣ���'image'�ľ������Ϊ'Mine'�����ܱ�ʾ���ڵ��ף���
            // ����'myText'�ϵ���������Ϊ���ַ�����
            // ͬʱ����'SLisDeadorWin'ʵ����'isDeadorWin'��������Ϊtrue��
            // ����ܱ�ʾ��Ϸ����������Ѿ����˻�Ӯ�ˣ���
            if (numberToShow == -1)
            {
                image.sprite = Mine;
                myText.text = "";
                SLisDeadorWin.Instance.isDeadorWin = true;
            }
            MapGenerator.Instance.clickedTiles += 1;
            if (MapGenerator.Instance.clickedTiles == (MapGenerator.Instance.GetTotalTiles() - MapGenerator.Instance.mineCount))
            {
                // ��ʾʤ���ı�
                MapGenerator.Instance.gameObject.transform.Find("Text").gameObject.SetActive(true);

                // �����Ϸ�Ѿ�����
                SLisDeadorWin.Instance.isDeadorWin = true;

                // ����ʤ��������
                SLtimer.Instance.GameWon();
            }
            // ���'numberToShow'�Ƿ����0��
            // ����ǣ���'myText'�ϵ���������Ϊ���ַ�����
            // ����㼶�ṹ�Ϸ�����һ����Ϣ��ʹ��'sendAutoReveal'������'vector2'��Ϊ������
            // �����ǰԪ�ظ���û�е��ף�����������Զ���ʾ�����е�����Ԫ�ء�
            if (numberToShow == 0)
            {
                myText.text = "";
                Vector2 vector2 = new Vector2(x, y);
                SendMessageUpwards("sendAutoReveal", vector2);
            }
        }
    }

    public void AutoReveal(Vector2 vector2)
    {
        if (isReveal == false)
        {
            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    // �������ĸ��� (x, y)
                    if (i == 0 && j == 0)
                        continue;

                    int nx = x + i;
                    int ny = y + j;

                    // ���nx��ny�Ƿ�������ı߽��ڣ�����������grid��һ����ά���飬���Ĵ�С��width x height
                    if (nx >= 0 && nx < MapGenerator.Instance.mapWidth && ny >= 0 && ny < MapGenerator.Instance.mapHeight)
                    {
                        // �ж���Χ�ĸ����Ƿ���(v2x, v2y)
                        if (nx == vector2.x && ny == vector2.y)
                            Reveal();
                    }
                }
            }
            // print("");
        }
    }
}
