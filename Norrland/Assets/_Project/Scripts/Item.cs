using UnityEngine;

public class Item : MonoBehaviour
{
    private Transform _transform;
    
    private void Awake()
    {
        _transform = transform;
    }

    private void OnTriggerEnter(Collider other)
    {
        // Debug.Log($"{other.name} {nameof(OnTriggerEnter)}");
    }
    
    private void OnTriggerStay(Collider other)
    {
        // Debug.Log($"{other.name} {nameof(OnTriggerStay)}");
    }
    
    private void OnTriggerExit(Collider other)
    {
        // Debug.Log($"{other.name} {nameof(OnTriggerExit)}");
    }
}
