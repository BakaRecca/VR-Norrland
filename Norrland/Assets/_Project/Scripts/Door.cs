using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;
using Valve.VR;

public class Door : MonoBehaviour
{
    [SerializeField] SteamVR_Action_Boolean action;

    private AudioSource audioSource;
    [SerializeField] string sceneToLoad;

    bool isOpen = false;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void OpenDoor()
    {
        if (!isOpen)
        {
            return;
        }

        LoadLevel.Instance.StartLoadingScene(sceneToLoad);
    }

    public void UnlockDoor()
    {
        isOpen = true;
        audioSource.Play();
        //RogerAudio.Instance.Play(RogerAudioType.WakingUpInCabin);
    }

    void HandHoverUpdate(Hand hand)
    {
        SteamVR_Behaviour_Pose pose = hand.GetComponent<SteamVR_Behaviour_Pose>();
        if (action.GetStateDown(pose.inputSource))
        {
            OpenDoor();
        }
    }
}
