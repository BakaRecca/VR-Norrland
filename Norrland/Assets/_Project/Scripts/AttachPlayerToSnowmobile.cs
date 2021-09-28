using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttachPlayerToSnowmobile : MonoBehaviour
{
    private Transform _transform;
    private Transform _playerTransform;

    private void Awake()
    {
        _transform = transform;
    }

#if UNITY_EDITOR
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            AttachPlayer();
            Debug.Log($"Player Attached");
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            DetachPlayer();
        }
    }
#endif

    public void FindAndSetPlayer()
    {
        _playerTransform = PlayerController.Instance.transform;
    }

    public void AttachPlayer()
    {
        _playerTransform.position = _transform.position;
        _playerTransform.rotation = _transform.rotation;
    }

    public void DetachPlayer()
    {
    }
}
