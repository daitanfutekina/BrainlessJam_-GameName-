using UnityEngine;
using UnityEngine.SceneManagement;

public class PickupObject : MonoBehaviour
{
    [Header("Scene Interaction")]
    [SerializeField] private string minigameScene; // The scene to load
    [SerializeField] PopupUI popup;
    
    [Header("Inventory")]
    [SerializeField] InventoryManager inventoryManager;
    [SerializeField] Ingredient relevantIngredient;

    [Header("Input")]
    [SerializeField] private string pickupHotkeyName = "Pickup";

    [Header("Scene Object")]
    [SerializeField] private GameObject objectToDelete;
    [SerializeField] private bool respawnObject = false;
    [SerializeField] private float respawnTime = 10f;

    private bool playerInRange;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) 
                if (relevantIngredient.relevantTool == inventoryManager.GetSelectedIngredient()?.ingredientName)
                {
                    playerInRange = true;
                    popup.ShowPopup("Press E to Gather");
                }
                else
                {
                    if (relevantIngredient.relevantTool == "Hands")
                    {
                        playerInRange = true;
                        popup.ShowPopup("Press E to Gather");
                    }
                    else
                    {
                         Debug.Log(relevantIngredient.relevantTool);
                        popup.ShowPopup("You need a " + relevantIngredient.relevantTool + " to gather this ingredient!");
                    }    
                }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            playerInRange = false;
            popup.HidePopup();
        
    }

    private void Update()
    {
        if (!playerInRange) return;

        if (Input.GetButtonDown(pickupHotkeyName))
        {
            if (!string.IsNullOrEmpty(minigameScene))
            {
                // Load the specified minigame scene
                SceneManager.LoadScene(minigameScene);
            }
            else
            {
                Debug.LogWarning("Minigame Scene is not set!");
                popup.ShowPopup("Error: Gathering Minigame Not Set!");
            }

            if (respawnObject)
                Invoke(nameof(ReEnable), respawnTime);
            else
                Destroy(gameObject);
        }
    }

    private void ReEnable()
    {
        if (objectToDelete != null)
            objectToDelete.SetActive(true);
    }
}
