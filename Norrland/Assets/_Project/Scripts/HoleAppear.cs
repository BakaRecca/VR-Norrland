using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoleAppear : MonoBehaviour
{
    [SerializeField] Mesh solid;
    [SerializeField] Mesh hole;
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

        yield return new WaitForSeconds(2f);

        meshfilter.mesh = hole;
        particleSystem.Stop();

        Debug.Log("DrillingDne");
    }

}
