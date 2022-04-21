using UnityEngine;

public class DeathFloor : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        DroppableObject droppableObject;
        if (other.TryGetComponent(out droppableObject))
            GameManager.Instance.AddScore(droppableObject.Score);

        Destroy(other.gameObject);
    }
}