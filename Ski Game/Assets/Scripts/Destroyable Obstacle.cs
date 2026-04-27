using UnityEngine;

public class DestroyableObstacle : Obstacle
{
    internal virtual void OnCollisionEnter(Collision collision)
    {
        base.OnCollision(collision);
        gameObject.SetActive(false);
    }
    
}
