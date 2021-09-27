using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttachPlayerToSnowmobile : MonoBehaviour
{
    Transform attachPointTrans;
    [SerializeField] GameObject player;


    void Start()
    {
        attachPointTrans = transform;
    }

    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {

            AttachPlayer();
            /*AttachPlayer();*/
            Debug.Log($"Player Attached");
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            DetachPlayer();
        }
    }

    

    public void AttachPlayer()
    {
        player.transform.parent = attachPointTrans;
        player.transform.position = attachPointTrans.position;
        player.transform.rotation = attachPointTrans.rotation;
        /*player.transform.localPosition = Vector3.zero;
        player.transform.localRotation = Quaternion.identity;
        player.transform.localScale = Vector3.one;*/
        Debug.Log($"Player's Parent: {player.transform.parent.name}");
    }

    public void DetachPlayer()
    {
        player.transform.parent = null;
    }
}
