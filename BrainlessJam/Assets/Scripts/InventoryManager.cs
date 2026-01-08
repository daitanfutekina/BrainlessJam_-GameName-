using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] private RectTransform selector;
    [SerializeField] private DataHolder dataHolder;

    private SlotManager[] slots;

    private void Awake()
    {
        if (dataHolder == null)
        {
            Debug.LogError("InventoryManager: DataHolder not assigned.");
            return;
        }

        slots = GetComponentsInChildren<SlotManager>(true);

        dataHolder.InitializeIfNeeded(slots.Length);

        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].Initialize(dataHolder, i);
        }
    }

    private void Update()
    {
        if (slots == null || slots.Length == 0) return;

        ScrollManagement();
        MoveSelector();
    }

    private void ScrollManagement()
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

    public void MoveSelector()
    {
        if (selector == null || slots[dataHolder.selectorPos] == null) return;
        selector.position = slots[dataHolder.selectorPos].transform.position;
    }

    public SlotManager GetSelectedSlot()
    {
        return slots[dataHolder.selectorPos];
    }

    public Ingredient GetSelectedIngredient()
    {
        return dataHolder.ingredients[dataHolder.selectorPos];
    }

    // Adds ingredient to first empty slot
    public bool TryAddToFirstEmptySlot(Ingredient ingredient)
    {
        if (ingredient == null) return false;

        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].GetIngredient() == null)
            {
                slots[i].SetIngredient(ingredient);
                dataHolder.selectorPos = i; // optional: move selector
                MoveSelector();
                return true;
            }
        }

        return false; // inventory full
    }
}
