using System;
using NUnit.Framework.Constraints;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public delegate void TimerEvent();

    private DateTime raceStart;
    private TimeSpan  raceTime;
    private TimeSpan penaltyTime;
    private TimeSpan bestTime;
    private bool racing = false;
    [SerializeField] private TMPro.TMP_Text timerText, bestTimeText;
    private string bestTimeKey = "bestTimeLVL1";

    private void Start()
    {
        int bestTimeInt = PlayerPrefs.GetInt(bestTimeKey, int.MaxValue);
        bestTime = new TimeSpan(bestTimeInt);
        bestTimeText.text = bestTime.ToString("mm\\:ss");
    }
    
    public void OnEnable()
    {
        StartGate.StartRace += StartRace;
        FinishGate.FinishRace += FinishRace;
        Flag.RacePenalty += AddRacePenalty;
    }
    
    public void OnDisable()
    {
        StartGate.StartRace -= StartRace;
        FinishGate.FinishRace -= FinishRace;
        Flag.RacePenalty -= AddRacePenalty;
    }

    void AddRacePenalty()
    {
        penaltyTime += new TimeSpan( 0, 0, 0, 3, 0);
    }
    
    void StartRace()
    {
        raceStart = System.DateTime.Now;
        racing = true;
        Debug.Log("Starting race");
    }

    void FinishRace()
    {
        Debug.Log("Finishing race");
        racing = false;
        GameData.Instance.AddLevelTime((float)raceTime.TotalMilliseconds / 1000f);
        if (raceTime < bestTime)
        {
            bestTimeText.text = "Best Time: " + raceTime.ToString("mm\\:ss");
            PlayerPrefs.SetInt(bestTimeKey, (int)raceTime.Ticks);
            PlayerPrefs.Save();
        }
    }

    void Update()
    {
        if(racing) 
            raceTime = DateTime.Now - raceStart + penaltyTime;
        timerText.text = "Time: " + raceTime.ToString("mm\\:ss");
    }
}