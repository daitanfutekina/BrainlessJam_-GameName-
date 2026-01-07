using UnityEngine;
using UnityEngine.UI;

public class SlotManager : MonoBehaviour
{
    [SerializeField] private Ingredient ingredient;
    [SerializeField] private Image image;

    void Start()
    {
        UpdateSlot();
    }

    public Ingredient GetIngredient()
    {
        return ingredient;
    }

    public void SetIngredient(Ingredient newIngredient)
    {
        ingredient = newIngredient;
        UpdateSlot();
    }

    void UpdateSlot()
    {
        if (image == null) return;

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
        UpdateSlot();
    }
#endif
}
