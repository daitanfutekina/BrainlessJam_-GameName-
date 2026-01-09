using UnityEngine;


public class GoalManager : MonoBehaviour
{
    [Header("Goals")]
    public Ingredient[] possibleGoals;
    public Ingredient currentGoal;

    [Header("Models")]
    public GameObject goalModelParent;
    
    void Start()
    {
        CreateGoal();
    }

    void CreateGoal()
    {
        int randomIndex = Random.Range(0, possibleGoals.Length);
        currentGoal = possibleGoals[randomIndex];
        Debug.Log("New Goal Created: " + currentGoal.ingredientName);
        GameObject instantiatedModel = Instantiate(currentGoal.modelPrefab, goalModelParent.transform);

        // Attach floating behavior
        Floating floating = instantiatedModel.AddComponent<Floating>();
        floating.amplitude = 0.25f;
        floating.speed = 1f;
    }


}
