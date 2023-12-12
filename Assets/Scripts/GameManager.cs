using System.Collections;
using System.Collections.Generic;
using Incidents;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public UnityEvent<IncidentBase> openQNAPopup = new UnityEvent<IncidentBase>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }      
}
