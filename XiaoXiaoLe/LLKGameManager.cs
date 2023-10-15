// ������Ҫ�������ռ�
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ����һ��������LLKGameManager�࣬�̳���Unity3D�Ļ���MonoBehaviour
public class LLKGameManager : MonoBehaviour
{
    // Ҫ������������Ʒ����
    private GameSweet pressedSweet;
    private GameSweet enteredSweet;
    private bool gameOver=false;
    // ����һ��������ö������SweetsType����ʾ��Ʒ������
    public enum SweetsType
    {
        EMPTY,          // ��
        NORMAL,         // ��ͨ
        BARRIER,        // �ϰ�
        ROW_CLEAR,      // ������
        COLUMN_CLEAR,   // ������
        COUNT           // �������
    }
    // ��ƷԤ�����ֵ䣬ͨ����Ʒ�������õ���Ӧ����Ʒ��Ϸ����
    public Dictionary<SweetsType, GameObject> sweetPrefabDict;

    // �ṹ��SweetPrefab��������Ʒ���ͺ�Ԥ����
    [System.Serializable] // ʹ�ṹ��������л���������Unity�༭���оͿ��Կ������༭����ṹ��
    public struct SweetPrefab
    {
        public SweetsType type;   // ��Ʒ����
        public GameObject prefab; // Ԥ����
    }
    public SweetPrefab[] sweetPrefabs; // ��ƷԤ��������

    // ����ʵ����
    private static LLKGameManager _instance;
    public static LLKGameManager Instance
    {
        get { return _instance; }
        set { _instance = value; }
    }
    private void Awake()
    {
        _instance = this; // ��Awake���������õ�����ʵ��
    }
    // �������������
    public int xColumn;
    public int yRow;
    public GameObject gridPrefabs; // ����Ԥ����
    // ���ʱ��
    public float fillTime=0.1f;
    public float fillTimeMove = 0.39f;
    // ��Ʒ���飬�洢��Ʒ�ű��������ͬ��Ʒ��������ֵ��ͬ�Ľű�
    private GameSweet[,] sweets;
    public GameSweet CreateNewSweet(int x, int y, SweetsType type)
    {
        GameObject newSweet = Instantiate(sweetPrefabDict[type], CorrectPositon(x, y), Quaternion.identity);
        newSweet.transform.parent = transform;

        sweets[x, y] = newSweet.GetComponent<GameSweet>();
        sweets[x, y].Init(x, y, this, type);

        return sweets[x, y];
    }
    // Start��������Ϸ��ʼǰ�ĵ�һ֡����ʱ����
    void Start()
    {
        // �ֵ�ʵ����
        sweetPrefabDict = new Dictionary<SweetsType, GameObject>();
        for (int i = 0; i < sweetPrefabs.Length; i++)
        {
            // ����ֵ��в���������Ʒ���ͣ�����ӵ��ֵ���
            if (!sweetPrefabDict.ContainsKey(sweetPrefabs[i].type))
            {
                sweetPrefabDict.Add(sweetPrefabs[i].type, sweetPrefabs[i].prefab);
            }
        }

        // ��ͼʵ����

        //��һ��
        /*
        for (int x=-xColumn/2; x<xColumn/2;x++)
        {
            for (int y=-yRow/2;y<yRow/2;y++)
            {
                GameObject chocolate = Instantiate(gridPrefabs, new Vector2(x, y), Quaternion.identity);
                chocolate.transform.SetParent(transform);//��LLKGameManager��Ϊ�丸����
            }
        }*/
        // ��������
        for (int x = 0; x < xColumn; x++)
        {
            for (int y = 0; y < yRow; y++)
            {
                // �����µ�������󣬲�������λ�ú͸�����
                GameObject chocolate = Instantiate(gridPrefabs, CorrectPositon(x, y), Quaternion.identity);
                chocolate.transform.SetParent(transform);
            }
        }
        //��Ʒʵ����
        // ��ʼ��һ����ά�������洢��Ϸ�е���Ʒ������Ĵ�С��xColumn��yRow����
        sweets = new GameSweet[xColumn, yRow];
        // ʹ��Ƕ��ѭ�������������ά����
        for (int x = 0; x < xColumn; x++)
        {
            for (int y = 0; y < yRow; y++)
            {
                /*
                // ʹ��Instantiate��������һ���µ���Ʒ��Ϸ������������ģ�����ֵ�sweetPrefabDict����SweetsType.NORMAL��Ӧ����Ϸ����
                // ����Ʒ��λ����ͨ������CorrectPositon��������ģ�������������x��y��ֵ������һ������Ϸ����ȷ��λ��
                // Quaternion.identity��ʾû���κ���ת
                //CorrectPositon(x, y)
                GameObject newSweet = Instantiate(sweetPrefabDict[SweetsType.NORMAL],Vector3.zero , Quaternion.identity);
                // ��������Ʒ�ĸ�����Ϊ��ǰ����Ϸ����LLKGameManager��
                newSweet.transform.SetParent(transform);
                // ��ȡ����Ʒ�ϵ�GameSweet�����������洢�ڶ�ά����sweets�ж�Ӧ��λ��
                sweets[x, y] = newSweet.GetComponent<GameSweet>();
                // ʹ��Init������ʼ����Ʒ�����ԣ��������������е�λ��(x, y)���������LLKGameManager����this�����Լ������ͣ�SweetsType.NORMAL��
                sweets[x, y].Init(x, y, this, SweetsType.NORMAL);
                if (sweets[x, y].CanMove())
                {
                    sweets[x, y].MovedComponent.Move(x,y,fillTime);
                }
                if (sweets[x, y].CanColor())
                {
                    sweets[x, y].ColoredComponent.SetColor((ColorSweet.ColorType)(Random.Range(0,sweets[x,y].ColoredComponent.NumColors)));
                }*/
                CreateNewSweet(x, y, SweetsType.EMPTY);
            }
        }
        ClearAllMatchedSweet();
        StartCoroutine(AllFill());
    }
    // ����һ����������CorrectPosition����������������Ϊ������x, y��
    // �����������һ��Vector2���͵Ķ��󣬱�ʾ�ڶ�ά�ռ��е�һ��λ��
    public Vector2 CorrectPositon(int x, int y)
    {
        // ������µ�x��y��λ��
        // ͨ���ӵ�ǰ�����xλ�ã�transform.position.x����ȥ������һ�루xColumn / 2f���ټ��������x���õ��µ�xλ��
        // ͬ���ģ��ӵ�ǰ�����yλ�ã�transform.position.y����ȥ������һ�루yRow / 2f���ټ��������y���õ��µ�yλ��
        // ��������Ŀ���ǽ���ά�����е�����ת��Ϊ��Ϸ�����е�ʵ��λ��
        return new Vector2(transform.position.x - xColumn / 2f + x, transform.position.y + yRow / 2f - y);
    }
    // ��Ʒ�Ƿ����ڵ��жϷ���
    private bool IsFriend(GameSweet sweet1, GameSweet sweet2)
    {
        return (sweet1.X == sweet2.X && Mathf.Abs(sweet1.Y - sweet2.Y) == 1) || (sweet1.Y == sweet2.Y && Mathf.Abs(sweet1.X - sweet2.X) == 1);
    }

    public void PressSweet(GameSweet sweet)
    {
        if (gameOver)
        {
            return;
        }
        pressedSweet = sweet;
    }

    public void EnterSweet(GameSweet sweet)
    {
        if (gameOver)
        {
            return;
        }
        enteredSweet = sweet;
    }

    public void ReleaseSweet()
    {
        if (gameOver)
        {
            return;
        }
        if (IsFriend(pressedSweet, enteredSweet))
        {
            //print("IsFriend");
            ExchangeSweets(pressedSweet, enteredSweet);
        }
    }
    private void ExchangeSweets(GameSweet sweet1, GameSweet sweet2)
    {
        // �ж�������Ʒ�Ƿ���ƶ�
        if (sweet1.CanMove() && sweet2.CanMove())
        {
            // ����������Ʒ����Ʒ�����е�λ��
            sweets[sweet1.X, sweet1.Y] = sweet2;
            sweets[sweet2.X, sweet2.Y] = sweet1;

            // �жϽ������Ƿ����ƥ�����Ʒ
            if (MatchSweets(sweet1, sweet2.X, sweet2.Y) != null ||
                MatchSweets(sweet2, sweet1.X, sweet1.Y) != null)
            {

                // ��¼��Ʒ1��λ��
                int tempX = sweet1.X;
                int tempY = sweet1.Y;

                // ִ����Ʒ��������
                sweet1.MovedComponent.Move(sweet2.X, sweet2.Y, fillTimeMove);
                sweet2.MovedComponent.Move(tempX, tempY, fillTimeMove);
                // �������ƥ�����Ʒ�����
                ClearAllMatchedSweet();
                StartCoroutine(AllFill());
                // ���ð��µ���Ʒ�ͽ������Ʒ
                pressedSweet = null;
                enteredSweet = null;
            }
            else
            {
                print("��Ʒ��ƥ��");
            }
            
        }
    }
    public List<GameSweet> MatchSweets(GameSweet sweet, int newX, int newY)
    {
        if (sweet.CanColor())
        {
            ColorSweet.ColorType color = sweet.ColoredComponent.Color;
            List<GameSweet> matchRowSweets = new List<GameSweet>();
            List<GameSweet> matchLineSweets = new List<GameSweet>();
            List<GameSweet> finishedMatchingSweets = new List<GameSweet>();
            //��ƥ��
            matchRowSweets.Add(sweet);
            //i=0��������i=1��������
            for (int i = 0; i <= 1; i++)
            {
                for (int xDistance = 1; xDistance < xColumn; xDistance++)
                {
                    int x;
                    if (i == 0)
                    {
                        x = newX - xDistance;
                    }
                    else
                    {
                        x = newX + xDistance;
                    }
                    if (x < 0 || x >= xColumn)
                    {
                        break;
                    }
                    if (sweets[x, newY].CanColor() && sweets[x, newY].ColoredComponent.Color == color)
                    {
                        matchRowSweets.Add(sweets[x, newY]);
                    }
                    else
                    {
                        //print(xColumn);
                        break;
                    }
                }
            }
            if (matchRowSweets.Count >= 3)
            {
                for (int i = 0; i < matchRowSweets.Count; i++)
                {
                    finishedMatchingSweets.Add(matchRowSweets[i]);
                }
            }
            //��ƥ��
            matchLineSweets.Add(sweet);
            //i=0��������i=1��������
            for (int i = 0; i <= 1; i++)
            {
                for (int yDistance = 1; yDistance < yRow; yDistance++)
                {
                    int y;
                    if (i == 0)
                    {
                        y = newY - yDistance;
                    }
                    else
                    {
                        y = newY + yDistance;
                    }
                    if (y < 0 || y >= yRow)
                    {
                        break;
                    }

                    if (sweets[newX, y].CanColor() && sweets[newX, y].ColoredComponent.Color == color)
                    {
                        matchLineSweets.Add(sweets[newX, y]);
                    }
                    else
                    {
                        break;
                    }
                }
            }
            if (matchLineSweets.Count >= 3)
            {
                for (int i = 0; i < matchLineSweets.Count; i++)
                {
                    finishedMatchingSweets.Add(matchLineSweets[i]);
                }
            }
            if (finishedMatchingSweets.Count >= 3)
            {
                return finishedMatchingSweets;
            }
        }
        return null;
    }
    //�������
    public bool ClearSweet(int x, int y)
    {

        if (sweets[x, y].CanClear() && !sweets[x, y].ClearedComponent.IsClearing)
        {
            sweets[x, y].ClearedComponent.Clear();
            CreateNewSweet(x, y, SweetsType.EMPTY);
            return true;
        }
        return false;
    }
    //����һ���������÷��������������ƥ�����Ʒ
    private bool ClearAllMatchedSweet()
    {
        bool needRefill = false; //һ����־����ʾ�Ƿ���Ҫ����µ���Ʒ

        //����������Ϸ���
        for (int y = 0; y < yRow; y++)
        {
            for (int x = 0; x < xColumn; x++)
            {
                //��鵱ǰ��Ʒ�Ƿ�������
                if (sweets[x, y].CanClear())
                {
                    //�������������ͻ�ȡ����ƥ�����Ʒ���б�
                    List<GameSweet> matchList = MatchSweets(sweets[x, y], x, y);

                    //�����ȡ����ƥ�����Ʒ�б�
                    if (matchList != null)
                    {
                        //����ƥ�����Ʒ�б����ÿһ��ƥ�����Ʒ
                        for (int i = 0; i < matchList.Count; i++)
                        {
                            //����ɹ����һ����Ʒ���ͽ� needRefill ��Ϊ true
                            if (ClearSweet(matchList[i].X, matchList[i].Y))
                            {
                                needRefill = true;
                            }
                        }
                    }
                }
            }
        }
        //�����Ƿ���Ҫ����µ���Ʒ�ı�־
        return needRefill;
    }
    public IEnumerator AllFill()
    {
        bool needRefill = true;

        while (needRefill)
        {
            //yield return new WaitForSeconds(fillTime);
            while (Fill())
            {
                yield return new WaitForSeconds(fillTime);
            }

            //������������Ѿ�ƥ��õ���Ʒ
            needRefill = ClearAllMatchedSweet();
        }
    }
    public bool Fill()
    {
        bool filledNotFinished = false; // �жϱ�������Ƿ����

        // ��ֱ�������
        for (int y = yRow - 2; y >= 0; y--)
        {
            for (int x = 0; x < xColumn; x++)
            {
                GameSweet sweet = sweets[x, y]; // �õ���ǰԪ��λ�õ���Ʒ����

                if (sweet.CanMove()) // ����޷��ƶ������޷��������
                {
                    GameSweet sweetBelow = sweets[x, y + 1];

                    if (sweetBelow.Type == SweetsType.EMPTY) // ��ֱ���
                    {
                        Destroy(sweetBelow.gameObject);
                        sweet.MovedComponent.Move(x, y + 1, fillTime);
                        sweets[x, y + 1] = sweet;
                        CreateNewSweet(x, y, SweetsType.EMPTY);
                        filledNotFinished = true;
                    }
                }
            }
        }

        // �����ŵ��������������ȱ��λ��
        for (int x = 0; x < xColumn; x++)
        {
            GameSweet sweet = sweets[x, 0];

            if (sweet.Type == SweetsType.EMPTY)
            {
                GameObject newSweet = Instantiate(sweetPrefabDict[SweetsType.NORMAL], CorrectPositon(x, -1), Quaternion.identity);
                newSweet.transform.parent = transform;

                sweets[x, 0] = newSweet.GetComponent<GameSweet>();
                sweets[x, 0].Init(x, -1, this, SweetsType.NORMAL);
                sweets[x, 0].MovedComponent.Move(x, 0, fillTime);
                sweets[x, 0].ColoredComponent.SetColor((ColorSweet.ColorType)Random.Range(0, sweets[x, 0].ColoredComponent.NumColors));
                filledNotFinished = true;
            }
        }

        return filledNotFinished;
    }
}
