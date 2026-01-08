using UnityEngine;
using UnityEngine.InputSystem;

public class PickupObject : MonoBehaviour
{
    [Header("Inventory")]
    [SerializeField] private InventoryManager inventoryManager;
    [SerializeField] private Ingredient ingredientToSet;

    [Header("Input (Input Actions Name)")]
    [SerializeField] private string pickupHotkeyName = "Pickup";

    [Header("Scene")]
    [SerializeField] private GameObject objectToDelete;
    [SerializeField] private bool respawnObject = false;
    [SerializeField] private float respawnTime = 10f;

    private bool playerInRange;

    void OnCollisionEnter(Collision other)
    {
        if (other.collider.CompareTag("Player"))
            playerInRange = true;
    }

    void OnCollisionExit(Collision other)
    {
        if (other.collider.CompareTag("Player"))
            playerInRange = false;
    }

    void Update()
    {
        if (!playerInRange || inventoryManager == null)
            return;

        if (Input.GetButtonDown(pickupHotkeyName))
        {
            bool added = inventoryManager.TryAddToFirstEmptySlot(ingredientToSet);

            if (added)
            {
                if (respawnObject)
                    Invoke(nameof(ReEnable), respawnTime);
                else
                    Destroy(gameObject);
            }
            else
            {
                Debug.Log("Inventory is full!");
            }
        }
    }

    void ReEnable()
    {
        objectToDelete.SetActive(true);
    }
}
