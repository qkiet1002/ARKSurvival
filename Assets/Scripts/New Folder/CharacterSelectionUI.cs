using UnityEngine;
using UnityEngine.UI;
using Unity.Netcode;

public class CharacterSelectionUI : MonoBehaviour
{
    private CharacterSelector characterSelector; // Reference to the CharacterSelector script

    public Button[] characterButtons; // Array of buttons for each character
    public Button spawnButton; // Button to confirm character selection and spawn

    void Start()
    {
        // Find the local player's CharacterSelector component
        NetworkManager.Singleton.OnClientConnectedCallback += OnClientConnected;

        // Assign button click events to the character buttons
        for (int i = 0; i < characterButtons.Length; i++)
        {
            int index = i;
            characterButtons[i].onClick.AddListener(() => OnCharacterButtonClicked(index));
        }

        // Assign the spawn button click event
        spawnButton.onClick.AddListener(OnSpawnButtonClicked);

        // Log initialization
       // Debug.Log("CharacterSelectionUI initialized.");
    }

    private void OnClientConnected(ulong clientId)
    {
        if (NetworkManager.Singleton.LocalClientId == clientId)
        {
            var playerObject = NetworkManager.Singleton.SpawnManager.GetLocalPlayerObject();
            characterSelector = playerObject.GetComponent<CharacterSelector>();

            // Log the character selector assignment
            if (characterSelector != null)
            {
                Debug.Log("CharacterSelector component found and assigned.");
            }
            else
            {
                Debug.LogError("CharacterSelector component not found on the player object.");
            }
        }
    }

    // Called when a character button is clicked
    void OnCharacterButtonClicked(int index)
    {
        if (characterSelector != null)
        {
            characterSelector.SelectCharacter(index); // Update the selected character
            Debug.Log("Character button clicked: " + index);
        }
        else
        {
            Debug.LogError("CharacterSelector is not assigned.");
        }
    }

    // Called when the spawn button is clicked
    void OnSpawnButtonClicked()
    {
        if (characterSelector != null)
        {
            characterSelector.SpawnCharacter(); // Request to spawn the selected character
            Debug.Log("Spawn button clicked.");
        }
        else
        {
            Debug.LogError("CharacterSelector is not assigned.");
        }
    }
}
