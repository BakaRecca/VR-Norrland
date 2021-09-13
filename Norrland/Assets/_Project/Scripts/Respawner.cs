using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Respawner : MonoBehaviour
{
    [SerializeField, Range(1f, 50f)] private float respawnDistance;
    
    [Header("DEBUG")]
    [SerializeField] private bool log;
    
    private Transform _transform;
    private Vector3 _startPosition;
    private Quaternion _startRotation;
    private Rigidbody _rigidbody;

    private void Reset()
    {
        respawnDistance = 5f;
    }

    private void Awake()
    {
        _transform = transform;
        _startPosition = _transform.position;
        _startRotation = _transform.rotation;
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (_rigidbody.IsSleeping())
            return;

        if (IsTooFarAway())
            Respawn();
    }

    public void Respawn()
    {
        if (log)
            Debug.Log($"{name} has respawned! Distance: {Vector3.Distance(_startPosition, _transform.position)} > respawnDistance: {respawnDistance}");
        
        _transform.position = _startPosition;
        _transform.rotation = _startRotation;
        _rigidbody.velocity = Vector3.zero;
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
