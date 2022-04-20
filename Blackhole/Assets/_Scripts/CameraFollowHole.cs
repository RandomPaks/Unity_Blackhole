using UnityEngine;

public class CameraFollowHole : MonoBehaviour
{
    [Tooltip("Offset of the camera position")]
    public Vector3 cameraOffsetPosition;

    private GameObject _playerObject;
    private PlayerController _playerController;

    private void Awake()
    {
        if (_playerObject == null)
        {
            _playerObject = GameObject.FindGameObjectWithTag("Player");
            _playerController = _playerObject.GetComponent<PlayerController>();
        }
    }

    private void LateUpdate()
    {
        transform.position = _playerObject.transform.position + cameraOffsetPosition;
    }
}