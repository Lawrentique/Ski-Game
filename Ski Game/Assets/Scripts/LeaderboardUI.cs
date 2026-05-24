using UnityEngine;
using TMPro;

public class LeaderboardUI : MonoBehaviour
{
    [SerializeField] private TMP_Text[] leaderboardTexts;
    
    void OnEnable()
    {
        UpdateLeaderboard();
    }
    
    public void UpdateLeaderboard()
    {
        for (int n = 0; n < leaderboardTexts.Length; n++)
        {
            if (n < GameData.Instance.bestTimes.Count)
            {
                float time = GameData.Instance.bestTimes[n];

                int min = Mathf.FloorToInt(time / 60f);
                int sec = Mathf.FloorToInt(time % 60f);
                int milli = Mathf.FloorToInt((time * 1000f) % 1000f);
                
                leaderboardTexts[n].text = $"{min}:{sec}:{milli}";
            }
            else
            {
                leaderboardTexts[n].text = "-";
            }
        }
    }
}
