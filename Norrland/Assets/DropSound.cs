using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropSound : MonoBehaviour
{
    public AudioSource dropSound;

    private void OnCollisionEnter(Collision item)
    {
        if (item.relativeVelocity.magnitude > 3)
        {
            dropSound.Play();
        }
    }
}