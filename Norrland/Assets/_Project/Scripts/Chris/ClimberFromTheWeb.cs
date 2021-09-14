using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

[RequireComponent(typeof(Rigidbody))]
public class ClimberFromTheWeb : MonoBehaviour
{
    public ClimberHand RightHand;
    public ClimberHand LeftHand;
    public SteamVR_Action_Boolean ToggleGripButton;
    public SteamVR_Action_Pose position;
    public ConfigurableJoint ClimberHandle;

    private bool Climbing;
    private ClimberHand ActiveHand;
    private Rigidbody rigidBody;

    private void Awake()
    {
        
        rigidBody = GetComponent<Rigidbody>();
    }


    void Update()
    {
        updateHand(RightHand);
        updateHand(LeftHand);
        if (Climbing)
        {
            ClimberHandle.targetPosition = -ActiveHand.transform.localPosition;//update collider for hand movment
        }
    }

    void updateHand(ClimberHand Hand)
    {
        if (Climbing && Hand == ActiveHand)//if is the hand used for climbing check if we are letting go.
        {
            if (ToggleGripButton.GetStateUp(Hand.Hand))
            {
                ClimberHandle.connectedBody = null;
                Climbing = false;

                rigidBody.useGravity = true;
            }
        }
        else
        {
            if (ToggleGripButton.GetStateDown(Hand.Hand) || Hand.grabbing)
            {
                Debug.Log("Kommer vi ens hit ? ");
                Hand.grabbing = true;
                if (Hand.TouchedCount > 0)
                {
                    Debug.Log($"count, {Hand.TouchedCount}");
                    ActiveHand = Hand;
                    Climbing = true;
                    ClimberHandle.transform.position = Hand.transform.position;
                    rigidBody.useGravity = false;
                    ClimberHandle.connectedBody = rigidBody;
                    Hand.grabbing = false;
                    Debug.Log($"Kom counten hit, {Hand.TouchedCount}");
                }
            }
        }
    }
}