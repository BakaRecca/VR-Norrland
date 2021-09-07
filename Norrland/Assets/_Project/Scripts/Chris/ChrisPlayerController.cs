using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;
public class ChrisPlayerController : MonoBehaviour
{

    public SteamVR_Action_Vector2 input;
    public float speed = 1f;

    private CharacterController characterController;

    // Need to create a joystick / Touchpad movement in episode 1

    void Update()
    {
        if (input.axis.magnitude > 0.1f)
        {
            Vector3 direction = Player.instance.hmdTransform.TransformDirection(input.axis.x, 0, input.axis.y);
            characterController.Move(speed * Time.deltaTime * Vector3.ProjectOnPlane(direction, Vector3.up) - new Vector3(0, 9.81f, 0) * Time.deltaTime);
        }

        
    }
}
