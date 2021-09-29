using UnityEngine;
using Valve.VR.InteractionSystem;

[RequireComponent(typeof(Interactable))]
public class ForcePositionAttach : MonoBehaviour
{
    private Transform trans;
    private Interactable _interactable;

    private void Awake()
    {
        _interactable = GetComponent<Interactable>();
        trans = transform;
    }

    private void OnHandHoverBegin(Hand hand)
    {
        hand.ShowGrabHint();
    }

    private void OnHandHoverEnd(Hand hand)
    {
        hand.HideGrabHint();
    }

    private void HandHoverUpdate(Hand hand)
    {
        GrabTypes grabType = hand.GetGrabStarting();
        bool isGrabEnding = hand.IsGrabEnding(gameObject);

        //Grab the object
        if (_interactable.attachedToHand == null && grabType != GrabTypes.None)
        {
            hand.AttachObject(gameObject, grabType);
            hand.HoverLock(_interactable);
            hand.HideGrabHint();
        }

        //Release the object
        else if (isGrabEnding)
        {
            hand.DetachObject(gameObject);
            hand.HoverUnlock(_interactable);
        }
    }

    private void HandAttachedUpdate(Hand hand)
    {
        /*Debug.Log($"Hand position: {hand.transform.position}. Handle position: {trans.position}");*/

        hand.transform.position = trans.position;

        if (hand.IsGrabEnding(this.gameObject))
        {
            hand.DetachObject(gameObject);
        }
    }
}
