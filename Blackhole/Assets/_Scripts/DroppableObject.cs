using UnityEngine;

public class DroppableObject : MonoBehaviour
{
    [Tooltip("How much score the object gives")]
    public int Score;

    private Collider _collider;

    /// <summary>
    /// The code below could've been cramped inside the player controller 
    /// for more optimized gameplay but I opted for a more OOP approach
    /// in consideration that it'd be more modular/reusable in the long run
    /// when other developers would touch the scripts...
    /// </summary>

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