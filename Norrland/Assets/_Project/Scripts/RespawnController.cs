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

        private void Update()
        {
            if (restartAction.stateDown)
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            
            if (respawnAction.stateDown)
            {
                foreach (Respawner item in _items)
                {
                    item.Respawn();
                }
            }
        }
    }
}