using UnityEngine;
// add to bush/pickup areas in the scene
// press E to pickup item
public class InteractableBush : MonoBehaviour
{
    //public Item itemToGive;
    private bool playerInRange;

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            InventoryManager playerInv = FindFirstObjectByType<InventoryManager>();
            if (playerInv.AddItem(itemToGive))
            {
                Debug.Log("Picked up " + itemToGive.itemName);
                Destroy(gameObject); // Remove bush/fruit after picking
            }
        }
    }

    private void OnTriggerEnter(Collider other) => playerInRange = other.CompareTag("Player");
    private void OnTriggerExit(Collider other) => playerInRange = false;
}