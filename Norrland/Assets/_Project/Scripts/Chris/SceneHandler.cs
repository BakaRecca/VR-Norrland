using UnityEngine;
using Valve.VR.Extras;

public class SceneHandler : MonoBehaviour
{
    [SerializeField] string sceneName;
    
    [Header("DEBUG")]
    [SerializeField] private bool log;
    
    private SteamVR_LaserPointer[] _laserPointers;

    public void SetupPointers()
    {
        _laserPointers = PlayerController.Instance.LaserPointers;
        
        EnablePointers();
    }

    private void EnablePointers()
    {
        foreach (SteamVR_LaserPointer pointer in _laserPointers)
        {
            pointer.PointerIn += PointerInside;
            pointer.PointerOut += PointerOutside;
            pointer.PointerClick += PointerClick;
        }
    }

    private void OnDisable()
    {
        foreach (SteamVR_LaserPointer pointer in _laserPointers)
        {
            pointer.PointerIn -= PointerInside;
            pointer.PointerOut -= PointerOutside;
            pointer.PointerClick -= PointerClick;
        }
    }

    private void PointerClick(object sender, PointerEventArgs e)
    {
        if (e.target.name == "StartButton")
        {
            LoadLevel.Instance.StartLoadingScene(sceneName);
        }
        else if (e.target.name == "QuitButton")
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }
    }

    private void PointerInside(object sender, PointerEventArgs e)
    {
        if (!log)
            return;
        
        if (e.target.name == "StartButton" || e.target.name == "QuitButton")
        {
            // Debug.Log("Button was entered");
        }
    }

    private void PointerOutside(object sender, PointerEventArgs e)
    {
        if (!log)
            return;
        
        if (e.target.name == "StartButton" || e.target.name == "QuitButton")
        {
            // Debug.Log("Button was exited");
        }
    }
}