using UnityEngine;
using Valve.VR;
using UnityEngine.SceneManagement;

namespace _Project.Scripts
{
    public class RespawnController : MonoBehaviour
    {
        [SerializeField] private SteamVR_Action_Boolean respawnAction;
        [SerializeField] private SteamVR_Action_Boolean restartAction;
        
        private Respawner[] _items;

        private void Start()
        {
            _items = FindObjectsOfType<Respawner>();
        }

        private void OnEnable()
        {
            respawnAction.onStateDown += RespawnAllObjects;
            restartAction.onStateDown += RestartScene;
        }

        private void OnDisable()
        {
            respawnAction.onStateDown -= RespawnAllObjects;
            restartAction.onStateDown -= RestartScene;
        }

        private void RespawnAllObjects(SteamVR_Action_Boolean fromaction, SteamVR_Input_Sources fromsource)
        {
            foreach (Respawner item in _items)
            {
                item.Respawn();
            }
        }

        private void RestartScene(SteamVR_Action_Boolean fromaction, SteamVR_Input_Sources fromsource)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}