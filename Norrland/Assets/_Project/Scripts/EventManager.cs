using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public delegate void SnowmobileAction();
    public static event SnowmobileAction GetOnScooter;

    void Update()
    {
        if (true)
        {


            if (GetOnScooter != null)
            {
                GetOnScooter();
            }
        }
    }
}
