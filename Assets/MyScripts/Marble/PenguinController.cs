using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// [RequireComponent(typeof(Rigidbody))]
public class PenguinController : MonoBehaviour
{
	[HideInInspector] public Vector3 initialPosition { get; set; }
	[HideInInspector] public Vector3 forward, right;
	[HideInInspector] public Rigidbody rb;
	[HideInInspector] public float startSpeed; 

    ParticleSystem particles;
	public float movementSpeed = 1f; 

    public float jumpForce = 1000;
    public float timeBeforeNextJump = 1.2f;
    private float canJump = 0f;
    Animator anim;

	void Awake()
	{
		InitPhysics();
		particles = gameObject.GetComponentInChildren<ParticleSystem>();
		anim = GetComponent<Animator>();
		startSpeed = movementSpeed;
	}

	void Start () {
		InitCamera();
		initialPosition = transform.localPosition;
        canJump = Time.time + timeBeforeNextJump;
        anim.SetTrigger("jump");
 	}

	void FixedUpdate () {
 		MovePenguin(GetInput());
		#if !ENABLE_TESTING
		AddGravity();
		#endif
	}

	void MovePenguin(Vector2 input)
	{
		// define diamond workspace
		Vector3 rightMovement = right * input.x; // define the "right" direction
		Vector3 upMovement = forward * input.y; // define the "forward" direction
		Vector3 heading = Vector3.Normalize(rightMovement + upMovement);

        if (heading != Vector3.zero)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(heading), 0.15f);
            anim.SetInteger("Walk", 1);
        }

        else 
        {
            anim.SetInteger("Walk", 0);
        }

        transform.Translate(heading * movementSpeed * Time.deltaTime, Space.World);
	}

	void InitCamera()
	{
		forward = Camera.main.transform.forward; // vector aligned with the camera's forward vector
		forward.y = 0; // ensure the y value is always going to be set to 0
		forward = Vector3.Normalize(forward); // normalized vector for forward motion
		// rotate right vector 90 degrees around the x axis to obtain -45 deg shift from the world x axis
		right = Quaternion.Euler(new Vector3(0, 90, 0)) * forward; 
	}

	void InitPhysics()
	{
		rb = GetComponent<Rigidbody>();
		rb.useGravity = true;
		rb.mass = 3;
		rb.drag = 4;
	}
	
	void AddGravity()
	{
		// Have mass influence gravity
		rb.AddForce(Physics.gravity * (rb.mass * rb.mass)); 
	}

	Vector2 GetInput()
	{
		return MyPamSessionManager.Instance.player.PlayerInput.Input;
	}

	public void PlayParticles()
	{
		particles.Play();
	}

	public void Dive()
	{
		if (Time.time > canJump)
		{
			rb.AddForce(0, jumpForce, 0);
			canJump = Time.time + timeBeforeNextJump;
			anim.SetTrigger("jump");
		}
	}
}