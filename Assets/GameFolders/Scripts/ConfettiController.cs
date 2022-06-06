using UnityEngine;

public class ConfettiController : MonoBehaviour
{
    [SerializeField] private ParticleSystem[] _particles;

    private void OnEnable()
    {
        RuffGameManager.OnLevelComplete += PlayConfetti;
    }

    private void OnDisable()
    {
        RuffGameManager.OnLevelComplete -= PlayConfetti;
    }

    private void PlayConfetti()
    {
        foreach (var particle in _particles)
        {
            particle.Play();
        }
    }
}



