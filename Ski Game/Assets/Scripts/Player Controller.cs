using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float turnSpeed = 10f;
    [SerializeField] private float moveSpeed = 30f;
    
    [SerializeField] private bool isGrounded = true;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Vector3 pushbackForce;
    [SerializeField] private bool disabled = false;

    [SerializeField] private float disableTime = 1f;
    private float lastDisableTime;

    private InputAction move;
    private Rigidbody rb;
    
    void Awake()
    {
        move = InputSystem.actions.FindAction("Move");
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        isGrounded = Physics.Linecast(transform.position, transform.position - transform.up * 2, groundLayer);
        
        if (Time.timeSinceLevelLoad > lastDisableTime + disableTime)
            disabled = false;
        
        if (isGrounded && !disabled)
        {
            Vector2 moveVector = move.ReadValue<Vector2>();
        
            float slopeAngle = Mathf.Abs(transform.localEulerAngles.y - 180);
            float speedMultiplier = Mathf.Cos(Mathf.Deg2Rad * slopeAngle);
        
            rb.AddForce(transform.forward * moveSpeed * speedMultiplier * Time.fixedDeltaTime);
            transform.Rotate(0, moveVector.x * turnSpeed * Time.fixedDeltaTime, 0);
        }
        else
        {
            Vector2 moveVector = move.ReadValue<Vector2>();
            transform.Rotate(0, moveVector.x * turnSpeed * Time.fixedDeltaTime, 0);
        }
    }

    private void OnEnable()
    {
        Obstacle.OnPlayerHit += TakeDamage;
    }

    void TakeDamage()
    {
        disabled = true; //re-enable control after n seconds
        lastDisableTime = Time.timeSinceLevelLoad;
        rb.AddForce(pushbackForce, ForceMode.Impulse);
        
        Debug.Log("Got hit");
    }
}
