using UnityEngine;
using UnityEngine.Rendering.Universal;
using static GameManager;

public class Flag : MonoBehaviour
{
    private enum Direction
    {
        Left,
        Right
    };

    [SerializeField] private Direction flagDirection;
    private bool flagPassed = false;
    [SerializeField] private Material goodMaterial, badMaterial;
    public static event TimerEvent RacePenalty;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerController.playerPos != null && PlayerController.playerPos.position.z < transform.position.z &&
            !flagPassed)
        {
            flagPassed = true;
            Direction passingDirection = Direction.Right;
            if (PlayerController.playerPos.position.x < transform.position.x)
                passingDirection = Direction.Left;
            MeshRenderer mr = GetComponent<MeshRenderer>();

            if (passingDirection == flagDirection)
            {
                mr.material = goodMaterial;
            }
            else
            {
                mr.material = badMaterial;
                RacePenalty.Invoke();
            }
        }
    }
}