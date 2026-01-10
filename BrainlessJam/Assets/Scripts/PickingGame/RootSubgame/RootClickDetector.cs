using UnityEngine;

public class RootClickDetector : MonoBehaviour
{
    // LayerMask to filter which objects can be clicked (optional)
    public LayerMask clickableLayers = Physics.DefaultRaycastLayers;
    public RootManager minigameManager;
    // make a ref to a tmpUI text to show click count
    public TMPro.TMP_Text clickCountText;

    private int clickCount = 0;
    void Update()
    {
        // Detect left mouse button click
        if (Input.GetMouseButtonDown(0))
        {
            DetectClick();
        }
    }

    private void DetectClick()
    {
        // Create a ray from the camera to the mouse position
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        // Perform the raycast
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, clickableLayers))
        {
            clickCount++;
            if (clickCountText != null)
            {
                clickCountText.text = clickCount.ToString();
            }
            minigameManager.CheckClicks(clickCount);

            // Example: Call a method on the clicked object
            IClickable clickable = hit.collider.GetComponent<IClickable>();
            if (clickable != null)
            {
                clickable.OnClick();
            }
        }
    }
}
