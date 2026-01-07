using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public AudioMixer mixer;
    Camera cam;

    [Header("Buttons")]
    public GameObject playButton;
    public GameObject exitButton;
    public GameObject settingsButton;
    public GameObject closeSettingsButton;

    [Header("Animators")]
    public Animator playAnim;
    public Animator exitAnim;
    public Animator settingsAnim;

    public GameObject settingsPanel;

    RaycastHit hit;

    void Start()
    {
        cam = Camera.main;
    }

    void Update()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);

        bool playHover = false;
        bool exitHover = false;
        bool settingsHover = false;

        if (Physics.Raycast(ray, out hit, 100f))
        {
            playHover = hit.collider.gameObject == playButton;
            exitHover = hit.collider.gameObject == exitButton;
            settingsHover = hit.collider.gameObject == settingsButton;

            if (Input.GetMouseButtonDown(0))
            {
                if (hit.collider.gameObject == playButton)
                    PlayGame();
                else if (hit.collider.gameObject == exitButton)
                    ExitGame();
                else if (hit.collider.gameObject == settingsButton)
                    OpenSettings();
                else if (hit.collider.gameObject == closeSettingsButton)
                    CloseSettings();
            }
        }

        playAnim.SetBool("Hover", playHover);
        exitAnim.SetBool("Hover", exitHover);
        settingsAnim.SetBool("Hover", settingsHover);
    }

    void PlayGame()
    {
        playAnim.SetTrigger("Click");
        // SceneManager.LoadScene(1);
    }

    void ExitGame()
    {
        exitAnim.SetTrigger("Click");
        Application.Quit();
    }

    void OpenSettings()
    {
        settingsPanel.SetActive(true);
        settingsAnim.SetTrigger("Click");
    }

    void CloseSettings()
    {
        settingsPanel.SetActive(false);
    }

    public void SetVolume(float volume)
    {
        mixer.SetFloat("Vol", volume);
    }
}
