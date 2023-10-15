using UnityEngine;
using Unity.Netcode;
using UnityEngine.UI;

public class TryNetPlayer : NetworkBehaviour
{
    public float moveSpeed = 5f; // С���ƶ��ٶ�
    public float rotateSpeed = 200f; // С����ת�ٶ�
    [SerializeField]
    private Text nameLabel;
    private Rigidbody rb; // С��� Rigidbody ���
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
        rb = GetComponent<Rigidbody>(); // ��ȡ Rigidbody ���
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
            Vector3 pos= GetTargetPos(); // �� Update �е����ƶ�����ת����
            Quaternion rot=GetTargetRotate();
            updatePosAndRotServerRpc(pos, rot);
            rb.MovePosition(pos); // �ƶ�С��
            rb.MoveRotation(rot); // ��תС��
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
        float moveVertical = Input.GetAxis("Vertical"); // W �� S ��������ֵ
        Vector3 movement = transform.forward * moveVertical * moveSpeed * Time.deltaTime; // �����ƶ�����
        Vector3 pos = rb.position + movement;
        return pos;
    }

    Quaternion GetTargetRotate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal"); // A �� D ��������ֵ
        float turn = moveHorizontal * rotateSpeed * Time.deltaTime; // ������ת��
        Quaternion turnRotation = Quaternion.Euler(0f, turn, 0f); // ������ת��Ԫ��
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
