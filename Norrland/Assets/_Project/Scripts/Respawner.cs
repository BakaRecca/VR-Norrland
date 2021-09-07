using System;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

[RequireComponent(typeof(Rigidbody))]
public class Respawner : MonoBehaviour
{
    [SerializeField, Range(1f, 50f)] private float respawnDistance;
    
    private Transform _transform;
    private Vector3 _startPosition;
    private Rigidbody _rigidbody;

    private void Reset()
    {
        respawnDistance = 5f;
    }

    private void Awake()
    {
        _transform = transform;
        _startPosition = _transform.position;
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (_rigidbody.IsSleeping())
            return;

        if (IsTooFarAway())
            Respawn();
    }

    private void Respawn()
    {
        Debug.Log($"Respawn! Distance: {Vector3.Distance(_startPosition, _transform.position)} - respawnDistance: {respawnDistance}");
        
        _transform.position = _startPosition;
    }

    private bool IsTooFarAway()
    {
        return Vector3.Distance(_startPosition, _transform.position) > respawnDistance;
    }
    
    private void OnCollisionEnter(Collision other)
    {
        if (IsTooFarAway())
            Respawn();
    }
}
