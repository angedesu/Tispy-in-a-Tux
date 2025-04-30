using UnityEngine;

public class PourParticlesController : MonoBehaviour
{
    public GameObject pouringParticlesPrefab;

    public void PlayParticles()
    {
        if (pouringParticlesPrefab != null)
        {
            // Instantiate prefab
            GameObject particles = Instantiate(pouringParticlesPrefab, transform); 
            

            
            ParticleSystem ps = particles.GetComponent<ParticleSystem>();
            if (ps != null)
            {
                ps.Play();
                Debug.Log("Particles created and played");
                
            }
            else
            {
                Debug.LogWarning("Instantiated object does not have a ParticleSystem component.");
            }
        }
        else
        {
            Debug.LogWarning("pouringParticlesPrefab is not assigned.");
        }
    }
}