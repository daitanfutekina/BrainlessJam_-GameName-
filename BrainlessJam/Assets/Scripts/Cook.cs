using UnityEngine;

public class Cook : MonoBehaviour
{
    // public vars
    [Header("Inventory")]
    public InventoryManager inventoryManager;
    public Ingredient[] cookableIngredients;

    [Header("Configuration")]
    public float cookTime;
    public string pickupHotkeyName = "Pickup";

    [Header("UI")]
    public PopupUI popup;
    public string popupMessage = "Press E to cook held Item";

    private bool playerInRange = false;
    private Ingredient originalItem;
    private float elapsedCookTime;
    private bool isCooking = false;
    private float remainingCookTime = 0f;
    private int lastDisplayedSeconds = -1;
    
    
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            playerInRange = true;
            popup.ShowPopup(popupMessage);
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
        if (playerInRange)
        {
            if (Input.GetButtonDown(pickupHotkeyName))
            {
                if (inventoryManager.GetSelectedIngredient() != null)
                {
                    if (inventoryManager.GetSelectedIngredient().isCookable)
                    {
                        originalItem = inventoryManager.GetSelectedIngredient();
                        inventoryManager.GetSelectedSlot().SetIngredient(null);
                        isCooking = true;
                        remainingCookTime = cookTime;
                        lastDisplayedSeconds = Mathf.CeilToInt(remainingCookTime);
                        popup.ShowPopup($"Cooking... {lastDisplayedSeconds} seconds remaining...");
                    }
                    else
                    {
                        popup.ShowPopup("You can't cook this item!");
                    }
                }
                else
                {
                    popup.ShowPopup("You're not holding anything!");
                }
            }
        }

        if (isCooking)
        {
            remainingCookTime -= Time.deltaTime;
            int secondsLeft = Mathf.CeilToInt(Mathf.Max(0f, remainingCookTime));
            if (secondsLeft != lastDisplayedSeconds)
            {
                lastDisplayedSeconds = secondsLeft;
                popup.ShowPopup($"Cooking... {secondsLeft} seconds remaining...");
            }

            if (remainingCookTime <= 0f)
            {
                isCooking = false;
                SwitchToCooked();
            }
        }
    }

    void SwitchToCooked()
    {
        inventoryManager.GetSelectedSlot().SetIngredient(originalItem.cookResult);
        originalItem = null;
        popup.ShowPopup("Cooking complete!");
        StartCoroutine(HidePopupAfter(2f));
    }

    System.Collections.IEnumerator HidePopupAfter(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        popup.HidePopup();
    }

}
