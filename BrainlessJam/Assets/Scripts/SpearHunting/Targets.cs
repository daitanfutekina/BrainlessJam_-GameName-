using UnityEngine;
using UnityEngine.SceneManagement;

public class Targets : MonoBehaviour
{
    [Header("Configuration")]
    public TargetManager targetManager;
    public SpearHuntingManager minigameManager;
    public int winSceneBuildIndex;

    [Header("Inventory")]
    public DataHolder dataHolder;          // Reference to the ScriptableObject holding inventory
    public Ingredient ingredientToAdd;     // Ingredient to add on hit
    public int inventorySlotCount = 10;    // Default inventory size

    [Header("Values")]
    public float startPoint;
    public float endPoint;
    public float speed = 1f;

    private void Start()
    {
        // Ensure the DataHolder's ingredients array is initialized
        if (dataHolder != null)
            dataHolder.InitializeIfNeeded(inventorySlotCount);
    }

    private void Update()
    {
        OscillateLerp(transform, 'x', startPoint, endPoint, speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("MinigameSpear"))
        {
            targetManager.hitCount++;

            // Add ingredient to inventory
            if (dataHolder != null && ingredientToAdd != null)
            {
                bool added = TryAddToNextEmptySlot(dataHolder, ingredientToAdd);
                if (!added)
                    Debug.Log("Inventory Full! Could not add ingredient: " + ingredientToAdd.ingredientName);
            }

            // Load the win scene
            SceneManager.LoadScene(winSceneBuildIndex);
        }
    }

    /// <summary>
    /// Adds an ingredient to the first empty slot in the DataHolder
    /// </summary>
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

    public static void OscillateLerp(Transform t, char axis, float min, float max, float speed = 1f)
    {
        float tValue = Mathf.PingPong(Time.time * speed, 1f);
        float value = Mathf.Lerp(min, max, tValue);

        Vector3 pos = t.position;
        switch (axis)
        {
            case 'x': pos.x = value; break;
            case 'y': pos.y = value; break;
            case 'z': pos.z = value; break;
        }
        t.position = pos;
    }
}
