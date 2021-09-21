using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RogerAudio : MonoBehaviour
{
    [SerializeField] List<AudioClip> audioClips;

    private AudioSource source;

    static public RogerAudio instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;    
        }

        else if (instance != null)
        {
            Destroy(gameObject);
        }

        source = GetComponent<AudioSource>();
    }

    public void Play(RogerAudioType audioType, float volume)
    {
        source.clip = audioClips[((int)audioType)];
        source.volume = volume;
        source.Play();
    }
}
