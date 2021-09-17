using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Valve.VR.Extras;

public class SceneHandler : MonoBehaviour
{
    [SerializeField] public SteamVR_LaserPointer laserPointer;

    void Awake()
    {
        laserPointer.PointerIn += PointerInside;
        laserPointer.PointerOut += PointerOutside;
        laserPointer.PointerClick += PointerClick;
    }

    public void PointerClick(object sender, PointerEventArgs e)
    {
        
        if (e.target.name == "StartButton")
        {
            LoadLevel.instance.StartLoadingScene("LoadingScene_Cabin_Test");
            Debug.Log("Button was clicked");
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

    public void PointerInside(object sender, PointerEventArgs e)
    {
        
        if (e.target.name == "StartButton" || e.target.name == "QuitButton")
        {
            Debug.Log("Button was entered");
        }
    }

    public void PointerOutside(object sender, PointerEventArgs e)
    {
        
        if (e.target.name == "StartButton" || e.target.name == "QuitButton")
        {
            Debug.Log("Button was exited");
        }
    }
}