using UnityEngine;
using UnityEngine.XR;

public class SetVROrNot : MonoBehaviour
{
    [SerializeField] private GameObject VRRig;
    [SerializeField] private GameObject OfflineRig;

    [SerializeField] private VRMode VRMode;
    
    [Header("DEBUG")]
    [SerializeField] private bool log;

    private bool _isOnVR;

    private void Awake()
    {
        if (VRMode == VRMode.VR)
            FindVRHeadset();
        else
            _isOnVR = false;
        
        VRRig.SetActive(_isOnVR);
        OfflineRig.SetActive(!_isOnVR);
    }

    private void FindVRHeadset()
    {
        switch (XRSettings.isDeviceActive)
        {
            case false:
                if (log)
                    Debug.Log("No headset plugged");
                _isOnVR = false;
                break;
            case true when (XRSettings.loadedDeviceName == "Mock HMD" || XRSettings.loadedDeviceName == "MockHMDDisplay"):
                if (log)
                    Debug.Log("Using Mock HMD");
                _isOnVR = false;
                break;
            default:
                if (log)
                    Debug.Log("We have a headset" + XRSettings.loadedDeviceName);
                _isOnVR = true;
                break;
        }
    }
}

internal enum VRMode
{
    VR,
    Offline
}