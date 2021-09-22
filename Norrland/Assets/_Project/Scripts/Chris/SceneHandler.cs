using UnityEngine;
using Valve.VR.Extras;

public class SceneHandler : MonoBehaviour
{
    [SerializeField] private SteamVR_LaserPointer[] laserPointers;
    [SerializeField] string sceneName;
    
    [Header("DEBUG")]
    [SerializeField] private bool log;

    private void OnEnable()
    {
        foreach (SteamVR_LaserPointer pointer in laserPointers)
        {
            pointer.PointerIn += PointerInside;
            pointer.PointerOut += PointerOutside;
            pointer.PointerClick += PointerClick;
        }
    }

    private void OnDisable()
    {
        foreach (SteamVR_LaserPointer pointer in laserPointers)
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