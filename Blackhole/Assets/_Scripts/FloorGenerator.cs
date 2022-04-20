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
    [Tooltip("The floor gameobject used to render the floor (to be scale with the cut floor mesh collider)")]
    [SerializeField] private GameObject _floorRenderGameObject;
    [Tooltip("The floor layer gameobject used for collecting raycasts (ensures that out of bound clicks register)")]
    [SerializeField] private GameObject _floorLayerGameObject;
    [Tooltip("The death floor gameobject used to destroy collected objects (to be scale with the cut floor mesh collider)")]
    [SerializeField] private GameObject _deathFloorGameObject;

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

        //scaled floor render
        _floorRenderGameObject.transform.localScale *= FloorScale;

        //2x scaled to ensure that out of bound clicks for raycasts register
        _floorLayerGameObject.transform.localScale = new Vector3(
            _floorLayerGameObject.transform.localScale.x * FloorScale * 2,
            _floorLayerGameObject.transform.localScale.y * FloorScale * 2,
            1);

        _deathFloorGameObject.transform.localScale *= FloorScale;
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
