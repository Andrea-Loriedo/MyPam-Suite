using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarbleController : MonoBehaviour {

	Rigidbody rb;
	[SerializeField] float speed;
	float mousePosX = 0;
	float mousePosY = 0;

	void Start () {
		rb = GetComponent<Rigidbody>();
 	}

 // Update is called once per frame
	void FixedUpdate () {

		mousePosX = Input.GetAxis("Mouse X") + mousePosX;
		mousePosY = Input.GetAxis("Mouse Y") + mousePosY;

		var x = Input.GetAxis ("Mouse X") * speed;
		var y = Input.GetAxis ("Mouse Y") * speed;

		Vector3 movement = new Vector3(mousePosX, 0f, mousePosY);

		transform.Rotate(mousePosX, 0f, mousePosY);

		rb.AddForce(movement);
	}
}