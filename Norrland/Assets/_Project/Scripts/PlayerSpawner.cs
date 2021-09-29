using UnityEngine;
using UnityEngine.Events;
using Valve.VR.InteractionSystem;

public class PlayerSpawner : MonoBehaviour
{
    [SerializeField] private GameObject debugPlayerObject;
    [SerializeField] private SceneType sceneType;

    [Header("DEBUG")]
    [SerializeField] private bool log;

    public UnityEvent onPlayerSpawned;

    private Transform _transform;
    private GameObject _playerObject;
    private PlayerController _playerController;

    private void Awake()
    {
        _transform = transform;
        
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
            
            _playerObject = debugPlayerObject;
        }
        else if (debugPlayerObject != null && playerObjects.Length > 1)
        {
            foreach (Player player in playerObjects)
            {
                if (player.gameObject == debugPlayerObject)
                    continue;

                _playerObject = player.gameObject;
            }
        }
        else if (playerObjects.Length > 0)
            _playerObject = playerObjects[0].gameObject;
        else
            _playerObject = debugPlayerObject;
        
        _playerObject.SetActive(true);

        if (debugPlayerObject != null && debugPlayerObject != _playerObject)
            Destroy(debugPlayerObject);

        _playerObject.transform.position = _transform.position;
        _playerObject.transform.rotation = _transform.rotation;
        
        _playerController = _playerObject.GetComponent<PlayerController>();

        InitScene();
        
        onPlayerSpawned.Invoke();

        if (log)
            Debug.Log($"Player Has Spawned!");
    }

    private void InitScene()
    {
        switch (sceneType)
        {
            case SceneType.Menu:
                InitMenu();
                break;
            case SceneType.Cabin:
                InitCabin();
                break;
            case SceneType.Lake:
                InitLake();
                break;
        }
    }
    
    private void InitMenu()
    {
        _playerController.DisableTeleporting();
        _playerController.DisableMotionMovement();
        _playerController.DisableClimbing();
        
        _playerController.EnableLaserPointers();
    }

    private void InitCabin()
    {
        _playerController.DisableLaserPointers();
        _playerController.DisableMotionMovement();
        _playerController.DisableClimbing();
        
        _playerController.EnableTeleporting();
    }
    
    private void InitLake()
    {
        _playerController.DisableLaserPointers();
        _playerController.DisableMotionMovement();
        _playerController.DisableClimbing();
        
        _playerController.EnableTeleporting();
    }
}
