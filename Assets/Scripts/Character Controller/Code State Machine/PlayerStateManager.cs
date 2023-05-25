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
    public Sliding slideState = new Sliding();
    #endregion

    #region Current and previous states
    [SerializeField] private State currentState;
    private State previousState;
    #endregion

    #region Properties

    public Rigidbody rb;

    public float horizontalDrag = 5f;
    public float runAcceleration = 5f;
    public float runMaxSpeed = 5f;
    public float airAcceleration = 5f;
    public float airMaxSpeed = 5f;
    public float jumpForce = 5f;
    public float climbSpeed = 5f;
    public float slideSpeed = 10f;
    public float climbExitJumpForce = 3f;
    
    public RaycastHit groundRayCastResults;
    [SerializeField] private float groundRayLength = 1.5f;
    public bool isGrounded = false; //{ get; private set; }
    [SerializeField] private LayerMask groundLayerMask;
    #endregion 

    private void Start()
    {
        //add current position as checkpoint at the start for testing
        ControlValues.Instance.lastCheckpoint = transform.position;
        ControlValues.Instance.checkpointBacklog.Add(transform.position);
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
        //hardcoded limitations because unity is stupid
        transform.position = new Vector3(transform.position.x, transform.position.y, 0);
        Vector3 eulerRotation = transform.rotation.eulerAngles;
        transform.rotation = Quaternion.Euler(0, eulerRotation.y, 0);
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

    public void ApplyDrag()
    {
        rb.velocity = new Vector3(Mathf.Lerp(rb.velocity.x, 0, horizontalDrag * Time.deltaTime), rb.velocity.y, 0);
    }

    public void Respawn()
    {
        rb.velocity = Vector3.zero;
        rb.position = ControlValues.Instance.lastCheckpoint;
        ChangeState(idleState);
    }
    
    private void OnGUI()
    {
        GUI.Label(new Rect(10, 10, 200, 30), "Current State: " + currentState.ToString());
        GUI.Label(new Rect(10, 20, 200, 30), "Current Velocity: " + rb.velocity.ToString());
    }

    private void OnTriggerEnter(Collider other) // to implement it quickly I'm doing this here but there's probably a cleaner way
    {
        switch (other.tag)
        {
            case "ClimbSurface":
                ClimbSurface climbSurface = other.transform.parent.GetComponent<ClimbSurface>();
                ControlValues.Instance.currentClimbStart = climbSurface.startPoint.position;
                ControlValues.Instance.currentClimbEnd = climbSurface.endPoint.position;
                ControlValues.Instance.currentClimbOrientation = climbSurface.climbOrientation;
            
                ChangeState(climbingState);
                break;
            
            case "SlideSurface":
                SlideSurface slideSurface = other.transform.parent.GetComponent<SlideSurface>();
                ControlValues.Instance.currentSlideStart = slideSurface.startPoint.position;
                ControlValues.Instance.currentSlideEnd = slideSurface.endPoint.position;
                ControlValues.Instance.currentSlideDirection = (slideSurface.endPoint.position - slideSurface.startPoint.position).normalized;
            
                ChangeState(slideState);
                break;
            
            case "DeathSurface":
                Respawn();
                break;
            
            case "Checkpoint":
                if (!ControlValues.Instance.checkpointBacklog.Contains(other.transform.position))
                {
                    ControlValues.Instance.lastCheckpoint = other.transform.position;
                    ControlValues.Instance.checkpointBacklog.Add(other.transform.position);
                }
                break;
            
            default:
                break;
        }
        
    }
}
