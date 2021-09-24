using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;
using Valve.VR;

public class FollowPath : MonoBehaviour
{
    [SerializeField] private SteamVR_Action_Single throttle;
    [SerializeField] private SteamVR_Behaviour_Pose hand;

    public PathCreator pathCreator;

    public float minSpeed = 0f;
    public float maxSpeed = 20f;
    public float timeZeroToMax = 4f;
    float acceleratePerSec;

    public float speed = 0;

    private float throttleAmount;
    private float distanceTravelled;

    private void Awake()
    {
        acceleratePerSec = maxSpeed / timeZeroToMax;
        speed = 0f;
    }

    private void FixedUpdate()
    {
        Debug.Log("FixedUpdate realTime: " + Time.realtimeSinceStartup);

        if (throttleAmount < 0.01f)
        {
            if (speed > 0f)
            {
                speed *= 0.95f;

                /*speed -= acceleratePerSec * Time.deltaTime;*/
            }
            Debug.Log($"Gasar såhär mycket: {throttleAmount}");
        }
        else
        {
            speed += acceleratePerSec * Time.deltaTime * throttleAmount;
            Debug.Log($"Gasar såhär mycket: {throttleAmount}");
        }

        speed = Mathf.Clamp(speed, minSpeed, maxSpeed);

        distanceTravelled += speed * Time.deltaTime;
        transform.position = pathCreator.path.GetPointAtDistance(distanceTravelled);
        transform.rotation = pathCreator.path.GetRotationAtDistance(distanceTravelled);
    }

    void Update()
    {
        Debug.Log("Update realTime: " + Time.realtimeSinceStartup);

        throttleAmount = throttle.GetAxis(hand.inputSource);       
    }
}
