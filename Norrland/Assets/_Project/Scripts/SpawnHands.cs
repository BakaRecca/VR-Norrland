using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnHands : MonoBehaviour
{
    [SerializeField] private GameObject leftHand;
    [SerializeField] private GameObject rightHand;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            ShowHands();
        }
    }

    private void ShowHands()
    {
        leftHand.SetActive(true);
        rightHand.SetActive(true);
    }
}
