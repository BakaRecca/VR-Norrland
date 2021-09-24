using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;
using Valve.VR;

public class FollowPath : MonoBehaviour
{
    [SerializeField] private SteamVR_Action_Single throttle;
    [SerializeField] private SteamVR_Behaviour_Pose hand;
    [SerializeField] private float throttleAmount;
    
    [SerializeField] private float speed = 0;

    private float minSpeed = 0f;
    private float maxSpeed = 20f;
    private float timeZeroToMax = 4f;
    private float acceleratePerSec;
    private float distanceTravelled;

    public PathCreator pathCreator;

    private void Awake()
    {
        acceleratePerSec = maxSpeed / timeZeroToMax;
        speed = 0f;
    }

    private void FixedUpdate()
    {
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
    }

    void Update()
    {
        throttleAmount = throttle.GetAxis(hand.inputSource);       
    }
}
