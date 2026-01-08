using UnityEngine;

[CreateAssetMenu(fileName = "Ingredient", menuName = "Scriptable Objects/Ingredient")]
public class Ingredient : ScriptableObject
{
    public string ingredientName;
    public Sprite icon;
    public string relevantTool;
    public Collider relevantZone;
    public GameObject modelPrefab;
    public bool isTool;
    public bool cookable;
    public Ingredient cookResult;
        
    void Start()
    {
        if (relevantTool == "")
        {
            relevantTool = null;
        }
    }
}
