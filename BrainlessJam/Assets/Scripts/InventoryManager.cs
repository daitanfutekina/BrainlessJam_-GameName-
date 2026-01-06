using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InventoryManager : MonoBehaviour
{
    public List<Item> items = new List<Item>();
    public int maxSlots = 5;
    

    // This is the "reference" other scripts will use
    public bool HasItem(string nameToSearch)
    {
        return items.Exists(item => item.itemName == nameToSearch);
    }

    public bool AddItem(Item newItem)
    {
        if (items.Count >= maxSlots) return false; // Full
        if (items.Contains(newItem)) return false; // No duplicates

        items.Add(newItem);
        InventoryUI.Instance.UpdateUI(); // Tell the UI to refresh
        return true;
    }
}