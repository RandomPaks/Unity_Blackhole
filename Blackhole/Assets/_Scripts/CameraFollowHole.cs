using UnityEngine;

public class CameraFollowHole : MonoBehaviour
{
    [Header("General Settings")]
    [Tooltip("Offset of the camera position")]
    public Vector3 CameraOffsetPosition;

    [Header("Zoom Settings")]
    [Tooltip("How zoomed in the camera is initially")]
    public float ZoomLevel = 0;
    [Tooltip("How far the camera zooms in/out")]
    public float ZoomLevelIncrement = -1f;
    [Tooltip("How fast it takes for the camera to zoom in/out")]
    public float ZoomSpeed = 5.0f;

    private float _zoomDesiredLevel;

    private PlayerController _player;

    private void Start()
    {
        _player = GameManager.Instance.Player;

        _zoomDesiredLevel = ZoomLevel;
    }

    private void LateUpdate()
    {
        UpdatePosition();
    }

    private void UpdatePosition()
    {
        ZoomLevel = Mathf.MoveTowards(ZoomLevel, _zoomDesiredLevel, ZoomSpeed * Time.deltaTime);

        transform.position = _player.transform.position + CameraOffsetPosition + (transform.forward * ZoomLevel);
    }

    public void AddZoomLevel()
    {
        _zoomDesiredLevel += ZoomLevelIncrement;
    }
}