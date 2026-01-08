using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] private RectTransform selector;
    [SerializeField] private DataHolder dataHolder;

    private SlotManager[] slots;

    void Awake()
    {
        slots = GetComponentsInChildren<SlotManager>(true);

        if (dataHolder == null)
        {
            Debug.LogError("InventoryManager: DataHolder not assigned.");
            return;
        }

        dataHolder.InitializeIfNeeded(slots.Length);

        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].Initialize(dataHolder, i);
        }
    }

    void Update()
    {
        if (slots == null || slots.Length == 0)
            return;

        ScrollManagement();
        MoveSelector();
    }

    void ScrollManagement()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");

        if (scroll > 0)
            dataHolder.selectorPos++;
        else if (scroll < 0)
            dataHolder.selectorPos--;

        if (dataHolder.selectorPos < 0)
            dataHolder.selectorPos = slots.Length - 1;
        else if (dataHolder.selectorPos >= slots.Length)
            dataHolder.selectorPos = 0;
    }

    void MoveSelector()
    {
        selector.position = slots[dataHolder.selectorPos].transform.position;
    }

    // 🔹 NEW: Safe accessor for other scripts (PickupObject, etc.)
    public int GetSelectedIndex()
    {
        return dataHolder.selectorPos;
    }

    public SlotManager GetSelectedSlot()
    {
        return slots[dataHolder.selectorPos];
    }

    public Ingredient GetSelectedIngredient()
    {
        return dataHolder.ingredients[dataHolder.selectorPos];
    }

    public bool TryAddToFirstEmptySlot(Ingredient ingredient)
    {
        if (ingredient == null)
            return false;

        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].GetIngredient() == null)
            {
                slots[i].SetIngredient(ingredient);

                // Optional: move selector to the filled slot
                dataHolder.selectorPos = i;
                MoveSelector();

                return true;
            }
        }

        // Inventory full
        return false;
    }

}
