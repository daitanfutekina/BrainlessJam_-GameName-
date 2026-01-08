using UnityEngine;
using UnityEngine.SceneManagement;

public class Targets : MonoBehaviour
{
    [Header("Configuration")]
    public TargetManager targetManager;
    public SpearHuntingManager minigameManager;
    public int winSceneBuildIndex;

    [Header("Values")]
    public float minSpeed;
    public float maxSpeed;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "MinigameSpear")
        {
            targetManager.hitCount++;
            SceneManager.LoadScene(winSceneBuildIndex);
        }
    }
}
