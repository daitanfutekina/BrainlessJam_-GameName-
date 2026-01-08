using UnityEngine;
using TMPro; // Only if using TextMeshPro

public class PopupUI : MonoBehaviour
{
    [SerializeField] private GameObject popupPanel; // The panel to show
    [SerializeField] private TMP_Text messageText;  // Text component

    private void Awake()
    {
        popupPanel.SetActive(false); // Ensure it starts hidden
    }

    public void ShowPopup(string message)
    {
        if (popupPanel == null || messageText == null) return;

        messageText.text = message;
        popupPanel.SetActive(true);
    }

    public void HidePopup()
    {
        popupPanel.SetActive(false);
    }
}
