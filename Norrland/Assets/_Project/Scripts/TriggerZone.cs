using UnityEngine;
using UnityEngine.Events;

public class TriggerZone : MonoBehaviour
{
    [SerializeField] string tagToTrigger;
    [SerializeField] UnityEvent triggerOnEnter;
    [SerializeField] bool destroyOnTrigger = true;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag(tagToTrigger))
        {
            return;
        }

        triggerOnEnter.Invoke();

        if (destroyOnTrigger)
        {
            Destroy(gameObject);
        }

    }
}
