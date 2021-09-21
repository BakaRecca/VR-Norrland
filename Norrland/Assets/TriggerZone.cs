using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
        {
            return;
        }
        RogerAudio.instance.Play(RogerAudioType.BeforeHeadingOut);

        Destroy(gameObject);
    }



}
