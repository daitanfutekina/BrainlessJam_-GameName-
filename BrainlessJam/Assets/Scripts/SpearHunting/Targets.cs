using UnityEngine;
using UnityEngine.SceneManagement;

public class Targets : MonoBehaviour
{
    [Header("Configuration")]
    public TargetManager targetManager;
    public SpearHuntingManager minigameManager;
    public int winSceneBuildIndex;

    [Header("Values")]
    public float startPoint;
    public float endPoint;
    public float speed = 1f;
    
    
    void Update()
    {
        OscillateLerp(gameObject.transform, 'x', startPoint, endPoint, speed);
    }    
    
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "MinigameSpear")
        {
            targetManager.hitCount++;
            SceneManager.LoadScene(winSceneBuildIndex);
        }
    }

    public static void OscillateLerp(Transform t, char axis, float min, float max, float speed = 1f)
{
    // PingPong returns a value between 0 and 1 * range
    float tValue = Mathf.PingPong(Time.time * speed, 1f); 
    float value = Mathf.Lerp(min, max, tValue);

    Vector3 pos = t.position;
    switch (axis)
    {
        case 'x': pos.x = value; break;
        case 'y': pos.y = value; break;
        case 'z': pos.z = value; break;
    }
    t.position = pos;
}


}
