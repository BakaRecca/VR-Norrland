using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RogerAudio : MonoBehaviour
{
    [SerializeField] List<AudioClip> audioClips;

    private AudioSource source;

    static public RogerAudio instance;

   [SerializeField] bool RogerWakesUp = false;

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

    private void Start()
    {
        if (RogerWakesUp)
        {
            Invoke("WakeUpRoger", 3f);
        }
    }

    public void Play(RogerAudioType audioType, float volume = 1f)
    {
        source.clip = audioClips[((int)audioType)];
        source.volume = volume;
        source.Play();
    }

    void WakeUpRoger()
    {
        Play(RogerAudioType.WakingUpInCabin);
    }
}
