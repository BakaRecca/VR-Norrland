using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class HoleAppear : MonoBehaviour
{
    [SerializeField] private Mesh solid;
    [SerializeField] private Mesh hole;

    [SerializeField] private AudioClip audioClip;
    [SerializeField, Range(0f, 1f)] private float volume = 1f;

    public UnityEvent onHoleIsDrilled;

    private MeshFilter _meshfilter;
    private ParticleSystem _particleSystem;

    private bool _isDrilling;

    private void Awake()
    {
        _meshfilter = GetComponentInChildren<MeshFilter>();
        _particleSystem = GetComponentInChildren<ParticleSystem>();
        
        _meshfilter.mesh = solid;
        _particleSystem.Stop();
    }

    private void Start()
    {
        
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("IceDrill"))
            return;
        
        if (_isDrilling)
            return;

        StartCoroutine(DrillTheHole());

        _isDrilling = true;
    }
    
    private IEnumerator DrillTheHole()
    {
        Debug.Log("Start drilling");

        _particleSystem.Play();
        PlayAudio();
        yield return new WaitForSeconds(2f);

        _meshfilter.mesh = hole;
        _particleSystem.Stop();
        
        onHoleIsDrilled.Invoke();

        Debug.Log("DrillingDne");
    }
    
    private void PlayAudio()
    {
        AudioSource.PlayClipAtPoint(audioClip, transform.position, volume);
    }
}
