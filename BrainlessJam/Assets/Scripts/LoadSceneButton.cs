using UnityEngine;
using UnityEngine.SceneManagement; // Required for scene loading

public class LoadSceneButton : MonoBehaviour
{
    // Name of the scene to load (set in Inspector)
    [SerializeField] private string sceneName = "";

    // Optional: Load by build index instead of name
    [SerializeField] private int sceneIndex = -1;

    /// <summary>
    /// Loads the specified scene when called.
    /// </summary>
    public void LoadScene()
    {
        try
        {
            if (!string.IsNullOrEmpty(sceneName))
            {
                // Load by scene name
                SceneManager.LoadScene(sceneName);
            }
            else if (sceneIndex >= 0 && sceneIndex < SceneManager.sceneCountInBuildSettings)
            {
                // Load by build index
                SceneManager.LoadScene(sceneIndex);
            }
            else
            {
                Debug.LogError("No valid scene name or index provided.");
            }
        }
        catch (System.Exception ex)
        {
            Debug.LogError($"Failed to load scene: {ex.Message}");
        }
    }
}
