using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using Incidents;
using UnityEngine;
using UnityEngine.Events;

public class GameManager
{
    private static GameManager instance;
    public UnityEvent<IncidentBase> openQNAPopup = new UnityEvent<IncidentBase>();
    public bool isPopUpOpen = false;

    public static GameManager Instance
    {
        get {
            if (instance == null)
            {
                instance = new GameManager();
            }
            return instance;
        }
    }      
}
