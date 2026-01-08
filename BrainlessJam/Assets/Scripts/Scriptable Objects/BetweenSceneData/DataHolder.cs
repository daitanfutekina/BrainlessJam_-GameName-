using UnityEngine;

[CreateAssetMenu(fileName = "DataHolder", menuName = "Inventory/Data Holder")]
public class DataHolder : ScriptableObject
{
    [Header("Inventory Data")]
    public Ingredient[] ingredients;

    [Header("Selection")]
    public int selectorPos;

    public void InitializeIfNeeded(int slotCount)
    {
        if (ingredients == null || ingredients.Length != slotCount)
        {
            ingredients = new Ingredient[slotCount];
        }
    }
}
