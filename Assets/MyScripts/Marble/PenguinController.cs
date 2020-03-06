using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// [RequireComponent(typeof(Rigidbody))]
public class PenguinController : MonoBehaviour
{
	[HideInInspector] public Vector3 initialPosition { get; set; }
	[HideInInspector] public Vector3 forward, right;
	[HideInInspector] public Rigidbody rb;
    ParticleSystem particles;
	public float movementSpeed = 3f; 

    public float jumpForce = 1000;
    public float timeBeforeNextJump = 1.2f;
    private float canJump = 0f;
    Animator anim;
    Collider collider;

	bool lockVert = false;
	bool lockHoriz = false;

	void Awake()
	{
		InitPhysics();
		particles = gameObject.GetComponentInChildren<ParticleSystem>();
		anim = GetComponent<Animator>();
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

	void LockDirections(Vector2 input)
	{
		if (input.x != 0 && input.y != 0)
			lockHoriz = lockVert = false;
		else if (input.x != 0 && input.y == 0)
			lockHoriz = true;
		else if (input.y != 0 && input.x == 0)
			lockVert = true;
	}

	void MovePenguin(Vector2 input)
	{
		LockDirections(input);
		// define diamond workspace
		Vector3 rightMovement = lockHoriz ? Vector3.zero : (right * input.x); // define the "right" direction
		Vector3 upMovement = lockVert ? Vector3.zero : (forward * input.y); // define the "forward" direction
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
		rb.AddForce(0, jumpForce, 0);
		canJump = Time.time + timeBeforeNextJump;
		anim.SetTrigger("jump");
	}
}