using UnityEngine;

[CreateAssetMenu(fileName = "DataHolder", menuName = "Inventory/DataHolder")]
public class DataHolder : ScriptableObject
{
    public Ingredient[] ingredients;
    public int selectorPos = 0;

    // Initialize array only if null or wrong length
    public void InitializeIfNeeded(int slotCount)
    {
        if (ingredients == null || ingredients.Length != slotCount)
        {
            ingredients = new Ingredient[slotCount];
        }
    }
}
