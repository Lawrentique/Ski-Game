using UnityEngine;
using static GameManager;

public class StartGate : MonoBehaviour
{
    public static event TimerEvent StartRace;
    
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            StartRace.Invoke();
        }
    }
}
