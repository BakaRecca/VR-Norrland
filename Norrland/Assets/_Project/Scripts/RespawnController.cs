using UnityEngine;
using Valve.VR;

namespace _Project.Scripts
{
    public class RespawnController : MonoBehaviour
    {
        [SerializeField] private SteamVR_Action_Boolean respawnAction;
        
        private Respawner[] _items;

        private void Start()
        {
            _items = FindObjectsOfType<Respawner>();
        }

        private void Update()
        {
            if (respawnAction.stateDown)
            {
                Debug.Log($"SÅSÅSÅSÅS");
                foreach (Respawner item in _items)
                {
                    item.Respawn();
                }
            }
        }
    }
}