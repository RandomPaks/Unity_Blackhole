using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorGenerator : MonoBehaviour
{
    public static FloorGenerator Instance { get; private set; }

    [Header("Floor Settings")]
    [Tooltip("Size of the floor")]
    public float FloorScale = 1f;
    [Tooltip("The 2D collider used as reference points for the cut floor mesh")]
    public PolygonCollider2D FloorCollider2D;

    [Header("For 3D Floor")]
    [Tooltip("The new mesh collider with holes on the floor")]
    [SerializeField] private MeshCollider _cutFloorMeshCollider;
    [Tooltip("The floor gameobject used to render the floor (used to scale with the cut floor mesh collider)")]
    [SerializeField] private GameObject _floorGameObject;

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
        //2 because index 0 is for the floor points and index 1 is for the hole points
        FloorCollider2D.pathCount = 2;

        Vector2[] points = FloorCollider2D.GetPath(0);
        for (int i = 0; i < points.Length; i++)
        {
            points[i] *= FloorScale;
        }
        FloorCollider2D.SetPath(0, points);

        //1.5x scaled to ensure that out of bound clicks for raycasts register
        _floorGameObject.transform.localScale = new Vector3(
            _floorGameObject.transform.localScale.x * FloorScale * 1.5f, 
            _floorGameObject.transform.localScale.y *  FloorScale * 1.5f, 
            1);
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
        _cutFloorMeshCollider.sharedMesh = _generatedMesh;
    }
}
