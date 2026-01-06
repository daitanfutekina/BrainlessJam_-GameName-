using UnityEngine;
using System.Collections;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] GameObject[] slots;
    [SerializeField] GameObject selector;

    int selector_pos = 0;
    
    void Update()
    {
        ScrollManagement();
        MoveSelector();
    }

    void ScrollManagement()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll < 0)
        {
            selector_pos--;
        }
        if (scroll > 0)
        {
            selector_pos++;
        }
        if (selector_pos < 0)
        {
            selector_pos = slots.Length -1;
        }
        else if (selector_pos > slots.Length -1)
        {
            selector_pos = 0;
        }
    }
    void MoveSelector()
    {
        selector.transform.position = slots[selector_pos].transform.position;
    }
}
