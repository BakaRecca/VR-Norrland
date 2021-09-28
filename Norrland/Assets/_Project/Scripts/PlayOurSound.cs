using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayOurSound : MonoBehaviour
{
    [SerializeField] List<AudioClip> audioClips;

    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void Play()
    {
        audioSource.clip = audioClips[Random.Range(0, audioClips.Count)];
        audioSource.Play();
    }

}
