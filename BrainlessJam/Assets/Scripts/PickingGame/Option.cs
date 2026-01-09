using UnityEngine;

public class Option : MonoBehaviour
{
    public bool isPoisoned;
    public ParticleSystem poisonParticles;

    void Start()
    {
        poisonParticles.Stop();
        Debug.Log(isPoisoned);
    }
    
    
    void Update()
    {
        ManageParticles();
    }

    void ManageParticles()
    {
        if (isPoisoned)
        {
            if (!poisonParticles.isPlaying)
            {
                poisonParticles.Play();
            }
        }
        else
        {
            poisonParticles.Stop();
        }
    }
}
