using UnityEngine;
using Valve.VR;

public class ClimberSteam : MonoBehaviour
{
    public static ClimberSteam Instance;
    
    [Header("DEBUG")]
    [SerializeField] private bool log;

    private Transform _transform;
    private CharacterController _characterController;
    private PlayerController _playerController;

    private SteamVR_Behaviour_Pose _climbingHand;
    
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance != null)
            Destroy(gameObject);

        _transform = transform;
        _characterController = GetComponent<CharacterController>();
        _playerController = GetComponent<PlayerController>();
    }

    private void FixedUpdate()
    {
        if (!_climbingHand)
            return;
        
        Climb();
    }

    private void Climb()
    {
        Vector3 handVelocity = _climbingHand.GetVelocity();
        Vector3 climbVelocity = new Vector3(handVelocity.x, -handVelocity.y, handVelocity.z);
        _characterController.Move(_transform.localRotation * climbVelocity * Time.deltaTime);
    }

    public void SetHand(SteamVR_Behaviour_Pose hand)
    {
        if (log)
            Debug.Log($"SET NEW Hand: {hand.inputSource} - old: {(_climbingHand != null ? _climbingHand.inputSource : (SteamVR_Input_Sources?)null)}");
        
        _climbingHand = hand;

        UpdatePlayerController();
    }

    public void RemoveHand(SteamVR_Behaviour_Pose hand)
    {
        if (_climbingHand == null)
            return;

        if (hand.inputSource == _climbingHand.inputSource)
        {
            if (log)
                Debug.Log($"REMOVE Hand: {hand.inputSource} - Yes!!!");

            _climbingHand = null;
        }
        else if (log)
            Debug.Log($"REMOVE Hand: {hand.inputSource} ??? No...");

        UpdatePlayerController();
    }

    private void UpdatePlayerController()
    {
        _playerController.enabled = _climbingHand == null;
    }
}
