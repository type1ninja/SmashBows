﻿using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]

public class RigidbodyFPS : MonoBehaviour
{
    #region Variables (private)

    private bool grounded = false;
    private Vector3 groundVelocity;
    private CapsuleCollider capsule;
    private Rigidbody rigbod;

    private GameObject pausePanel;

    // Inputs Cache
    private bool jumpFlag = false;

    #endregion

    #region Properties (public)

    // Speeds
    public float walkSpeed = 8.0f;
    public float bowDrawnSpeed = 4.0f;
    public float walkBackwardSpeed = 4.0f;
    public float runSpeed = 14.0f;
    public float runBackwardSpeed = 6.0f;
    public float sidestepSpeed = 8.0f;
    public float runSidestepSpeed = 12.0f;
    public float maxVelocityChange = 10.0f;

    // Air
    public float inAirControl = 0.1f;
    public float jumpHeight = 2.0f;

    // Can Flags
    public bool canRunSidestep = true;
    public bool canJump = true;
    public bool canRun = true;

    #endregion

    #region Unity event functions

    /// <summary>
    /// Use for initialization
    /// </summary>
    void Awake()
    {
        capsule = GetComponent<CapsuleCollider>();
        rigbod = GetComponent<Rigidbody>();
        rigbod.freezeRotation = true;
        rigbod.useGravity = true;

        pausePanel = GameObject.Find("PauseCanvas").transform.Find("PausePanel").gameObject;
    }

    /// <summary>
    /// Use this for initialization
    /// </summary>
    void Start()
    {
        
    }

    /// <summary>
    /// Update is called once per frame
    /// </summary>
    void Update()
    {
        // Cache the input
        if (Input.GetButtonDown("Jump") && !pausePanel.activeSelf)
            jumpFlag = true;
    }

    /// <summary>
    /// Update for physics
    /// </summary>
    void FixedUpdate()
    {
        var inputVector = Vector3.zero;
        // Cache de input
        if (!pausePanel.activeSelf)
        {
            inputVector = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        } 

        // On the ground
        if (grounded)
        {
            // Apply a force that attempts to reach our target velocity
            var velocityChange = CalculateVelocityChange(inputVector);
            rigbod.AddForce(velocityChange, ForceMode.VelocityChange);

            // Jump
            if (canJump && jumpFlag)
            {
                jumpFlag = false;
                rigbod.velocity = new Vector3(rigbod.velocity.x, rigbod.velocity.y + CalculateJumpVerticalSpeed(), rigbod.velocity.z);
            }

            // By setting the grounded to false in every FixedUpdate we avoid
            // checking if the character is not grounded on OnCollisionExit()
            grounded = false;
        }
        // In mid-air
        else
        {
            // Uses the input vector to affect the mid air direction
            var velocityChange = transform.TransformDirection(inputVector) * inAirControl;
            rigbod.AddForce(velocityChange, ForceMode.VelocityChange);
        }
    }

    // Unparent if we are no longer standing on our parent
    void OnCollisionExit(Collision collision)
    {
        if (collision.transform == transform.parent)
            transform.parent = null;
    }

    // If there are collisions check if the character is grounded
    void OnCollisionStay(Collision col)
    {
        TrackGrounded(col);
    }

    void OnCollisionEnter(Collision col)
    {
        TrackGrounded(col);
    }

    #endregion

    #region Methods

    // From the user input calculate using the set up speeds the velocity change
    private Vector3 CalculateVelocityChange(Vector3 inputVector)
    {

        var relativeVelocity = transform.TransformDirection(inputVector);
        /*
        // Calculate how fast we should be moving
        if (inputVector.z > 0)
        {
            relativeVelocity.z *= (canRun && Input.GetButton("Sprint")) ? runSpeed : walkSpeed;
        }
        else
        {
            relativeVelocity.z *= (canRun && Input.GetButton("Sprint")) ? runBackwardSpeed : walkBackwardSpeed;
        }
        relativeVelocity.x *= (canRunSidestep && Input.GetButton("Sprint")) ? runSidestepSpeed : sidestepSpeed;
        */

        if (Input.GetButton("PrimaryFire")) {
            relativeVelocity.z *= bowDrawnSpeed;
            relativeVelocity.x *= bowDrawnSpeed;
        } else
        {
            relativeVelocity.z *= walkSpeed;
            relativeVelocity.x *= sidestepSpeed;
        }

        // Calcualte the delta velocity
        var currRelativeVelocity = rigbod.velocity - groundVelocity;
        var velocityChange = relativeVelocity - currRelativeVelocity;
        velocityChange.x = Mathf.Clamp(velocityChange.x, -maxVelocityChange, maxVelocityChange);
        velocityChange.z = Mathf.Clamp(velocityChange.z, -maxVelocityChange, maxVelocityChange);
        velocityChange.y = 0;

        return velocityChange;
    }

    // From the jump height and gravity we deduce the upwards speed for the character to reach at the apex.
    private float CalculateJumpVerticalSpeed()
    {
        return Mathf.Sqrt(2f * jumpHeight * Mathf.Abs(Physics.gravity.y));
    }

    // Check if the base of the capsule is colliding to track if it's grounded
    private void TrackGrounded(Collision collision)
    {
        var maxHeight = capsule.bounds.min.y + capsule.radius * .9f;
        foreach (var contact in collision.contacts)
        {
            if (contact.point.y < maxHeight)
            {
                if (isKinematic(collision))
                {
                    // Get the ground velocity and we parent to it
                    groundVelocity = collision.transform.GetComponent<Rigidbody>().velocity;
                    transform.parent = collision.transform;
                }
                else if (isStatic(collision))
                {
                    // Just parent to it since it's static
                    transform.parent = collision.transform;
                }
                else
                {
                    // We are standing over a dinamic object,
                    // set the groundVelocity to Zero to avoid jiggers and extreme accelerations
                    groundVelocity = Vector3.zero;
                }

                // Esta en el suelo
                grounded = true;
            }

            break;
        }
    }

    private bool isKinematic(Collision collision)
    {
        return isKinematic(GetComponent<Collider>().transform);
    }

    private bool isKinematic(Transform transform)
    {
        Rigidbody otherRigbod = transform.GetComponent<Rigidbody>();
        return otherRigbod && otherRigbod.isKinematic;
    }

    private bool isStatic(Collision collision)
    {
        return isStatic(collision.transform);
    }

    private bool isStatic(Transform transform)
    {
        return transform.gameObject.isStatic;
    }

    #endregion
}