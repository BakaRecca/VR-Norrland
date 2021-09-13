using UnityEngine;
using Valve.VR.InteractionSystem;

public class Item : MonoBehaviour
{
    private Transform _transform;
    private Interactable _interactable;

    public bool IsHeld { get; private set; }

    private void Awake()
    {
        _transform = transform;
        _interactable = GetComponent<Interactable>();
    }

    private void OnAttachedToHand(Hand hand)
    {
        IsHeld = true;
    }
    
    private void OnDetachedFromHand(Hand hand)
    {
        IsHeld = false;
    }

    public void Attach(Transform parent, Vector3 position)
    {
        CancelInteraction();
        
        _transform.SetParent(parent);
        _transform.localPosition = position;
    }

    private void CancelInteraction()
    {
        _interactable.enabled = false;

        if (TryGetComponent(out Rigidbody rigidbody))
        {
            rigidbody.isKinematic = true;
            rigidbody.useGravity = false;
        }

        if (TryGetComponent(out Collider collider))
            collider.enabled = false;
    }
}
