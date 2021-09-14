using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ClimbInteractibe : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other is CharacterController)
        {
            //Climber.climbingHand =
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other is CharacterController)
        {
            //Climber.climbingHand && Climber.climbingHand.name == 
        }
    }
}
