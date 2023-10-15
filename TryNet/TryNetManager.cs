using UnityEngine;
using Unity.Netcode;
public class TryNetManager : MonoBehaviour
{
    [SerializeField]
    private GameObject playerPrefab; // ���Ԥ����

    [SerializeField]
    private GameObject coinPrefab;   // ���Ԥ����

    [SerializeField]
    private int numberOfCoins = 10;   // Ҫ���ɵĽ������

    [SerializeField]
    private Vector3 spawnAreaSize;   // ��������Ĵ�С

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
        // �������//�����������񽻸���NetworkManager
        //Instantiate(playerPrefab, new Vector3(0f, -0.345f, 0f), Quaternion.identity);

        // �������
        for (int i = 0; i < numberOfCoins; i++)
        {
            Vector3 randomPosition = new Vector3(
                Random.Range(-spawnAreaSize.x / 2, spawnAreaSize.x / 2),
                -0.345f, // ���ý�ҵ�Y���꣨�߶ȣ�
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
