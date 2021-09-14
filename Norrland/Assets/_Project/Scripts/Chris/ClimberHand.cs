using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;
public class ClimberHand : MonoBehaviour
{
    public SteamVR_Input_Sources Hand;
    public int TouchedCount;
    public bool grabbing;

   


    void OnTriggerEnter(Collider other)
    {
        Debug.Log($"Touching {other.name}");
        if (other.CompareTag("Climbable"))
        {
            
            Debug.Log($"And is climbable {other.name}"  );
            TouchedCount++;
        }
    }
    void OnTriggerExit(Collider other)
    {
        Debug.Log($"Not touching {other.name}");
        if (other.CompareTag("Climbable"))
        {
            Debug.Log($"And is climbable {other.name}");
            TouchedCount--;
        }
    }
}