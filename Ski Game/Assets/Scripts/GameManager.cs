using System;
using NUnit.Framework.Constraints;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public delegate void TimerEvent();

    private DateTime raceStart;
    private TimeSpan raceTime;
    private bool racing = false;
    
    public void OnEnable()
    {
        StartGate.StartRace += StartRace;
        FinishGate.FinishRace += FinishRace;
    }
    
    void StartRace()
    {
        raceStart = System.DateTime.Now;
        Debug.Log("Starting race");
    }

    void FinishRace()
    {
        Debug.Log("Finishing race");
    }

    void Update()
    {
        raceTime = DateTime.Now - raceStart;
        Debug.Log("Race time: " + raceTime.ToString("mm:ss:ff"));
    }
}