using UnityEngine;
using Valve.VR;

public class ClimberHandSteam : MonoBehaviour
{
    [SerializeField] private SteamVR_Action_Boolean grabAction;

    [Header("DEBUG")]
    [SerializeField] private bool log;

    private SteamVR_Behaviour_Pose _hand;

    private int _touchCount;

    private void Awake()
    {
        _hand = GetComponent<SteamVR_Behaviour_Pose>();
    }

    private void Update()
    {
        InputCheck();
    }

    private void InputCheck()
    {
        if (grabAction.GetStateDown(_hand.inputSource))
        {
            if (log)
                Debug.Log($"{_hand.inputSource} Trigger Down");
            
            Grab();
        }
        else if (grabAction.GetStateUp(_hand.inputSource))
        {
            if (log)
                Debug.Log($"{_hand.inputSource} Trigger Up");
            
            Release();
        }
    }

    private void Grab()
    {
        if (_touchCount <= 0)
            return;
        
        if (log)
            Debug.Log($"{_hand.inputSource} Trigger Down");
        
        ClimberSteam.Instance.SetHand(_hand);
    }
    
    private void Release()
    {
        if (log)
            Debug.Log($"{_hand.inputSource} Trigger Up");
        
        ClimberSteam.Instance.RemoveHand(_hand);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Climbable"))
            return;

        _touchCount++;

        if (log)
            Debug.Log($"{other.name} ENTER - count: {_touchCount}");
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Climbable"))
            return;

        _touchCount--;

        if (log)
            Debug.Log($"{other.name} EXIT - count: {_touchCount}");
    }
}
