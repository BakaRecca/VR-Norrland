using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class IcePick : MonoBehaviour
{
    [Header("Ice Pick")]
    [SerializeField] private Hand _hand;
    [SerializeField] private Transform attachmentOffset;
    [EnumFlags, SerializeField] private Hand.AttachmentFlags attachmentFlags;
    
    [Header("DEBUG")]
    [SerializeField] private bool log;

    private SteamVR_Behaviour_Pose _handPose;

    private void Reset()
    {
        attachmentFlags = Hand.AttachmentFlags.SnapOnAttach | Hand.AttachmentFlags.DetachFromOtherHand | 
                          Hand.AttachmentFlags.DetachOthers | Hand.AttachmentFlags.TurnOnKinematic;
    }

    private void Awake()
    {
        _handPose = _hand.GetComponent<SteamVR_Behaviour_Pose>();
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

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Climbable"))
            return;
        
        if (log)
            Debug.Log($"IcePick Enter: {other.name}");

        if (!other.TryGetComponent(out Climbable climbable))
            return;
        
        ClimberSteam.Instance.SetHand(_handPose, climbable.Direction);
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Climbable"))
            return;
        
        // Debug.Log($"IcePick Exit: {other.name}");
        ClimberSteam.Instance.RemoveHand(_handPose);
    }
}
