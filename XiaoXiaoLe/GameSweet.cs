using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ����һ����ΪGameSweet�Ĺ����࣬���̳���MonoBehaviour�࣬MonoBehaviour��Unity3D�����нű��Ļ���
public class GameSweet : MonoBehaviour
{
    // ��������˽����������x��y���������ڱ�ʾ��Ϸ���ǹ���λ��
    private int x;
    private int y;

    // ����������������X��Y�����ǵ�get�������ڻ�ȡx��y��ֵ
    // X��set�����ڸ�x��ֵǰ�����ǹ��Ƿ�����ƶ���������ԣ��Ž��и�ֵ
    // Y��set����ֱ�Ӷ�y���и�ֵ�������κμ��
    public int X { get => x; set { if (CanMove()) { x = value; } } }
    public int Y { get => y; set { if (CanMove()) { y = value; } } }

    // ����һ��������SweetsType���͵�����Type����get��set�����ֱ����ڻ�ȡ������type��ֵ
    public LLKGameManager.SweetsType Type { get => type; set => type = value; }

    // ����һ��������MovedSweet���͵�����MovedComponent����get�������ڻ�ȡmovedComponent��ֵ��û�ж���set����
   

    // ����һ��˽�е�SweetsType���͵ı���type���������ڱ�ʾ�ǹ�������
    private LLKGameManager.SweetsType type;

    // ʹ��HideInInspector���Ա��llkGameManager���⽫��Unity��Inspector��������ش��ֶ�
    // ����һ��������LLKGameManager���͵ı���llkGameManager���������ڹ�����Ϸ�ĸ���״̬
    [HideInInspector]
    public LLKGameManager llkGameManager;

    // ����һ��˽�е�MovedSweet���͵ı���movedComponent���������ڿ����ǹ����ƶ�
    private MovedSweet movedComponent;
    public MovedSweet MovedComponent { get => movedComponent; }


    private ColorSweet coloredComponent;
    public ColorSweet ColoredComponent { get => coloredComponent; }
    private ClearedSweet clearedComponent;
    public ClearedSweet ClearedComponent
    {
        get
        {
            return clearedComponent;
        }
    }


    // ����һ��������CanMove���������movedComponent��Ϊnull����ʾ�ǹ������ƶ�������true
    public bool CanMove()
    {
        return movedComponent != null;
    }
    public bool CanColor()
    {
        return coloredComponent != null;
    }
    public bool CanClear()
    {
        // �ж���Ʒ�Ƿ���� ClearedComponent ���
        return clearedComponent != null;
    }
    // ����һ��������Init���������ڳ�ʼ���ǹ������ԣ�����λ�á���������Ϸ���������ǹ�����
    public void Init(int _x, int _y, LLKGameManager _llkgameManager, LLKGameManager.SweetsType _type)
    {
        x = _x;
        y = _y;
        llkGameManager = _llkgameManager;
        type = _type;
    }
    private void Awake()
    {
        movedComponent = GetComponent<MovedSweet>();
        coloredComponent = GetComponent<ColorSweet>();
        clearedComponent = GetComponent<ClearedSweet>();
    }
    private void OnMouseEnter()
    {
        // ����������Ʒʱ��֪ͨ��Ϸ������
        llkGameManager.EnterSweet(this);
    }

    private void OnMouseDown()
    {
        // ����갴����Ʒʱ��֪ͨ��Ϸ������
        llkGameManager.PressSweet(this);
    }

    private void OnMouseUp()
    {
        // ������ɿ���Ʒʱ��֪ͨ��Ϸ������
        llkGameManager.ReleaseSweet();
    }
    // Start��������Ϸ��ʼǰ�ĵ�һ֡����ʱ���ã�����Ϊ�գ���Ҫ��ʵ��ʱ��д����Ĵ���
    void Start()
    {

    }

    // Update������ÿһ֡����ʱ���ã�����Ϊ�գ���Ҫ��ʵ��ʱ��д����Ĵ���
    void Update()
    {

    }
}
