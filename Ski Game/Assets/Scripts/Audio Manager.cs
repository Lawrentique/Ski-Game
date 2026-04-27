using UnityEngine;
public class AudioManager : MonoBehaviour
{
    private AudioSource audioSource;
    [SerializeField] private AudioClip obstacleHitSound;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        Obstacle.OnPlayerHit += PlayObstacleHitSound;
    }

    void PlayObstacleHitSound()
    {
        audioSource.PlayOneShot(obstacleHitSound);
    }
}
