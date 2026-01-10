using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.InputSystem;

public class Setts : MonoBehaviour
{
    public GameObject setts;

    public bool isActive = false;
    void Start()
    {
        setts.SetActive(false);
        isActive = false;
    }

    void Update()
    {
        if (Keyboard.current.escapeKey.IsPressed())
        {
            CloseSett();
        }
    }


    public void OpenSett()
    {
        setts.SetActive(true);
        isActive = true;
    }
    public void CloseSett()
    {
        setts.SetActive(false);
        isActive = false;
    }


}
