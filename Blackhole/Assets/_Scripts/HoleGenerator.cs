using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoleGenerator : MonoBehaviour
{
    [Tooltip("The 2D collider used for adding points to the floor to cut a hole")]
    [SerializeField] private PolygonCollider2D _holeCollider2D;

    public void CutHole(Vector2 holePosition, Vector3 holeScale, int playerIndex = 1)
    {
        _holeCollider2D.transform.position = holePosition;
        _holeCollider2D.transform.localScale = holeScale;
        
        Vector2[] points = _holeCollider2D.GetPath(0);

        for (int i = 0; i < points.Length; i++)
        {
            points[i] = _holeCollider2D.transform.TransformPoint(points[i]);
        }

        FloorGenerator.Instance.FloorCollider2D.SetPath(playerIndex, points);
    }
}
