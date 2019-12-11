using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarbleController : MonoBehaviour {
	[HideInInspector] public Vector3 forward, right;
	[SerializeField] float speed;
	[SerializeField] VirtualJoystick movement;
	Rigidbody rb;
	GameObject hole;
	HoleCollisionCheck fallDetection;
	Vector3 initialPosition
	{
		get; set;
	}

	void Start () {
		rb = GetComponent<Rigidbody>();
		hole = GameObject.FindWithTag("Hole");
		fallDetection = hole.GetComponent<HoleCollisionCheck>();

		forward = Camera.main.transform.forward; // vector aligned with the camera's forward vector
		forward.y = 0; // ensure the y value is always going to be set to 0
		forward = Vector3.Normalize(forward); // normalized vector for forward motion
		right = Quaternion.Euler(new Vector3(0, 90, 0)) * forward; // rotate right vector 90 degrees around the x axis to obtain -45 deg shift from the world x axis
		initialPosition = transform.localPosition;
 	}

	void FixedUpdate () {
		MoveMarble();
		AddGravity();
		CheckFall();
	}

	void MoveMarble()
	{
		// define diamond workspace
		Vector3 rightMovement = right * movement.Horizontal(); // define the "right" direction
		Vector3 upMovement = forward * movement.Vertical(); // define the "forward" direction
		Vector3 heading = Vector3.Normalize(rightMovement + upMovement) * speed;

		Vector3 direction = new Vector3(movement.Horizontal(), 0f, movement.Vertical()); 

		if (direction != Vector3.zero) {
			transform.forward = heading; // transform the world forward vector into the orthographic forward vector
		}

		rb.AddForce(heading);
		rb.AddTorque(heading * speed * Time.deltaTime);
	}

	void AddGravity()
	{
		rb.AddForce(Physics.gravity * (rb.mass * rb.mass)); // Have mass influence gravity
	}

	void CheckFall()
	{
		if(fallDetection.throughHole)
		{
			transform.position = initialPosition;
		}
		fallDetection.throughHole = false;
	}
}