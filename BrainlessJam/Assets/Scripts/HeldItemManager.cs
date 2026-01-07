using UnityEngine;

public class HeldItemManager : MonoBehaviour
{
    [SerializeField] InventoryManager inventoryManager;
    [SerializeField] GameObject modelHolder;

    MeshFilter mesh;
    MeshRenderer meshRenderer;

    Ingredient selectedIngredient;

    void Start()
    {
        mesh = modelHolder.GetComponent<MeshFilter>();
        meshRenderer = modelHolder.GetComponent<MeshRenderer>();
    }
    
    
    void Update()
    {
        if (inventoryManager.GetSelectedIngredient() != null) {selectedIngredient = inventoryManager.GetSelectedIngredient();} else {selectedIngredient = null;}
        
        if (selectedIngredient == null)
        {
            mesh.mesh = null;
            meshRenderer.material = null;
        }
        else
        {
            mesh.mesh = selectedIngredient.relevantModel;
        }
        meshRenderer.material = selectedIngredient.relevantMaterial;
    }
}
