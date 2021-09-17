using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowMobileScript : MonoBehaviour
{
    Transform transform;
    Rigidbody rb;
    RaycastHit rayHit;

    [SerializeField] LayerMask layerMask;

    [SerializeField] float speed;


    void Start()
    {
        transform = GetComponent<Transform>();
        rb = GetComponent<Rigidbody>();
    }


    void Update()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        Vector3 tempVect = new Vector3(h, 0, v);
        tempVect = tempVect.normalized * speed * Time.deltaTime;
        rb.MovePosition(transform.position + tempVect);
    }

    private void FixedUpdate()
    {
        ShootRaycast();
    }

    private void ShootRaycast()
    {
        float rayLength = 2f;

        Vector3 down = transform.TransformDirection(Vector3.down);
        Vector3 up = transform.TransformDirection(Vector3.up);
        Vector3 transformOrigin = transform.position + up * 0.5f;

        if (Physics.Raycast(transformOrigin, down, out rayHit, rayLength, layerMask))
        {
            RotateUpToRayNormal(rayHit.normal);
        }

        Debug.DrawRay(transformOrigin, up * rayLength, Color.blue);
        Debug.DrawRay(transformOrigin, down * rayLength, Color.red);
    }

    private void RotateUpToRayNormal(Vector3 normal)
    {
        transform.up = Vector3.Lerp(transform.up, normal, Time.deltaTime);
    }
}
