using UnityEngine;

[RequireComponent(typeof(HoleGenerator))]
public class PlayerController : MonoBehaviour
{
    [Header("Hole Settings")]
    [Tooltip("Size of the starting hole")]
    public float HoleScale = 1f;
    [Tooltip("How much the hole will expand when it reaches a certain threshold")]
    public float HoleScaleMultiplier = 1.1f;
    [Tooltip("How fast it takes for the hole to scale")]
    public float HoleScaleTime = 5.0f;

    [Header("Movement Settings")]
    [Tooltip("Speed of the hole controller")]
    public float MoveSpeed = 2.0f;

    private const float _holeScale2D = 0.5f;

    private Vector3 _moveDirection;
    private int _layerMask;
    private Vector3 _desiredLocalScale;

    private HoleGenerator _holeGenerator;
    private CharacterController _characterController;
    private Camera _mainCamera;

    private void Awake()
    {
        _holeGenerator = GetComponent<HoleGenerator>();
        _characterController = GetComponent<CharacterController>();
    }

    private void Start()
    {
        _mainCamera = GameManager.Instance.MainCamera;

        //used to raycast from camera to floor
        _layerMask = LayerMask.GetMask("Floor Raycast");

        //sets the size of the initial hole
        transform.localScale = new Vector3(transform.localScale.x * HoleScale, 0.01f, transform.localScale.z * HoleScale);
        _desiredLocalScale = transform.localScale;
    }

    private void Update()
    {
        UpdateLocalScale();
    }

    private void FixedUpdate()
    {
        Move();

        _holeGenerator.CutHole(transform.position, transform.localScale * _holeScale2D);
    }

    private void Move()
    {
        if (Input.GetMouseButton(0))
        {
            RaycastHit hit;
            Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 999f, _layerMask))
            {
                _moveDirection = (hit.point - transform.position).normalized;
            }

            _characterController.Move(_moveDirection * MoveSpeed * Time.deltaTime);
        }
    }

    public void ScaleHoleScale()
    {
        _desiredLocalScale = new Vector3(
            _desiredLocalScale.x * HoleScaleMultiplier, 
            0.01f,
            _desiredLocalScale.z * HoleScaleMultiplier);
    }

    private void UpdateLocalScale()
    {
        transform.localScale = Vector3.Lerp(transform.localScale, _desiredLocalScale, HoleScaleTime * Time.deltaTime);
    }
}
