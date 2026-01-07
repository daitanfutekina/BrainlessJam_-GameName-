using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] private RectTransform selector;

    private SlotManager[] slots;
    public int selectorPos = 0;

    void Awake()
    {
        slots = GetComponentsInChildren<SlotManager>(true);
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
            selectorPos++;
        else if (scroll < 0)
            selectorPos--;

        if (selectorPos < 0)
            selectorPos = slots.Length - 1;
        else if (selectorPos >= slots.Length)
            selectorPos = 0;
    }

    public void MoveSelector()
    {
        if (slots[selectorPos] == null)
            return;

        selector.position = slots[selectorPos].transform.position;
    }

    // Returns the currently selected SlotManager
    public SlotManager GetSelectedSlot()
    {
        if (slots == null || slots.Length == 0)
            return null;

        if (selectorPos < 0 || selectorPos >= slots.Length)
            return null;

        return slots[selectorPos];
    }

    // Returns the currently selected Ingredient
    public Ingredient GetSelectedIngredient()
    {
        SlotManager slot = GetSelectedSlot();
        if (slot == null) return null;

        return slot.GetIngredient();
    }

}
