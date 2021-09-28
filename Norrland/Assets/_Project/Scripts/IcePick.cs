using UnityEngine;
using Valve.VR.InteractionSystem;

public class IcePick : MonoBehaviour
{
    [Header("Ice Pick")]
    [SerializeField] private Hand _hand;
    [SerializeField] private Transform attachmentOffset;
    [EnumFlags, SerializeField] private Hand.AttachmentFlags attachmentFlags;

    private void Reset()
    {
        attachmentFlags = Hand.AttachmentFlags.SnapOnAttach | Hand.AttachmentFlags.DetachFromOtherHand | 
                          Hand.AttachmentFlags.DetachOthers | Hand.AttachmentFlags.TurnOnKinematic;
    }

    public void Attach()
    {
        gameObject.SetActive(true);
        _hand.AttachObject(gameObject, GrabTypes.Scripted, attachmentFlags, attachmentOffset);
    }

    public void Detach()
    {
        _hand.DetachObject(gameObject);
        gameObject.SetActive(false);
    }
    
    // DELETE THIS COMMENT

    // protected override void OnAttachedToHand(Hand hand)
    // {
    //     base.OnAttachedToHand(hand);
    //     Debug.Log($"[ICE_ICE_ICE] - {nameof(OnAttachedToHand)} - {hand}");
    // }

    private void HandAttachedUpdate()
    {
        // if (Input.GetKeyDown(KeyCode.U))
        // {
        //     Debug.Log($"[ICE_ICE_ICE] - {nameof(HandAttachedUpdate)}");
        // }
    }

    // protected override void OnDetachedFromHand(Hand hand)
    // {
    //     base.OnDetachedFromHand(hand);
    //     Debug.Log($"[ICE_ICE_ICE] - {nameof(OnDetachedFromHand)} - {hand}");
    // }
}