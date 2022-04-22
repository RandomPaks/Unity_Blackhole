using UnityEngine;

public class DroppableGenerator : MonoBehaviour
{
    [Tooltip("Number of objects to spawn inside the game")]
    public int NumObjects = 100;
    [Tooltip("Prefabs to spawn")]
    public GameObject[] ObjectsToSpawn;

    private int _objectType;

    //10 is the basis of our everything relating to ourfloor
    private const float _floorSize = 10;

    private void Start()
    {
        SpawnDifferentObjects();
    }

    private void SpawnDifferentObjects()
    {
        for (int i = 0; i < NumObjects; i++)
        {
            float rand = Mathf.InverseLerp(0, NumObjects, Random.Range(0, NumObjects)); //returns a value between 0 and 1

            if (rand < 0.5f)
            {
                _objectType = 0;
            }
            else if (rand >= 0.5f && rand < 0.75f)
            {
                _objectType = 1;
            }
            else if (rand >= 0.75f && rand < 0.90f)
            {
                _objectType = 2;
            }
            else if (rand >= 0.90f && rand < 0.98f)
            {
                _objectType = 3;
            }
            else if (rand >= 0.98f && rand < 1)
            {
                _objectType = 4;
            }

            SpawnObject();
        }
    }
    
    private void SpawnObject()
    {
        //ensures that the object is always within the bounds of the level
        float posX = _floorSize / 2 * FloorGenerator.Instance.FloorScale;
        float posZ = _floorSize / 2 * FloorGenerator.Instance.FloorScale;

        Vector3 pos = new Vector3(
            Random.Range(-posX + ObjectsToSpawn[_objectType].transform.localScale.x, posX - ObjectsToSpawn[_objectType].transform.localScale.x), //local scale make sure the object doesn't hit the wall
            ObjectsToSpawn[_objectType].transform.position.y,
            Random.Range(-posZ + ObjectsToSpawn[_objectType].transform.localScale.z, posZ - ObjectsToSpawn[_objectType].transform.localScale.z));

        Vector3 rot = new Vector3(0, Random.Range(0.0f, 360.0f), 0);

        Instantiate(ObjectsToSpawn[_objectType], pos, Quaternion.Euler(rot), gameObject.transform);
    }
}
