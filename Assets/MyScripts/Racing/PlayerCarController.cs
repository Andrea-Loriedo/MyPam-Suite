using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCarController : MonoBehaviour {

	[HideInInspector] public Vector3 initialPosition { get; set; }
    [HideInInspector] public Quaternion initialRotation { get; set; }
	[HideInInspector] public Vector3 forward, right;
    [HideInInspector] public Rigidbody rb;
    [SerializeField] float speed = 15f; 
    Transform playerCar;    


    void Awake()
    {
        InitCamera();
        rb = GetComponent<Rigidbody>();
        playerCar = GetComponentInChildren<Transform>();
    }

    void Update()
    {
        // playerCar.position = Vector3.Lerp(playerCar.position, transform.position, Time.time);
        MoveCar(GetInput());
    }

    void MoveCar(Vector2 input)
	{
		// define diamond workspace
		Vector3 rightMovement = right * input.x; // define the "right" direction
		Vector3 upMovement = forward * input.y; // define the "forward" direction
		Vector3 heading = Vector3.Normalize(rightMovement + upMovement) * speed;

        Vector3 direction = new Vector3(input.x, 0f, input.y); 

		if (direction != Vector3.zero) {
			transform.LookAt(heading); // transform the world forward vector into the orthographic forward vector
            transform.Rotate(90, 0, 0);
		}

        transform.position = Vector3.Lerp(transform.position, direction, Time.time);
	}

    Vector2 GetInput()
	{
		return MyPamSessionManager.Instance.player.PlayerInput.Input;
	}

    void InitCamera()
	{
		forward = Camera.main.transform.forward; // vector aligned with the camera's forward vector
		forward.y = 0; // ensure the y value is always going to be set to 0
		forward = Vector3.Normalize(forward); // normalized vector for forward motion
		// rotate right vector 90 degrees around the x axis to obtain -45 deg shift from the world x axis
		right = Quaternion.Euler(new Vector3(0, 90, 0)) * forward; 
	}
}