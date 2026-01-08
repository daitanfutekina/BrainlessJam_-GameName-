using System.Linq.Expressions;
using UnityEngine;

public class Setts : MonoBehaviour
{
    public GameObject setts;
    void Start()
    {
        setts.SetActive(!true);

    }
    public void OpenSett()
    {
        setts.SetActive(true);
    }
    public void CloseSett()
    {
        setts.SetActive(!true);
    }


}
