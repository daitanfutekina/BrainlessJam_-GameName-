using UnityEngine;

[CreateAssetMenu(menuName = "WhackAMole/Config")]
public class WhackConfig : ScriptableObject
{
    public float gameDuration = 20f;
    public float spawnIntervalMin = 0.4f;
    public float spawnIntervalMax = 1.2f;
    public float moleUpTime = 0.9f;
    public int poolSize = 8;
    public int scorePerHit = 10;
    public int maxSimultaneous = 2;
}
