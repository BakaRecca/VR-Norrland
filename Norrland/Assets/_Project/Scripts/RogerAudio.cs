using System.Collections.Generic;
using UnityEngine;

public class RogerAudio : MonoBehaviour
{
    public static RogerAudio Instance;
    
    [SerializeField] List<AudioClip> audioClips;
    [SerializeField] private bool rogerWakesUp;

    private AudioSource source;
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;    
        }
        else if (Instance != null)
        {
            Destroy(gameObject);
        }

        source = GetComponent<AudioSource>();
        
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        if (rogerWakesUp)
        {
            Play(RogerAudioType.BedSqueking);
            Invoke(nameof(WakeUpRoger), 3f);
        }

    }

    public void Play(RogerAudioType audioType, float volume = 1f)
    {
        source.clip = audioClips[(int)audioType];
        source.volume = volume;
        source.Play();
    }

    private void WakeUpRoger()
    {
        Play(RogerAudioType.WakingUpInCabin);
    }

    private void BedDoesSound()
    {
        Play(RogerAudioType.BedSqueking);
    }

    public void RogerHouseTrigger()
    {
        Play(RogerAudioType.BeforeHeadingOut);
    }

    public void RogerPacked()
    {
        Play(RogerAudioType.BackpackPacked);
    }

    public void RogerStandingByScooter()
    {
        Play(RogerAudioType.MaybeTakeScooter);
    }

    public void RogerRidingTheScooter()
    {
        Play(RogerAudioType.WhatAGlide);
    }

    public void RogerLookingAtIce()
    {
        Play(RogerAudioType.ThinIceImNotAPussy);
    }

    public void RogerFallsIN()
    {
        Play(RogerAudioType.FallsIN);
    }

}
