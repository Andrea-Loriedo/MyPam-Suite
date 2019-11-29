using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarbleController : MonoBehaviour {

	public Vector3 forward, right;
	Rigidbody rb;
	[SerializeField] float speed;
	[SerializeField] VirtualJoystick cursor;

	void Start () {
		rb = GetComponent<Rigidbody>();
		forward = Camera.main.transform.forward; // vector aligned with the camera's forward vector
		forward.y = 0; // ensure the y value is always going to be set to 0
		forward = Vector3.Normalize(forward); // normalized vector for motion
		right = Quaternion.Euler(new Vector3(0, 90, 0)) * forward; // rotate right vector 90 degrees around the x axis to obtain -45 deg shift from the world x axis
 	}

 // Update is called once per frame
	void FixedUpdate () {
		// MoveWithJoystick();
		MoveWithMyPam();
	}

	void MoveWithJoystick()
	{
		Vector3 direction = new Vector3(cursor.MouseHorizontal(), 0f, cursor.MouseVertical()); 

		Vector3 rightMovement = right * cursor.MouseHorizontal(); // define the "right" direction
		Vector3 upMovement = forward * cursor.MouseVertical(); // define the "forward" direction

		Vector3 heading = Vector3.Normalize(rightMovement + upMovement) * speed;

		if (direction != Vector3.zero) {
			transform.forward = heading; // transform the world forward vector into the orthographic forward vector
		}

		rb.AddForce(heading);
	}

	void MoveWithMyPam()
	{
		Vector3 direction = new Vector3(cursor.MyPamHorizontal(), 0f, cursor.MyPamVertical()); 

		// define diamond workspace
		Vector3 rightMovement = right * cursor.MyPamHorizontal(); // define the "right" direction
		Vector3 upMovement = forward * cursor.MyPamVertical(); // define the "forward" direction
		Vector3 heading = Vector3.Normalize(rightMovement + upMovement) * speed;

		if (direction != Vector3.zero) {
			transform.forward = heading; // transform the world forward vector into the orthographic forward vector
		}

		// Debug.Log(heading.ToString());

		// heading = cursor.myPam.BallInputPosition;

		rb.AddForce(heading);
	}
}