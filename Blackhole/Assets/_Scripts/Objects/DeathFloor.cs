using Sound;
using UnityEngine;

public class DeathFloor : MonoBehaviour
{
    [Tooltip("Audio to play on score")]
    [SerializeField] private string _audioName;

    private void OnTriggerEnter(Collider other)
    {
        Droppable droppable;
        if (other.TryGetComponent(out droppable))
            GameManager.Instance.AddScore(droppable.Score);

        if (AudioManager.Instance != null && !GameManager.Instance.IsGameFinished)
            AudioManager.Instance.PlayOneShot(_audioName);

        Destroy(other.gameObject);
    }
}