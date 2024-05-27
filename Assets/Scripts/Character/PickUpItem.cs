using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using TMPro;

public class PickUpItem : NetworkBehaviour
{
    public GameObject pickupButton; // Tham chiếu đến nút nhặt
    public GameObject weaponPrefab; // Prefab của vũ khí
    private WeaponAttachment weaponAttachment; // Tham chiếu đến WeaponAttachment script trên người chơi
    public MeshCollider meshCollider;
    private bool isPlayerInRange = false; // vùng hiển thị button

    void Start()
    {
        meshCollider = gameObject.GetComponent<MeshCollider>();
        pickupButton.SetActive(false);
    }

    void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.E))
        {
            if (IsClient)
            {
                PickupItemServerRpc();
 
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            pickupButton.SetActive(true);
            isPlayerInRange = true;
            weaponAttachment = other.GetComponentInChildren<WeaponAttachment>();
            if (weaponAttachment == null)
            {
                Debug.LogWarning("WeaponAttachment script not found on Player.");
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            pickupButton.SetActive(false);
            isPlayerInRange = false;
        }
    }

    [ServerRpc(RequireOwnership = false)]
    void PickupItemServerRpc(ServerRpcParams rpcParams = default)
    {
        ulong clientId = rpcParams.Receive.SenderClientId;
        if (NetworkManager.Singleton.ConnectedClients.TryGetValue(clientId, out var client))
        {
            var playerNetworkObject = client.PlayerObject;
            if (playerNetworkObject != null)
            {
                Debug.Log(playerNetworkObject);
                weaponAttachment = playerNetworkObject.GetComponentInChildren<WeaponAttachment>();
                if (weaponAttachment != null && weaponPrefab != null)
                {
                    meshCollider.enabled = false;
                    weaponAttachment.weapon = weaponPrefab;
                    weaponAttachment.AttachWeapon();
                }

                PickupItemClientRpc(playerNetworkObject.NetworkObjectId);
            }
        }

        pickupButton.SetActive(false);
        isPlayerInRange = false;
        gameObject.SetActive(false);
    }

    [ClientRpc]
    void PickupItemClientRpc(ulong playerNetworkObjectId, ClientRpcParams rpcParams = default)
    {
        if (NetworkManager.Singleton.SpawnManager.SpawnedObjects.TryGetValue(playerNetworkObjectId, out var playerNetworkObject))
        {
            weaponAttachment = playerNetworkObject.GetComponentInChildren<WeaponAttachment>();
            if (weaponAttachment != null && weaponPrefab != null)
            {
                meshCollider.enabled = false;
                weaponAttachment.weapon = weaponPrefab;
                weaponAttachment.AttachWeapon();
            }
        }

        pickupButton.SetActive(false);
        isPlayerInRange = false;
        gameObject.SetActive(false);
    }
}
