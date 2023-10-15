using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ���ǽ���ͼ����������Ϊһ�� MonoBehaviour���������ǾͿ��Խ�����Ϊһ�������ӵ���Ϸ�����ϡ�
public class MapGenerator : MonoBehaviour
{
    // �����ͼ�Ŀ�ȣ��߶Ⱥ͵��׵�������
    public int mapWidth = 10;
    public int mapHeight = 10;
    public int mineCount = 10;
    public int cubicbig = 100;//���ӵĴ�С
    private int totalTiles;  // ˽�еĸ�������
    public int clickedTiles=0;
    // ���ݿ�Ⱥ͸߶ȼ����������
    public void CalculateTotalTiles(int width, int height)
    {
        totalTiles = width * height;
    }

    // �ṩһ�������ķ�������ȡ��������
    public int GetTotalTiles()
    {
        return totalTiles;
    }
    public GameObject prefab;
    // ����һ����ά�������洢��ͼ���ݡ�-1��ʾ�е��ף�����ֵ��ʾ��Χ���׵�������
    public int[,] map;
    private static MapGenerator instance;
    public static MapGenerator Instance
    {
        get
        {
            return instance;
        }

        set
        {
            instance = value;
        }
    }
    private void Awake()
    {
        GenerateMap();
        //�����������
        CalculateTotalTiles(mapWidth, mapHeight);
        Instance = this;
    }

    // ����Ϸ��ʼʱ���ɵ�ͼ��
    private void Start()
    {
       
    }

    // ���ɵ�ͼ�ĺ�����
    private void GenerateMap()
    {
        
        // ���ȣ����ǳ�ʼ����ͼ���顣
        map = new int[mapWidth, mapHeight];

        // Ȼ�������ڵ�ͼ����������ɵ��ס�ÿ����һ�����ף����Ǿ�ѡ��һ�������λ�ã�Ȼ���Ǹ�λ�õ�ֵ����Ϊ-1��
        //�÷����������ظ�
        /*
        for (int i = 0; i < mineCount; i++)
        {
            int x = Random.Range(0, mapWidth);
            int y = Random.Range(0, mapHeight);
            map[x, y] = -1;
        }*/
        // ����һ���б��洢���п��ܵ�λ��
        List<Vector2Int> positions = new List<Vector2Int>();

        // �����п��ܵ�λ����ӵ��б���
        for (int x = 0; x < mapWidth; x++)
        {
            for (int y = 0; y < mapHeight; y++)
            {
                positions.Add(new Vector2Int(x, y));
            }
        }

        // ������ɵ���
        for (int i = 0; i < mineCount; i++)
        {
            // ������е�λ�ö��Ѿ���ʹ�ã���ô��ֹͣ���ɵ���
            if (positions.Count == 0)
            {
                break;
            }

            // ���б������ѡȡһ��λ��
            int index = Random.Range(0, positions.Count);
            Vector2Int position = positions[index];

            // ��ѡȡ��λ�ô��б����Ƴ�����ȷ�����λ�ò��ᱻ�ٴ�ʹ��
            positions.RemoveAt(index);

            // ��ѡȡ��λ�������ɵ���
            map[position.x, position.y] = -1;
        }

        // ���������Ҫ�����ͼ��ÿһ��������Χ�ĵ������������Ǳ���ÿһ�����ӣ���������ǵ��ף�ֵ��Ϊ-1�������Ǿͼ�������Χ�ĵ���������
        // ������������ķ����Ǳ����ø�����Χ�����и��ӣ����ĳ�������ǵ��ף�ֵΪ-1�������Ǿͽ���������һ��Ȼ�����ǽ��ø��ӵ�ֵ����Ϊ��������ֵ��
        for (int x = 0; x < mapWidth; x++)
        {
            for (int y = 0; y < mapHeight; y++)
            {
                if (map[x, y] == -1) continue;
                int count = 0;
                for (int dx = -1; dx <= 1; dx++)
                {
                    for (int dy = -1; dy <= 1; dy++)
                    {
                        if (dx == 0 && dy == 0) continue;
                        int nx = x + dx;
                        int ny = y + dy;
                        if (nx >= 0 && nx < mapWidth && ny >= 0 && ny < mapHeight && map[nx, ny] == -1)
                        {
                            count++;
                        }
                    }
                }
                map[x, y] = count;
            }
        }

        for (int y = 0; y < mapHeight; y++)
        {
            for (int x = 0; x < mapWidth; x++)
            {
                GameObject SLGrid = Instantiate(prefab, CorrectPositon(x, y), Quaternion.identity);
                SLGrid.GetComponent<SLGrid>().numberToShow = map[x, y];
                SLGrid.GetComponent<SLGrid>().x = x;
                SLGrid.GetComponent<SLGrid>().y = y;
                SLGrid.transform.SetParent(transform);
            }
        }
    }
    public Vector2 CorrectPositon(int x, int y)
    {
        return new Vector2(transform.position.x - mapWidth* cubicbig / 2f + x* cubicbig, transform.position.y - mapHeight* cubicbig / 2f + y* cubicbig);
    }
    public void sendAutoReveal(Vector2 vector2)
    {
        BroadcastMessage("AutoReveal",vector2);
    }
}
