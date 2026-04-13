using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float turnSpeed = 10f;
    [SerializeField] private float moveSpeed = 30f;

    private InputAction move;
    private Rigidbody rb;
    
    void Awake()
    {
        move = InputSystem.actions.FindAction("Move");
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        
        Vector2 moveVector = move.ReadValue<Vector2>();
        
        float slopeAngle = Mathf.Abs(transform.localEulerAngles.y - 180);
        float speedMultiplier = Mathf.Cos(Mathf.Deg2Rad * slopeAngle);
        
        rb.AddForce(transform.forward * moveSpeed * speedMultiplier * Time.fixedDeltaTime);
        transform.Rotate(0, moveVector.x * turnSpeed * Time.fixedDeltaTime, 0);

        
        
    }
}
