using UnityEngine;

public class HoleGenerator : MonoBehaviour
{
    [Tooltip("The 2D collider used for adding points to the floor to cut a hole")]
    [SerializeField] private PolygonCollider2D _holeCollider2D;

    public void CutHole(Vector3 holePosition, Vector3 holeScale)
    {
        _holeCollider2D.transform.position = new Vector3(holePosition.x, holePosition.z, 0);
        _holeCollider2D.transform.localScale = new Vector3(holeScale.x, holeScale.z, 1);
        
        Vector2[] points = _holeCollider2D.GetPath(0);

        for (int i = 0; i < points.Length; i++)
        {
            points[i] = _holeCollider2D.transform.TransformPoint(points[i]);
        }

        FloorGenerator.Instance.FloorCollider2D.SetPath(1, points);
    }
}
