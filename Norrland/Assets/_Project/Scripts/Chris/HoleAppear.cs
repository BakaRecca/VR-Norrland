using System.Collections;
using UnityEngine;

public class HoleAppear : MonoBehaviour
{
    [SerializeField] private Mesh solid;
    [SerializeField] private Mesh hole;

    [SerializeField] private AudioClip audioClip;
    [SerializeField, Range(0f, 1f)] private float volume = 1f;

    private MeshFilter _meshfilter;
    private ParticleSystem _particleSystem;

    private void Awake()
    {
        _meshfilter = GetComponentInChildren<MeshFilter>();
        _particleSystem = GetComponentInChildren<ParticleSystem>();
    }

    private void Start()
    {
        _meshfilter.mesh = solid;
        _particleSystem.Stop();
    }
    
    private void OnTriggerStay(Collider other)
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            StartCoroutine(DrillTheHole());
        }
    }
    
    private IEnumerator DrillTheHole()
    {
        Debug.Log("Start drilling");

        _particleSystem.Play();
        PlayAudio();
        yield return new WaitForSeconds(2f);

        _meshfilter.mesh = hole;
        _particleSystem.Stop();

        Debug.Log("DrillingDne");
    }
    
    private void PlayAudio()
    {
        AudioSource.PlayClipAtPoint(audioClip, transform.position, volume);
    }
}
