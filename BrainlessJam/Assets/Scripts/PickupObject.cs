using UnityEngine;

public class PickupObject : MonoBehaviour
{
    [Header("Inventory")]
    [SerializeField] private InventoryManager inventoryManager;
    [SerializeField] private Ingredient ingredientToSet;

    [Header("Input")]
    [SerializeField] private string pickupHotkeyName = "Pickup";

    [Header("Scene")]
    [SerializeField] private GameObject objectToDelete;
    [SerializeField] private bool respawnObject = false;
    [SerializeField] private float respawnTime = 10f;

    private Ingredient selectedItem;
    
    private bool playerInRange;

    private void Start()
    {
        selectedItem = inventoryManager.GetSelectedIngredient();
    }
    
    
    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.CompareTag("Player"))
            playerInRange = true;
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.collider.CompareTag("Player"))
            playerInRange = false;
    }

    private void Update()
    {
        if (!playerInRange || inventoryManager == null) return;

        if (Input.GetButtonDown(pickupHotkeyName))
        {
            bool added = (inventoryManager.TryAddToFirstEmptySlot(ingredientToSet) && ingredientToSet.relevantTool == selectedItem.ingredientName);
            

            if (added)
            {
                if (respawnObject)
                    Invoke(nameof(ReEnable), respawnTime);
                else
                    Destroy(gameObject);
            }
            else
            {
                Debug.Log("Inventory Full");
            }
        }
    }

    private void ReEnable()
    {
        objectToDelete.SetActive(true);
    }
}
