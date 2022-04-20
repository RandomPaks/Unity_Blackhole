using UnityEngine;

public class DroppableObject : MonoBehaviour
{
    [Tooltip("How much score the object gives")]
    public int Score;

    private void Awake()
    {
        //GetComponent<Rigidbody>().Sleep();
    }

    private void Update()
    {
        Debug.Log(GetComponent<Rigidbody>().IsSleeping());
    }

    private void OnTriggerEnter(Collider other)
    {

    }

    private void OnTriggerExit(Collider other)
    {
        
    }
}