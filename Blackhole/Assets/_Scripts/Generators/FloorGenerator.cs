using UnityEngine;

public class FloorGenerator : MonoBehaviour
{
    public static FloorGenerator Instance { get; private set; }

    [Tooltip("The 2D collider used as reference points for the cut floor mesh")]
    public PolygonCollider2D FloorCollider2D;

    [Header("Floor Settings")]
    [Tooltip("Size of the floor")]
    [Range(1, 100)]
    public float FloorScale = 1f;

    [Header("For 3D Floor")]
    [Tooltip("The new mesh collider with holes on the floor")]
    [SerializeField] private MeshCollider _cutFloorMeshCollider;
    [Tooltip("Used to render the floor (to be scaled with the cut floor mesh collider)")]
    [SerializeField] private GameObject _floorRenderGameObject;
    [Tooltip("Used for collecting raycasts and optimization (ensures that out of bound clicks register)")]
    [SerializeField] private GameObject _groundZeroGameObject;
    [Tooltip("Used to destroy collected objects (to be scaled with the cut floor mesh collider)")]
    [SerializeField] private GameObject _deathFloorGameObject;

    //used for generating a new mesh when GenerateMeshCollider is called
    private Mesh _generatedMesh;
    private Collider _groundZeroCollider;

    public Collider GroundZeroCollider => _groundZeroCollider;
    public Collider CutFloorMeshCollider => _cutFloorMeshCollider;

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;

        _groundZeroCollider = _groundZeroGameObject.GetComponent<Collider>();
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
        _groundZeroGameObject.transform.localScale = new Vector3(
            _groundZeroGameObject.transform.localScale.x * FloorScale * 2,
            _groundZeroGameObject.transform.localScale.y * FloorScale * 2,
            1);

        //2x in any case objects go out of bounds and will get destroyed
        _deathFloorGameObject.transform.localScale = new Vector3(
            _deathFloorGameObject.transform.localScale.x * FloorScale * 2,
            _deathFloorGameObject.transform.localScale.y * FloorScale * 2,
            1);
    }

    private void FixedUpdate()
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
