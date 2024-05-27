using UnityEngine;
using Unity.Netcode;
using UnityEngine.UI;

public class CharacterSelector : NetworkBehaviour
{
    public GameObject[] characterPrefabs; // Array of character prefabs to select from
    public Transform spawnPoint; // Spawn point for the characters

    private int selectedCharacterIndex = 0;

    public void SelectCharacter(int index)
    {
        if (index >= 0 && index < characterPrefabs.Length)
        {
            selectedCharacterIndex = index;
            Debug.Log("Character selected: " + characterPrefabs[index].name);
        }
    }

    public void SpawnCharacter()
    {
        if (IsOwner)
        {
            SpawnCharacterServerRpc(selectedCharacterIndex);
        }
    }

    [ServerRpc(RequireOwnership = false)]
    private void SpawnCharacterServerRpc(int index, ServerRpcParams serverRpcParams = default)
    {
        var clientId = serverRpcParams.Receive.SenderClientId;
        var playerObject = NetworkManager.Singleton.ConnectedClients[clientId].PlayerObject;

        if (playerObject != null)
        {
            var spawnPos = spawnPoint.position;

            GameObject characterInstance = Instantiate(characterPrefabs[index], spawnPos, Quaternion.identity);
            NetworkObject networkObject = characterInstance.GetComponent<NetworkObject>();
            networkObject.SpawnWithOwnership(clientId);

            Debug.Log("Character spawned: " + characterPrefabs[index].name + " for Client ID: " + clientId);

            // Assign the character instance as the player object for this client
            NetworkManager.Singleton.ConnectedClients[clientId].PlayerObject = networkObject;
        }
    }
}
