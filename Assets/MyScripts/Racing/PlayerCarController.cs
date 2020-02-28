using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCarController : MonoBehaviour {

	[HideInInspector] public Vector3 initialPosition { get; set; }
    [HideInInspector] public Quaternion initialRotation { get; set; }
	[HideInInspector] public Vector3 forward, right;
    [SerializeField] float speed = 2; 
    [SerializeField] GameObject playerCar;    

    float radius = 3.5f;


    void Awake()
    {
        InitCamera();
    }

    void FixedUpdate()
    {
        playerCar.transform.position = Vector3.Lerp(playerCar.transform.position, transform.position, speed*Time.deltaTime);
        playerCar.transform.LookAt(transform);
        // FollowCursor();

        // playerCar.transform.position = new Vector3(transform.position.x, 0.126f, transform.position.z);
        MoveCar(GetInput());
    }

    void MoveCar(Vector2 input)
	{
		// define diamond workspace
		Vector3 rightMovement = right * input.x; // define the "right" direction
		Vector3 upMovement = forward * input.y; // define the "forward" direction
		Vector3 heading = Vector3.Normalize(rightMovement + upMovement);
        Vector3 direction = new Vector3(input.x, 0f, input.y); 

		// if (direction != Vector3.zero) {
		// 	// transform.LookAt(direction); // transform the world forward vector into the orthographic forward vector
        //     // transform.Rotate(90, 0, 0);
		// }

        transform.position = Vector3.Lerp(transform.position, rightMovement * radius + upMovement * radius, speed*Time.time);
        // transform.position = new Vector3(transform.position.x, 0.011f, transform.position.z);


        Logger.Debug($"x = {input.x}, y = {input.y}");
	}

    void FollowCursor()
    {
        Vector3 diff = transform.position - playerCar.transform.position;
        float rotY = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        playerCar.transform.rotation = Quaternion.Euler(0.0f, rotY, 0.0f);
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