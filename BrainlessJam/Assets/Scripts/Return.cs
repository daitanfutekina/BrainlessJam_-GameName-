using UnityEngine;
using UnityEngine.SceneManagement;

public class Return : MonoBehaviour
{
   public Animator PlayAnimator;
    public Animator SettAnimator;
    public Animator ExitAnimator;
    public void ReturnPlayAnimation()
    {
        PlayAnimator.SetInteger("Index", 0);

    }
    public void Startgame()
    {
        SceneManager.LoadScene(1);
    }
   

}
