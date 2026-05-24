using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public delegate void TimerEvent();

    private DateTime raceStart;
    private TimeSpan raceTime;
    private TimeSpan penaltyTime;
    private bool racing = false;

    private float bestTime;

    [SerializeField] private LeaderboardUI leaderboardUI;
    [SerializeField] private TMPro.TMP_Text timerText, bestTimeText;
    [SerializeField] private GameObject newRecordText;

    private string bestTimeKey = "bestTimeLVL1";

    private void Start()
    {
        bestTime = PlayerPrefs.GetFloat(bestTimeKey, 999999f);

        if (newRecordText != null)
            newRecordText.SetActive(false);

        if (bestTime >= 999999f)
            bestTimeText.text = "Best Time: --:--";
        else
            bestTimeText.text = "Best Time: " + FormatTime(bestTime);
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
        penaltyTime += new TimeSpan(0, 0, 3);
    }

    void StartRace()
    {
        raceStart = DateTime.Now;
        penaltyTime = TimeSpan.Zero;
        raceTime = TimeSpan.Zero;
        racing = true;

        if (newRecordText != null)
            newRecordText.SetActive(false);
    }

    void FinishRace()
    {
        Debug.Log("Finishing race");
        racing = false;

        float finalTime = (float)raceTime.TotalSeconds;

        GameData.Instance.AddLevelTime(finalTime);
        leaderboardUI.UpdateLeaderboard();

        if (finalTime < bestTime)
        {
            bestTime = finalTime;
            PlayerPrefs.SetFloat(bestTimeKey, bestTime);
            PlayerPrefs.Save();

            bestTimeText.text = "Best Time: " + FormatTime(bestTime);

            newRecordText.SetActive(true);
        }
        else
        {
            newRecordText.SetActive(false);
        }
    }

    void Update()
    {
        if (racing)
            raceTime = DateTime.Now - raceStart + penaltyTime;

        timerText.text = "Time: " + raceTime.ToString("mm\\:ss");
    }

    private string FormatTime(float time)
    {
        int min = Mathf.FloorToInt(time / 60f);
        int sec = Mathf.FloorToInt(time % 60f);

        return $"{min:00}:{sec:00}";
    }
}