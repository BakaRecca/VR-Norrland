using UnityEngine;
using UnityEngine.Events;

public class TriggerZone : MonoBehaviour
{
    [SerializeField] string tagToTrigger;
    [SerializeField] UnityEvent triggerOnEnter;
    
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag(tagToTrigger))
        {
            return;
        }
        
        triggerOnEnter.Invoke();

        Destroy(gameObject);
    }
}
