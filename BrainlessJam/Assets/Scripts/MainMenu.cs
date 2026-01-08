using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public AudioMixer mixer;
    Camera cam;

    [Header("Buttons")]
    public GameObject playButton;
    public GameObject settingsButton;
    public GameObject exitButton;

    [Header("Animators")]
    public Animator playAnim;
    public Animator settingsAnim;
    public Animator exitAnim;


    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip hoverClip;   // stone slide / rumble
    public AudioClip clickClip;   // heavier stone impact

    RaycastHit hit;

    GameObject lastHoverButton;

    void Start()
    {
        cam = Camera.main;
        //settingsPanel.SetActive(false);
        lastHoverButton = null;
    }

    void Update()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);

        bool playHover = false;
        bool settingsHover = false;
        bool exitHover = false;

        GameObject currentHover = null;

        if (Physics.Raycast(ray, out hit, 100f))
        {
            GameObject hitObj = hit.collider.gameObject;

            if (hitObj == playButton)
            {
                playHover = true;
                currentHover = playButton;
            }
            else if (hitObj == settingsButton)
            {
                settingsHover = true;
                currentHover = settingsButton;
            }
            else if (hitObj == exitButton)
            {
                exitHover = true;
                currentHover = exitButton;
            }

            // Hover sound (only once per entry)
            if (currentHover != null && currentHover != lastHoverButton)
            {
                audioSource.PlayOneShot(hoverClip);
                lastHoverButton = currentHover;
            }

            if (Input.GetMouseButtonDown(0))
            {
                audioSource.PlayOneShot(clickClip);

                if (hitObj == playButton)
                    PlayGame();
                else if (hitObj == settingsButton)
                    OpenSettings();
                else if (hitObj == exitButton)
                    ExitGame();
            }
        }
        else
        {
            lastHoverButton = null;
        }

        playAnim.SetBool("Hover", playHover);
        settingsAnim.SetBool("Hover", settingsHover);
        exitAnim.SetBool("Hover", exitHover);
    }
    
    void PlayGame()
    {
        playAnim.SetTrigger("Click");
        SceneManager.LoadScene(1);
    }

    void OpenSettings()
    {
        settingsAnim.SetTrigger("Click");
      //  settingsPanel.SetActive(true);
    }

    void ExitGame()
    {
        exitAnim.SetTrigger("Click");
        Application.Quit();
    }

    public void SetVolume(float volume)
    {
        mixer.SetFloat("Vol", volume);
    }
    public void Fullscreen(bool isFull)
    {
        Screen.fullScreen = isFull;
        Screen.fullScreenMode = FullScreenMode.ExclusiveFullScreen;
    }
    public void setQuality(int index)
    {
        QualitySettings.SetQualityLevel(index);
    }
}
