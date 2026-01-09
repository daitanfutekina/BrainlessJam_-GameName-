using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.InputSystem;

public class Setts : MonoBehaviour
{
    public GameObject setts;
    void Start()
    {
        setts.SetActive(false);
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
    }
    public void CloseSett()
    {
        setts.SetActive(false);
    }


}
