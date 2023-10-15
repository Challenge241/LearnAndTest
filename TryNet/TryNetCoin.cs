// 引入了一些基础的命名空间，使得我们可以使用基础的系统集合、Unity引擎的类和函数，以及Unity Netcode的网络行为。
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class TryNetCoin : NetworkBehaviour
{
    // 定义了一个私有的网络变量‘networkIsActive’，用于在网络上同步一个布尔值，初始值为'true'。
    private NetworkVariable<bool> networkIsActive = new NetworkVariable<bool>(true);

    // 当网络对象生成时，此方法会被调用。在这个方法里，我们设置了这个游戏对象的活动状态。
    public override void OnNetworkSpawn()
    {
        networkIsActive.OnValueChanged += (preValue,newValue)=>{
            this.gameObject.SetActive(newValue);
        };
        // 根据'networkIsActive'的值来激活或禁用这个游戏对象。
        this.gameObject.SetActive(networkIsActive.Value);
    }

    // 这个公开的方法用于设置这个游戏对象的活动状态。
    public void SetActive(bool active)
    {
        // 如果这个对象在服务器上，我们直接设置‘networkIsActive’的值。
        if (this.IsServer)
        {
            networkIsActive.Value = active;
        }
        // 如果这个对象在客户端上，我们通过一个ServerRpc来请求服务器设置‘networkIsActive’的值。
        else if (this.IsClient)
        {
            SetNetworkActiveServerRpc(active);
        }
    }

    // 定义了一个ServerRpc方法，这个方法只能在服务器上执行。客户端可以调用这个方法来发送一个请求到服务器。
    [ServerRpc(RequireOwnership =false)]
    public void SetNetworkActiveServerRpc(bool active)
    {
        // 在服务器上设置‘networkIsActive’的值。
        networkIsActive.Value = active;
    }
}


