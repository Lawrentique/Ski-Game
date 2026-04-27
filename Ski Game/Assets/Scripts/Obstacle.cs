using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public delegate void playerhitAction();
    public static event playerhitAction OnPlayerHit;
    
    private void OnCollisionEnter(Collision collision)
    {
        OnCollision(collision);
    }

    internal virtual void OnCollision(Collision collision)
    {
        if (collision.collider.tag == "Player")
        {
            Debug.Log("Collided");
        }
        OnPlayerHit.Invoke();
    }
}
