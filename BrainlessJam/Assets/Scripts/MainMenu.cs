using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public AudioMixer mixer;
    public void PLay()
    {
        SceneManager.LoadScene(1);

    }
    public void Exit()
    {
        Application.Quit();

    }

    public void SetVolume(float volume)
    {
        mixer.SetFloat("Vol", volume);
    }
}
