using UnityEngine;

public class Climbable : MonoBehaviour
{
    [SerializeField] private float angle;

    private Vector3 _direction;
    private Vector3 _origin;

    public Vector3 Direction => _direction;

    private void Awake()
    {
        Calculate2DDirection();
        _origin = transform.position;
    }

    private void OnValidate()
    {
        Calculate2DDirection();
    }

    private void Calculate2DDirection()
    {
        _direction = new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad), 0f, Mathf.Sin(angle * Mathf.Deg2Rad));
        
        // Debug.Log($"Climbable - Direction: {_direction}");
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawRay(_origin, _direction * 100f);
        
        Gizmos.color = Color.red;
        Gizmos.DrawRay(_origin, _direction * -100f);
    }
}
