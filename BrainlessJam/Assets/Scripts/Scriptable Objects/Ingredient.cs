using UnityEngine;

[CreateAssetMenu(fileName = "Ingredient", menuName = "Scriptable Objects/Ingredient")]
public class Ingredient : ScriptableObject
{
    public string ingredientName;
    public Sprite icon;
    public string relevantTool;
    public Collider relevantZone;
    public Mesh relevantModel;
    public Material relevantMaterial;
    public bool isTool;
}
