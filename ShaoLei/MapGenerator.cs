using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 我们将地图生成器定义为一个 MonoBehaviour，这样我们就可以将它作为一个组件添加到游戏对象上。
public class MapGenerator : MonoBehaviour
{
    // 定义地图的宽度，高度和地雷的数量。
    public int mapWidth = 10;
    public int mapHeight = 10;
    public int mineCount = 10;
    public int cubicbig = 100;//格子的大小
    private int totalTiles;  // 私有的格子总数
    public int clickedTiles=0;
    // 根据宽度和高度计算格子总数
    public void CalculateTotalTiles(int width, int height)
    {
        totalTiles = width * height;
    }

    // 提供一个公开的方法来获取格子总数
    public int GetTotalTiles()
    {
        return totalTiles;
    }
    public GameObject prefab;
    // 定义一个二维数组来存储地图数据。-1表示有地雷，其他值表示周围地雷的数量。
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
        //计算格子总数
        CalculateTotalTiles(mapWidth, mapHeight);
        Instance = this;
    }

    // 在游戏开始时生成地图。
    private void Start()
    {
       
    }

    // 生成地图的函数。
    private void GenerateMap()
    {
        
        // 首先，我们初始化地图数组。
        map = new int[mapWidth, mapHeight];

        // 然后，我们在地图上随机地生成地雷。每生成一个地雷，我们就选择一个随机的位置，然后将那个位置的值设置为-1。
        //该方法可能有重复
        /*
        for (int i = 0; i < mineCount; i++)
        {
            int x = Random.Range(0, mapWidth);
            int y = Random.Range(0, mapHeight);
            map[x, y] = -1;
        }*/
        // 创建一个列表，存储所有可能的位置
        List<Vector2Int> positions = new List<Vector2Int>();

        // 将所有可能的位置添加到列表中
        for (int x = 0; x < mapWidth; x++)
        {
            for (int y = 0; y < mapHeight; y++)
            {
                positions.Add(new Vector2Int(x, y));
            }
        }

        // 随机生成地雷
        for (int i = 0; i < mineCount; i++)
        {
            // 如果所有的位置都已经被使用，那么就停止生成地雷
            if (positions.Count == 0)
            {
                break;
            }

            // 从列表中随机选取一个位置
            int index = Random.Range(0, positions.Count);
            Vector2Int position = positions[index];

            // 将选取的位置从列表中移除，以确保这个位置不会被再次使用
            positions.RemoveAt(index);

            // 在选取的位置上生成地雷
            map[position.x, position.y] = -1;
        }

        // 最后，我们需要计算地图上每一个格子周围的地雷数量。我们遍历每一个格子，如果它不是地雷（值不为-1），我们就计算它周围的地雷数量。
        // 计算地雷数量的方法是遍历该格子周围的所有格子，如果某个格子是地雷（值为-1），我们就将计数器加一。然后，我们将该格子的值设置为计数器的值。
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
