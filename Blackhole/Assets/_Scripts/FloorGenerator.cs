using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorGenerator : MonoBehaviour
{
    public static FloorGenerator Instance { get; private set; }

    [Tooltip("Size of the floor")]
    public float FloorScale = 1f;
    [Tooltip("The 2D collider used as reference points for the cut floor mesh")]
    public PolygonCollider2D FloorCollider2D;
    [Tooltip("The new mesh collider with holes on the floor")]
    [SerializeField] private MeshCollider _cutfloorMeshCollider;

    //used for generating a new mesh when GenerateMeshCollider is called
    private Mesh _generatedMesh;

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;
    }

    private void Start()
    {
        //+1 because index 0 is the floor points while numbers above 0 are the hole points
        FloorCollider2D.pathCount = GameManager.Instance.NumPlayers + 1;

        Vector2[] points = FloorCollider2D.GetPath(0);

        for (int i = 0; i < points.Length; i++)
        {
            points[i] *= FloorScale;
        }

        FloorCollider2D.SetPath(0, points);
    }

    private void Update()
    {
        GenerateFloorMeshCollider();
    }

    private void GenerateFloorMeshCollider()
    {
        //fixes memory problems with generating meshes
        if (_generatedMesh != null)
            Destroy(_generatedMesh);
        
        _generatedMesh = FloorCollider2D.CreateMesh(true, true);
        _cutfloorMeshCollider.sharedMesh = _generatedMesh;
    }
}
