using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoleAppear : MonoBehaviour
{
    [SerializeField] Mesh solid;
    [SerializeField] Mesh hole;

    [SerializeField] AudioClip audioClip;
    public float volume = 1f;

    MeshFilter meshfilter;

    

    ParticleSystem particleSystem;

    void Awake()
    {
        meshfilter = GetComponentInChildren<MeshFilter>();
        particleSystem = GetComponentInChildren<ParticleSystem>();

    }

    private void Start()
    {
        meshfilter.mesh = solid;
        particleSystem.Stop();
    }


    private void OnTriggerStay(Collider other)
    {

        if (Input.GetKeyDown(KeyCode.K))
        {
            StartCoroutine(DrillTheHole());
        }
    }


    IEnumerator DrillTheHole()
    {
        Debug.Log("Start drilling");

        particleSystem.Play();
        PlayAudio();
        yield return new WaitForSeconds(2f);

        meshfilter.mesh = hole;
        particleSystem.Stop();

        Debug.Log("DrillingDne");
    }


    void PlayAudio()
    {
        AudioSource.PlayClipAtPoint(audioClip, transform.position, volume);
    }


}
