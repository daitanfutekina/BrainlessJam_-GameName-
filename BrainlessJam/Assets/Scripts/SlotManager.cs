using UnityEngine;
using UnityEngine.UI;

public class SlotManager : MonoBehaviour
{
    [Header("Editor Preset (Optional)")]
    [SerializeField] private Ingredient presetIngredient;

    [SerializeField] private Image image;

    private DataHolder dataHolder;
    private int slotIndex;

    public void Initialize(DataHolder holder, int index)
    {
        dataHolder = holder;
        slotIndex = index;

        // Apply preset ONLY if DataHolder slot is empty
        if (dataHolder.ingredients[slotIndex] == null && presetIngredient != null)
        {
            dataHolder.ingredients[slotIndex] = presetIngredient;
        }

        UpdateSlot();
    }

    public Ingredient GetIngredient()
    {
        return dataHolder.ingredients[slotIndex];
    }

    public void SetIngredient(Ingredient newIngredient)
    {
        dataHolder.ingredients[slotIndex] = newIngredient;
        UpdateSlot();
    }

    void UpdateSlot()
    {
        if (image == null || dataHolder == null)
            return;

        Ingredient ingredient = dataHolder.ingredients[slotIndex];

        if (ingredient != null)
        {
            image.sprite = ingredient.icon;
            image.enabled = true;
        }
        else
        {
            image.sprite = null;
            image.enabled = false;
        }
    }

#if UNITY_EDITOR
    void OnValidate()
    {
        if (image == null)
            return;

        image.sprite = presetIngredient != null ? presetIngredient.icon : null;
        image.enabled = presetIngredient != null;
    }
#endif
}
