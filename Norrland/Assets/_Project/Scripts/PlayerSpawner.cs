using UnityEngine;
using Valve.VR.InteractionSystem;

public class PlayerSpawner : MonoBehaviour
{
    [SerializeField] private GameObject debugPlayerObject;
        
    private GameObject playerObject;

    private void Awake()
    {
        FindAndSpawnPlayer();
    }

    private void FindAndSpawnPlayer()
    {
        Player[] playerObjects = FindObjectsOfType<Player>();

        if (playerObjects.Length <= 0 && debugPlayerObject == null)
        {
            if (debugPlayerObject == null)
            {
                Debug.LogError($"No Player Objects found!!!");
                return;
            }
            
            playerObject = debugPlayerObject;
        }
        else if (debugPlayerObject != null && playerObjects.Length > 1)
        {
            foreach (Player player in playerObjects)
            {
                if (player.gameObject == debugPlayerObject)
                    continue;

                playerObject = player.gameObject;
            }
        }
        else if (playerObjects.Length > 0)
            playerObject = playerObjects[0].gameObject;
        else
            playerObject = debugPlayerObject;
        
        playerObject.SetActive(true);

        if (debugPlayerObject != null && debugPlayerObject != playerObject)
            Destroy(debugPlayerObject);

        playerObject.transform.position = transform.position;
        
        Debug.Log($"Player Has Spawned!");
    }
}
