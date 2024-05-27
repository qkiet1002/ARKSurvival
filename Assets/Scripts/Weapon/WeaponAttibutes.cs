using System.Collections;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using UnityEngine;
using Unity.Netcode;

public class WeaponAttibutes : MonoBehaviour
{
    public List<AttributesManager> attributesManagers;
    public string namePlayer;
/*    private void Start()
    {
        // Assuming this script is attached to the player, initialize attributesManager
        attributesManagers = GetComponentInChildren<AttributesManager>();
    }*/
    private void OnTriggerEnter(Collider other)
    {
   
            if (other.CompareTag("Enemy"))
            {
                other.GetComponent<AttributesManager>().TakeDamage(attributesManagers[0].attack);
               
            }
             
    }
/*    void ClientId( ServerRpcParams rpcParams = default)
    {
        ulong clientId = rpcParams.Receive.SenderClientId;
        if (NetworkManager.Singleton.ConnectedClients.TryGetValue(clientId, out var client))
        {
            var playerNetworkObject = client.PlayerObject;
            if (playerNetworkObject != null)
            {
                namePlayer = playerNetworkObject.name;
                Debug.Log(namePlayer);
            }
        }
    }*/
}
