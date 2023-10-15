using UnityEngine;
using Unity.Netcode;
using UnityEngine.UI;

public class TryNetPlayer : NetworkBehaviour
{
    public float moveSpeed = 5f; // 小球移动速度
    public float rotateSpeed = 200f; // 小球旋转速度
    [SerializeField]
    private Text nameLabel;
    private Rigidbody rb; // 小球的 Rigidbody 组件
    private NetworkVariable<Vector3> networkPlayerPos = new NetworkVariable<Vector3>(Vector3.zero);
    private NetworkVariable<Quaternion> networkPlayerRot = new NetworkVariable<Quaternion>(Quaternion.identity);
    private NetworkVariable<int> clientId = new NetworkVariable<int>();
    private Color[] playerColors = { Color.red, Color.blue, Color.white, Color.magenta };
    public override void OnNetworkSpawn()
    {
        if (this.IsServer)
        {
            clientId.Value = (int)this.OwnerClientId;
            print("Server id:" + this.OwnerClientId); 
        }    
    }
    void Start()
    {
        rb = GetComponent<Rigidbody>(); // 获取 Rigidbody 组件
        if (this.IsClient && this.IsOwner)
        { transform.position = new Vector3(Random.Range(-5, 5), -0.345f, Random.Range(-5, 5)); }
        nameLabel.text = clientId.Value.ToString();
        Renderer renderer = GetComponent<Renderer>();
        renderer.material.color = playerColors[clientId.Value % playerColors.Length];
    }

    void Update()
    {
        if (this.IsClient && this.IsOwner)
        {
            Vector3 pos= GetTargetPos(); // 在 Update 中调用移动和旋转函数
            Quaternion rot=GetTargetRotate();
            updatePosAndRotServerRpc(pos, rot);
            rb.MovePosition(pos); // 移动小球
            rb.MoveRotation(rot); // 旋转小球
        }
        else
        { 
            rb.MovePosition(networkPlayerPos.Value);
            rb.MoveRotation(networkPlayerRot.Value);
        }
    }
    [ServerRpc]
    public void updatePosAndRotServerRpc(Vector3 pos,Quaternion rot)
    {
        networkPlayerPos.Value = pos;
        networkPlayerRot.Value = rot;
    }


    Vector3 GetTargetPos()
    {
        float moveVertical = Input.GetAxis("Vertical"); // W 和 S 键的输入值
        Vector3 movement = transform.forward * moveVertical * moveSpeed * Time.deltaTime; // 计算移动向量
        Vector3 pos = rb.position + movement;
        return pos;
    }

    Quaternion GetTargetRotate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal"); // A 和 D 键的输入值
        float turn = moveHorizontal * rotateSpeed * Time.deltaTime; // 计算旋转量
        Quaternion turnRotation = Quaternion.Euler(0f, turn, 0f); // 创建旋转四元数
        return rb.rotation * turnRotation;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("coin"))
        {
            if (this.IsOwner)
            {
                // other.gameObject.SetActive(false);
                TryNetCoin cc = other.GetComponent<TryNetCoin>();
                cc.SetActive(false);
            }
        }else if(other.gameObject.CompareTag("Player")){
            if (IsClient && IsOwner)
            {
                ulong clientId = other.GetComponent<NetworkObject>().OwnerClientId;
                UpdatePlayerMeetServerRpc(this.OwnerClientId,clientId);
            }
        }   
    }
    [ServerRpc]
    void UpdatePlayerMeetServerRpc(ulong from,ulong to)
    {
        ClientRpcParams p = new ClientRpcParams
        {
            Send = new ClientRpcSendParams
            {
                TargetClientIds=new ulong[] {to}
            }
        };
        NotifyPlayerMeetClientRpc(from,p);
    }
    [ClientRpc]
    void NotifyPlayerMeetClientRpc(ulong from, ClientRpcParams p)
    {
        if (!this.IsOwner)
        {
            Debug.Log("Meet by player" +from);
        }
    }
}
