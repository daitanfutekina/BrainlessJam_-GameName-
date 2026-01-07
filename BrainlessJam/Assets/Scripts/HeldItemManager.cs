using UnityEngine;

public class HeldItemManager : MonoBehaviour
{
    [SerializeField] InventoryManager inventoryManager;
    [SerializeField] Transform modelHolder;

    Ingredient currentIngredient;
    Ingredient previousIngredient;

    GameObject currentHeldObject;

    void Update()
    {
        currentIngredient = inventoryManager.GetSelectedIngredient();

        // If the selected ingredient hasn't changed, do nothing
        if (currentIngredient == previousIngredient)
            return;

        // Ingredient changed → update held item
        UpdateHeldItem();

        previousIngredient = currentIngredient;
    }

    void UpdateHeldItem()
    {
        // Destroy old object if it exists
        if (currentHeldObject != null)
        {
            Destroy(currentHeldObject);
            currentHeldObject = null;
        }

        // If nothing selected, we're done
        if (currentIngredient == null)
            return;

        // Spawn new object
        currentHeldObject = Instantiate(
            currentIngredient.modelPrefab,
            modelHolder
        );

        currentHeldObject.transform.localPosition = Vector3.zero;
        currentHeldObject.transform.localRotation = Quaternion.identity;
    }
}
