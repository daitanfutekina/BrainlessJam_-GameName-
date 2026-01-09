using UnityEngine;

public class ClickDetector : MonoBehaviour
{
    // LayerMask to filter which objects can be clicked (optional)
    public LayerMask clickableLayers = Physics.DefaultRaycastLayers;
    public OptionManager optionManager;
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
            optionManager.CheckPoison(hit.collider.name);

            // Example: Call a method on the clicked object
            IClickable clickable = hit.collider.GetComponent<IClickable>();
            if (clickable != null)
            {
                clickable.OnClick();
            }
        }
    }
}

// Optional interface for clicked objects
public interface IClickable
{
    void OnClick();
}
