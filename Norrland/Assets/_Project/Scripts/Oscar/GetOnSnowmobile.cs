using UnityEngine;
using UnityEngine.Events;
using Valve.VR.InteractionSystem;

[RequireComponent(typeof(Interactable))]
public class GetOnSnowmobile : MonoBehaviour
{
    public UnityEvent onTrigger;
    
    private Interactable _interactable;

    private bool _attached;

    private void Awake()
    {
        _interactable = GetComponent<Interactable>();
    }

    private void OnHandHoverBegin(Hand hand)
    {
        if (_attached)
            return;
        hand.ShowGrabHint();
    }

    private void OnHandHoverEnd(Hand hand)
    {
        hand.HideGrabHint();
    }

    private void HandHoverUpdate(Hand hand)
    {
        if (_attached)
            return;
            
        GrabTypes grabType = hand.GetGrabStarting();
        bool isGrabEnding = hand.IsGrabEnding(gameObject);
        
        if (_interactable.attachedToHand == null && grabType != GrabTypes.None)
        {
            onTrigger.Invoke();
            _attached = true;
        }

        // GrabTypes grabType = hand.GetGrabStarting();
        // bool isGrabEnding = hand.IsGrabEnding(gameObject);
        //
        // //Grab the object
        // if (_interactable.attachedToHand == null && grabType != GrabTypes.None)
        // {
        //     hand.AttachObject(gameObject, grabType);
        //     hand.HoverLock(_interactable);
        //     hand.HideGrabHint();
        // }
        //
        // //Release the object
        // else if (isGrabEnding)
        // {
        //     hand.DetachObject(gameObject);
        //     hand.HoverUnlock(_interactable);
        // }
    }
}
