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

    public void RogerHouseTrigger()
    {
        Play(RogerAudioType.BeforeHeadingOut);
    }
}
