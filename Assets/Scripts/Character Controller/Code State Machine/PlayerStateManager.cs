using System;
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

    public float runAcceleration = 5f;
    public float runMaxSpeed = 5f;
    public float airAcceleration = 5f;
    public float airMaxSpeed = 5f;
    public float gravityForce = 5f;
    public float jumpForce = 5f;
    public float climbSpeed = 5f;
    public float drag = 0.1f;
    
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
        currentState.UpdateState(this);
    }

    private void FixedUpdate()
    {
        currentState.FixedUpdateState(this);
        
        //cast a ray downard from the bottom of the character collider to see if we are on the ground
        Ray groundRay = new Ray(transform.position, Vector3.down * groundRayLength);
        isGrounded = Physics.Raycast(groundRay, out groundRayCastResults, groundRayLength, ~groundLayerMask);
        Debug.DrawRay(groundRay.origin, groundRay.direction, Color.red);
    }

    public void ChangeState(State newState)
    {
        if (newState == currentState) return;
        currentState.ExitState(this);
        newState.EnterState(this);
        currentState = newState;
    }
    
    private void OnGUI()
    {
        GUI.Label(new Rect(10, 10, 200, 30), "Current State: " + currentState.ToString());
        GUI.Label(new Rect(10, 20, 200, 30), "Current Velocity: " + rb.velocity.ToString());
    }

    public void ApplyGravity()
    {
        //rb.AddForce(new Vector3(0, -gravityForce, 0), ForceMode.Acceleration);
    }

    public void ApplyDrag()
    {
        //rb.velocity = Vector3.Lerp(rb.velocity, Vector3.zero, drag * Time.deltaTime);
    }
    
    private void OnTriggerEnter(Collider other) // to implement it quickly I'm doing this here but there's probably a cleaner way
    {
        if (other.tag == "ClimbSurface")
        {
            ClimbSurface surface = other.transform.parent.GetComponent<ClimbSurface>();
            ControlValues.Instance.currentClimbStart = surface.startPoint.position;
            ControlValues.Instance.currentClimbEnd = surface.endPoint.position;
            ControlValues.Instance.currentClimbOrientation = surface.climbOrientation;
            
            ChangeState(climbingState);
            
        }
    }
}
