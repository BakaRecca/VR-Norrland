using UnityEngine;
using PathCreation;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class FollowPath : MonoBehaviour
{
    [SerializeField] private SteamVR_Action_Single throttle;
    [SerializeField] private SteamVR_Behaviour_Pose hand;
    [SerializeField] private float throttleAmount;
    [SerializeField] private Transform seatTransform;
    [SerializeField] private Transform getOffTransform;
    [SerializeField] private Transform playerGetOffTransform;
    [SerializeField] private float speed = 0;
    
    public bool playerIsOn;

    private Transform _transform;
    
    private float minSpeed = 0f;
    private float maxSpeed = 20f;
    private float timeZeroToMax = 4f;
    private float acceleratePerSec;
    private float distanceTravelled;

    private GameObject[] handModels;

    private Transform _playerTransform;

    public PathCreator pathCreator;

    private void Awake()
    {
        _transform = transform;
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
        else
        {
            _transform.position = getOffTransform.position;
            _transform.rotation = getOffTransform.rotation;

            _playerTransform.position = playerGetOffTransform.position;
            _playerTransform.rotation = playerGetOffTransform.rotation;
        }
        
        playerIsOn = active;
    }

    public void PlayerGetOff()
    {
        playerIsOn = false;
    }

    public void SetShowHands(bool active)
    {
        if (handModels == null)
        {
            RenderModel[] renderModels = PlayerController.Instance.GetComponentsInChildren<RenderModel>();

            /*Debug.Log(renderModels.Length);*/

            handModels = new GameObject[renderModels.Length];

            for (int i = 0; i < renderModels.Length; i++)
            {
                handModels[i] = renderModels[i].gameObject;
            }
        }
        

        foreach (var handModel in handModels)
        {
            handModel.SetActive(active);
        }
    }
}
