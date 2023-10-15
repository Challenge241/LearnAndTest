using UnityEngine;
using Unity.Netcode;
public class TryNetManager : MonoBehaviour
{
    [SerializeField]
    private GameObject playerPrefab; // 玩家预制体

    [SerializeField]
    private GameObject coinPrefab;   // 金币预制体

    [SerializeField]
    private int numberOfCoins = 10;   // 要生成的金币数量

    [SerializeField]
    private Vector3 spawnAreaSize;   // 生成区域的大小

    private void Start()
    {
        NetworkManager.Singleton.OnClientConnectedCallback += (id) => { print("Connected id:" + id); };
        NetworkManager.Singleton.OnClientDisconnectCallback += (id) => { print("Disconnect id:" + id); };
        NetworkManager.Singleton.OnServerStarted += () => { 
            print("Server Started");
            createA();
        };
    }
    private void createA(){
        // 创建玩家//后来将此任务交给了NetworkManager
        //Instantiate(playerPrefab, new Vector3(0f, -0.345f, 0f), Quaternion.identity);

        // 创建金币
        for (int i = 0; i < numberOfCoins; i++)
        {
            Vector3 randomPosition = new Vector3(
                Random.Range(-spawnAreaSize.x / 2, spawnAreaSize.x / 2),
                -0.345f, // 设置金币的Y坐标（高度）
                Random.Range(-spawnAreaSize.z / 2, spawnAreaSize.z / 2)
            );

            GameObject ob=Instantiate(coinPrefab, randomPosition, Quaternion.identity);
            ob.GetComponent<NetworkObject>().Spawn();
        }
    }
    public void OnStartServerBtnClick()
    {
        if (NetworkManager.Singleton.StartServer()) { print("A"); };
    }
    public void OnStartClientBtnClick()
    {
        if (NetworkManager.Singleton.StartClient()) { print("B"); };
    }
    public void OnStartHostBtnClick()
    {
        if (NetworkManager.Singleton.StartHost()) { print("C"); };
    }
    public void OnShotdownNetworkBtnClick()
    {
        NetworkManager.Singleton.Shutdown();
        print("shut down");
    }
}
