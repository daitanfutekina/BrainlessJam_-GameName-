using UnityEngine;


public class GoalManager : MonoBehaviour
{
    [Header("Goals")]
    public Ingredient[] possibleGoals;
    public Ingredient currentGoal;
    public InventoryManager inventoryManager;  
    public string pickupHotkeyName = "Pickup";
    public PopupUI popup;
    public DataHolder dataHolder;

    [Header("Models")]
    public GameObject goalModelParent;
    
    [Header("Particles")]
    public GameObject goalCompletedEffectParent;
    public GameObject goalFailedEffectParent;
    public ParticleSystem idleParticles;

    private bool playerInRange = false;

    void Start()
    {
        // Load goal from DataHolder if it exists
        if (dataHolder != null && dataHolder.currentGoal != null)
        {
            currentGoal = dataHolder.currentGoal;
            Debug.Log("Goal loaded from DataHolder: " + currentGoal.ingredientName);
            GameObject instantiatedModel = Instantiate(currentGoal.modelPrefab, goalModelParent.transform);
            Floating floating = instantiatedModel.AddComponent<Floating>();
            floating.amplitude = 0.25f;
            floating.speed = 1f;
        }
        else
        {
            CreateGoal();
        }

        if (idleParticles != null)
        {
            idleParticles.Play();
        }

        if (goalCompletedEffectParent != null)
        {
            foreach (ParticleSystem ps in goalCompletedEffectParent.GetComponentsInChildren<ParticleSystem>())
            {
                ps.Stop();
            }
        }
        if (goalFailedEffectParent != null)
        {
            foreach (ParticleSystem ps in goalFailedEffectParent.GetComponentsInChildren<ParticleSystem>())
            {
                ps.Stop();
            }
        }
    }

    void CreateGoal()
    {
        int randomIndex = Random.Range(0, possibleGoals.Length);
        
        // Ensure the new goal differs from the previous one
        while (currentGoal != null && possibleGoals[randomIndex].ingredientName == currentGoal.ingredientName)
        {
            randomIndex = Random.Range(0, possibleGoals.Length);
        }
        
        currentGoal = possibleGoals[randomIndex];
        
        // Save goal to DataHolder for persistence between scenes
        if (dataHolder != null)
        {
            dataHolder.currentGoal = currentGoal;
        }
        
        Debug.Log("New Goal Created: " + currentGoal.ingredientName);
        GameObject instantiatedModel = Instantiate(currentGoal.modelPrefab, goalModelParent.transform);

        Floating floating = instantiatedModel.AddComponent<Floating>();
        floating.amplitude = 0.25f;
        floating.speed = 1f;

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            playerInRange = true;
            popup.ShowPopup("Press E to submit your held item!");
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            playerInRange = false;
            popup.HidePopup();
        }
    }

    void Update()
    {
        ProcessZoneLogic();
        if (goalCompletedEffectParent != null && goalFailedEffectParent != null)
        {
            bool completedPlaying = false;
            bool failedPlaying = false;
            foreach (ParticleSystem ps in goalCompletedEffectParent.GetComponentsInChildren<ParticleSystem>())
            {
                if (ps.isPlaying)
                {
                    completedPlaying = true;
                    break;
                }
            }
            foreach (ParticleSystem ps in goalFailedEffectParent.GetComponentsInChildren<ParticleSystem>())
            {
                if (ps.isPlaying)
                {
                    failedPlaying = true;
                    break;
                }
            }
            if (!completedPlaying && !failedPlaying && idleParticles != null && !idleParticles.isPlaying)
            {
                idleParticles.Play();
            }
        }
    }
    void ProcessIncorrectItem()
    {
        
        StartCoroutine(DelayedFailurePopup());

    }

    
    void ProcessZoneLogic()
    {
        if (playerInRange)
        {
            if (Input.GetButtonDown(pickupHotkeyName))
            {
                if (inventoryManager.GetSelectedIngredient() != null)
                {
                    if (inventoryManager.GetSelectedIngredient().ingredientName == currentGoal.ingredientName)
                    {
                        popup.ShowPopup("The Gods are pleased with your offer!");
                        inventoryManager.GetSelectedSlot().SetIngredient(null);
                        if (goalCompletedEffectParent != null)
                        {
                            foreach (ParticleSystem ps in goalCompletedEffectParent.GetComponentsInChildren<ParticleSystem>())
                            {
                                ps.Play();
                            }
                        }
                        foreach (Transform child in goalModelParent.transform)
                        {
                            Destroy(child.gameObject);
                        }
                        CreateGoal();
                        
                    }
                    else
                    {
                        if (!inventoryManager.GetSelectedIngredient().isTool)
                        {
                            ProcessIncorrectItem();
                        }
                        else
                        {
                            popup.ShowPopup("You can't submit a tool as a goal!");
                        }
                    }
                }
                else
                {
                    popup.ShowPopup("You have no item selected!");
                }
            }
        }
    }
    
    System.Collections.IEnumerator DelayedFailurePopup()
    {
        popup.ShowPopup("The Gods are examining your offer...");
        yield return new WaitForSeconds(1f);
        popup.ShowPopup("The Gods are displeased! Try again!");
        if (goalFailedEffectParent != null)
        {
            foreach (ParticleSystem ps in goalFailedEffectParent.GetComponentsInChildren<ParticleSystem>())
            {
                ps.Play();
            }
        }
    }

}
