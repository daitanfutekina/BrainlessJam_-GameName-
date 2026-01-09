using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class OptionManager : MonoBehaviour
{
    public Option[] options;
    public string winScene;

    public DataHolder dataHolder;
    public Ingredient ingredientToAdd;
    
    private int poisionOption;

    void Awake()
    {
        AddPosion();
        AddPosion();
    }

    void AddPosion()
    {
        poisionOption = Random.Range(0, options.Length);
        if (!options[poisionOption].isPoisoned)
        {
            options[poisionOption].isPoisoned = true;
        }
        else
        {
            AddPosion();
        }
    }

    public void CheckPoison(string clickedName)
    {
        foreach (Option option in options)
        {
            if (!option.isPoisoned && option.gameObject.name == clickedName)
            {
                if (dataHolder != null && ingredientToAdd != null)
                {
                    bool added = TryAddToNextEmptySlot(dataHolder, ingredientToAdd);
                    if (!added)
                        Debug.Log("Inventory Full! Could not add ingredient: " + ingredientToAdd.ingredientName);
                }
                SceneManager.LoadScene(winScene);
            }
            else if (option.gameObject.name == clickedName)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }

    private bool TryAddToNextEmptySlot(DataHolder holder, Ingredient ingredient)
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
        return false; // No empty slot found
    }
}
