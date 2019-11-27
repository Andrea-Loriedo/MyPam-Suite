using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarbleController : MonoBehaviour {

	public Vector3 forward, right;
	Rigidbody rb;
	[SerializeField] float speed;
	[SerializeField] VirtualJoystick cursor;
	float mousePosX = 0;
	float mousePosY = 0;

	void Start () {
		rb = GetComponent<Rigidbody>();
		forward = Camera.main.transform.forward;
		forward.y = 0;
		forward = Vector3.Normalize(forward);
		right = Quaternion.Euler(new Vector3(0, 90, 0)) * forward;
 	}

 // Update is called once per frame
	void FixedUpdate () {

		mousePosX = Input.GetAxis("Mouse X") + mousePosX;
		mousePosY = Input.GetAxis("Mouse Y") + mousePosY;

		// Vector3 direction = new Vector3(cursor.Horizontal() * speed, 0f, cursor.Vertical() * speed);

		Vector3 rightMovement = right * cursor.Horizontal() * speed;
		Vector3 upMovement = forward * speed * cursor.Vertical() * speed;

		Vector3 heading = Vector3.Normalize(rightMovement + upMovement);

		transform.forward = heading;

		transform.Rotate(mousePosX, 0f, mousePosY);

		rb.AddForce(heading);
	}
}