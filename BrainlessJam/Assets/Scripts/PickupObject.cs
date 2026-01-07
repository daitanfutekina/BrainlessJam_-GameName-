using UnityEngine;
using UnityEngine.InputSystem;


public class PickupObject : MonoBehaviour
{
    [Header("Inventory")]
    [SerializeField] InventoryManager inventoryManager;
    [SerializeField] Ingredient ingredientToSet;
    
    [Header("Input, make sure to define in Input Map")]
    [SerializeField] string pickupHotkeyName = "Pickup";

    [Header("Scene")]
    [SerializeField] GameObject objectToDelete;
    [SerializeField] bool respawnObject = false;
    [SerializeField] float respawnTime = 10f;
 
    void OnCollisionEnter(Collision other)
    {
        if (Input.GetButtonDown(pickupHotkeyName))
        {
            AddToNextFreeSlot(ingredientToSet);
            if (respawnObject)
            {
                Invoke("ReEnable", respawnTime);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
    void ReEnable()
    {
        objectToDelete.SetActive(false);
    }
    void Update()
    {
         Debug.Log(inventoryManager.GetSelectedIngredient());
    }
    public void AddToNextFreeSlot(Ingredient objectToAdd)
    {
        if (inventoryManager.GetSelectedIngredient() != null)
        {
            inventoryManager.GetSelectedSlot()?.SetIngredient(ingredientToSet);
            objectToDelete.SetActive(false);
            return;
        }
        else
        {
            inventoryManager.selectorPos++;
            inventoryManager.MoveSelector();
            AddToNextFreeSlot(objectToAdd);
        }
        
    }
}
