using UnityEngine;
using PathCreation;
using Valve.VR;

public class FollowPath : MonoBehaviour
{
    [SerializeField] private SteamVR_Action_Single throttle;
    [SerializeField] private SteamVR_Behaviour_Pose hand;
    [SerializeField] private float throttleAmount;
    [SerializeField] private Transform seatTransform;
    [SerializeField] private float speed = 0;
    public bool playerIsOn;

    private float minSpeed = 0f;
    private float maxSpeed = 20f;
    private float timeZeroToMax = 4f;
    private float acceleratePerSec;
    private float distanceTravelled;

    private Transform _playerTransform;

    public PathCreator pathCreator;

    private void Awake()
    {
        acceleratePerSec = maxSpeed / timeZeroToMax;
        speed = 0f;
    }

    private void FixedUpdate()
    {
        if (!playerIsOn)
            return;
        
        if (throttleAmount < 0.01f)
        {
            if (speed > 0f)
            {
                speed *= 0.95f;

                /*speed -= acceleratePerSec * Time.deltaTime;*/
            }
        }
        else
        {
            speed += acceleratePerSec * Time.deltaTime * throttleAmount;
        }

        speed = Mathf.Clamp(speed, minSpeed, maxSpeed);

        distanceTravelled += speed * Time.deltaTime;
        transform.position = pathCreator.path.GetPointAtDistance(distanceTravelled);
        transform.rotation = pathCreator.path.GetRotationAtDistance(distanceTravelled);
        
        _playerTransform.position = seatTransform.position;
        _playerTransform.rotation = seatTransform.rotation;
    }

    void Update()
    {
        if (!playerIsOn)
            return;
        
        throttleAmount = throttle.GetAxis(hand.inputSource);
        // Debug.Log($"throttleAmount: {throttleAmount}");
    }
    
    public void FindAndSetPlayer()
    {
        hand = PlayerController.Instance.Hands[1].GetComponent<SteamVR_Behaviour_Pose>();
    }

    public void SetPlayerIsOn(bool active)
    {
        if (active)
            _playerTransform = PlayerController.Instance.transform;
        
        playerIsOn = active;
    }
}
