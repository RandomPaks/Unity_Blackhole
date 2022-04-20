using UnityEngine;

public class DroppableObject : MonoBehaviour
{
    [Tooltip("How much score the object gives")]
    public int Score;

    private Collider _collider;

    private void Awake()
    {
        _collider = GetComponent<Collider>();
    }

    private void Start()
    {
        Physics.IgnoreCollision(_collider, FloorGenerator.Instance.CutFloorMeshCollider, true);
    }

    private void OnTriggerEnter(Collider other)
    {
        Physics.IgnoreCollision(_collider, FloorGenerator.Instance.GroundZeroCollider, true);
        Physics.IgnoreCollision(_collider, FloorGenerator.Instance.CutFloorMeshCollider, false);
    }

    private void OnTriggerExit(Collider other)
    {
        Physics.IgnoreCollision(_collider, FloorGenerator.Instance.GroundZeroCollider, false);
        Physics.IgnoreCollision(_collider, FloorGenerator.Instance.CutFloorMeshCollider, true);
    }
}