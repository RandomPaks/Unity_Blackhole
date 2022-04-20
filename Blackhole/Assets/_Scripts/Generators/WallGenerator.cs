using UnityEngine;

public class WallGenerator : MonoBehaviour
{
    [Header("For Walls")]
    [Tooltip("The wall gameobject used to bound the player within the environment")]
    [SerializeField] private GameObject _wallGameObject;

    private void Start()
    {
        //north and south walls
        CreateWall(0, FloorGenerator.Instance.FloorScale * 5, Quaternion.identity);
        CreateWall(0, -FloorGenerator.Instance.FloorScale * 5, Quaternion.identity);

        //east and west walls
        CreateWall(FloorGenerator.Instance.FloorScale * 5, 0, Quaternion.Euler(0, 90, 0));
        CreateWall(-FloorGenerator.Instance.FloorScale * 5, 0, Quaternion.Euler(0, 90, 0));
    }

    private void CreateWall(float xPos, float zPos, Quaternion rot)
    {
        GameObject temp = Instantiate(_wallGameObject, new Vector3(xPos, 0.5f, zPos), rot, gameObject.transform);
        temp.transform.localScale = new Vector3(FloorGenerator.Instance.FloorScale * 10, 1, 0.1f);
    }
}
