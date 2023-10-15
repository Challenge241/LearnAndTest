// ������һЩ�����������ռ䣬ʹ�����ǿ���ʹ�û�����ϵͳ���ϡ�Unity�������ͺ������Լ�Unity Netcode��������Ϊ��
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class TryNetCoin : NetworkBehaviour
{
    // ������һ��˽�е����������networkIsActive����������������ͬ��һ������ֵ����ʼֵΪ'true'��
    private NetworkVariable<bool> networkIsActive = new NetworkVariable<bool>(true);

    // �������������ʱ���˷����ᱻ���á��������������������������Ϸ����Ļ״̬��
    public override void OnNetworkSpawn()
    {
        networkIsActive.OnValueChanged += (preValue,newValue)=>{
            this.gameObject.SetActive(newValue);
        };
        // ����'networkIsActive'��ֵ���������������Ϸ����
        this.gameObject.SetActive(networkIsActive.Value);
    }

    // ��������ķ����������������Ϸ����Ļ״̬��
    public void SetActive(bool active)
    {
        // �����������ڷ������ϣ�����ֱ�����á�networkIsActive����ֵ��
        if (this.IsServer)
        {
            networkIsActive.Value = active;
        }
        // �����������ڿͻ����ϣ�����ͨ��һ��ServerRpc��������������á�networkIsActive����ֵ��
        else if (this.IsClient)
        {
            SetNetworkActiveServerRpc(active);
        }
    }

    // ������һ��ServerRpc�������������ֻ���ڷ�������ִ�С��ͻ��˿��Ե����������������һ�����󵽷�������
    [ServerRpc(RequireOwnership =false)]
    public void SetNetworkActiveServerRpc(bool active)
    {
        // �ڷ����������á�networkIsActive����ֵ��
        networkIsActive.Value = active;
    }
}


