using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class HammerController : MonoBehaviour
{
	[HideInInspector] public Vector3 initialPosition { get; set; }
	[HideInInspector] public Vector3 forward, right;
	[HideInInspector] public Rigidbody rb;
	[SerializeField] float speed = 6f; 
    float radius = 3f;

	void Awake()
	{
		InitPhysics();
	}

	void Start () 
    {
		InitCamera();
		initialPosition = transform.position;
        transform.position = initialPosition;
 	}

	void FixedUpdate () 
    {
 		MoveHammer(GetInput());
	}

	void MoveHammer(Vector2 input)
	{
		// define diamond workspace
		Vector3 rightMovement = right * input.x; // define the "right" direction
		Vector3 upMovement = forward * input.y; // define the "forward" direction
		Vector3 heading = Vector3.Normalize(rightMovement + upMovement) * speed;
		Vector3 circleCentre = new Vector3(0f, 3f, 0f);
        rb.AddForce(heading * speed);

        Vector3 offset = transform.position - initialPosition;
        offset.Normalize();
        offset = offset * radius;
        transform.position = offset;

        // if (heading == Vector3.zero)
        //     transform.position = initialPosition;
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
		rb.useGravity = false;
		rb.mass = 3;
		rb.drag = 4;
	}

	Vector2 GetInput()
	{
		return MyPamSessionManager.Instance.player.PlayerInput.Input;
	}
}