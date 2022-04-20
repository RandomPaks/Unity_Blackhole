using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(HoleGenerator))]
public class HoleController : MonoBehaviour
{
    [Header("Hole Settings")]
    [Tooltip("Size of the hole")]
    public float HoleScale = 0.5f;

    [Header("Movement Settings")]
    [Tooltip("Speed of the hole controller")]
    public float MoveSpeed = 2.0f;

    private bool _isMoving;
    private Vector3 _moveDirection;
    private int _layerMask;

    private HoleGenerator _holeGenerator;
    private CharacterController _characterController;
    private GameObject _mainCameraObject;
    private Camera _mainCamera;

    private void Awake()
    {
        _holeGenerator = GetComponent<HoleGenerator>();
        _characterController = GetComponent<CharacterController>();

        // get a reference to our main camera
        if (_mainCameraObject == null)
        {
            _mainCameraObject = GameObject.FindGameObjectWithTag("MainCamera");
            _mainCamera = _mainCameraObject.GetComponent<Camera>();
        }
    }

    private void Start()
    {
        //used to raycast from camera to floor
        _layerMask = LayerMask.GetMask("Floor Raycast");

        //cuts the initial hole on the first frame
        _holeGenerator.CutHole(new Vector2(transform.position.x, transform.position.z), transform.localScale * HoleScale);
    }

    private void Update()
    {
        if (_isMoving) 
            _holeGenerator.CutHole(transform.position, transform.localScale * HoleScale);
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        _isMoving = false;

        if (Input.GetMouseButton(0))
        {
            _isMoving = true;

            RaycastHit hit;
            Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 999f, _layerMask))
            {
                _moveDirection = (hit.point - transform.position).normalized;
            }

            _characterController.Move(_moveDirection * MoveSpeed * Time.deltaTime);
        }
    }
}
