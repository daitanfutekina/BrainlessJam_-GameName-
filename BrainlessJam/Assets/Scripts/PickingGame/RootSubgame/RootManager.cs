using UnityEngine;
using UnityEngine.SceneManagement;

public class RootManager : MonoBehaviour
{
    public int requiredClicks = 10;
    public string winScene;
    public DataHolder dataHolder;
    public Ingredient ingredientToAdd;
    
    public void CheckClicks(int clickCount)
    {
        if (clickCount >= requiredClicks)
        {
            bool added = TryAddToNextEmptySlot(dataHolder, ingredientToAdd);
            SceneManager.LoadScene(winScene);
        }
            
    }
    bool TryAddToNextEmptySlot(DataHolder holder, Ingredient ingredient)
    {
        for (int i = 0; i < holder.ingredients.Length; i++)
        {
            if (holder.ingredients[i] == null)
            {
                holder.ingredients[i] = ingredient;
                Debug.Log($"Added ingredient '{ingredient.ingredientName}' to slot {i}");
                return true;
            }
        }
        Debug.Log("Inventory Full! Could not add ingredient: " + ingredient.ingredientName);
        return false;
    }
}
