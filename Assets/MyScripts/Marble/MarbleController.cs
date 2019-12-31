using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarbleController : MonoBehaviour
{
	[HideInInspector] public Vector3 forward, right;
	[SerializeField] float speed; 
	[SerializeField] TilemapGenerator map;
	[SerializeField] VirtualJoystick input;
	[HideInInspector] public IPlayerInput PlayerInput { get; set; }
	[HideInInspector] public Vector3 initialPosition { get; set; }
	Rigidbody rb;

	void Awake()
	{
		rb = GetComponent<Rigidbody>();
	}

	void Start () {
		InitCamera();
		InitInput();
		initialPosition = transform.localPosition;
 	}

	void FixedUpdate () {
 		MoveMarble(PlayerInput.Input);
		AddGravity();
	}

	void MoveMarble(Vector2 input)
	{
		// define diamond workspace
		Vector3 rightMovement = right * input.x; // define the "right" direction
		Vector3 upMovement = forward * input.y; // define the "forward" direction
		Vector3 heading = Vector3.Normalize(rightMovement + upMovement) * speed;

		Vector3 direction = new Vector3(input.x, 0f, input.y); 

		if (direction != Vector3.zero) {
			transform.forward = heading; // transform the world forward vector into the orthographic forward vector
		}

		rb.AddForce(heading);
		rb.AddTorque(heading * speed * Time.deltaTime);
	}

	void AddGravity()
	{
		// Have mass influence gravity
		rb.AddForce(Physics.gravity * (rb.mass * rb.mass)); 
	}

	void InitCamera()
	{
		forward = Camera.main.transform.forward; // vector aligned with the camera's forward vector
		forward.y = 0; // ensure the y value is always going to be set to 0
		forward = Vector3.Normalize(forward); // normalized vector for forward motion
		// rotate right vector 90 degrees around the x axis to obtain -45 deg shift from the world x axis
		right = Quaternion.Euler(new Vector3(0, 90, 0)) * forward; 
	}

	void InitInput()
	{
		// Create a new player input
		if (PlayerInput == null)
			PlayerInput = new PlayerInput();
	}
}