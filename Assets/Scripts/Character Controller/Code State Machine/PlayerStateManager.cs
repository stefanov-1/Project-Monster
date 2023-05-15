using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerStateManager : MonoBehaviour
{
    #region States
    public Idle idleState = new Idle();
    public Running runningState = new Running();
    public Jumping jumpingState = new Jumping();
    public InAir inAirState = new InAir();
    public Climbing climbingState = new Climbing();
    #endregion

    #region Current and previous states
    [SerializeField] private State currentState;
    private State previousState;
    #endregion

    #region Properties

    public Rigidbody rb;
    public float speed = 5f;
    public float airSpeedMultiplier = 0.5f;
    public float jumpForce = 10f;

    public RaycastHit groundRayCastResults;
    [SerializeField] private float groundRayLength = 1.5f;
    public bool isGrounded = false; //{ get; private set; }
    [SerializeField] private LayerMask groundLayerMask;
    #endregion 

    private void Start()
    {
    }
    private void OnEnable()
    {

        currentState = idleState;
        currentState.EnterState(this);
        previousState = currentState;
    }
    // Update is called once per frame
    void Update()
    {
        currentState = currentState.UpdateState(this);
        if (previousState != currentState)
        {
            previousState.ExitState(this);
            currentState.EnterState(this);
            previousState = currentState;
        }

    }

    private void FixedUpdate()
    {
        //cast a ray downard from the bottom of the character collider to see if we are on the ground
        Ray groundRay = new Ray(transform.position, Vector3.down * groundRayLength);
        isGrounded = Physics.Raycast(groundRay, out groundRayCastResults, groundRayLength, ~groundLayerMask);
        Debug.DrawRay(groundRay.origin, groundRay.direction, Color.red);
    }

    private void OnGUI()
    {
        GUI.Label(new Rect(10, 10, 200, 30), "Current State: " + currentState.ToString());
    }

}
