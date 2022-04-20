using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowHole : MonoBehaviour
{
    [Tooltip("Offset of the camera position")]
    public Vector3 cameraOffsetPosition;

    private GameObject _holeObject;
    private HoleController _holeController;

    private void Awake()
    {
        if (_holeObject == null)
        {
            _holeObject = GameObject.FindGameObjectWithTag("Player");
            _holeController = _holeObject.GetComponent<HoleController>();
        }
    }

    private void LateUpdate()
    {
        transform.position = _holeObject.transform.position + cameraOffsetPosition;
    }
}
