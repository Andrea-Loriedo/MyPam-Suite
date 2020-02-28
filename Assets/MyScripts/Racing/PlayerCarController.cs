using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCarController : MonoBehaviour {

	[HideInInspector] public Vector3 initialPosition { get; set; }
    [HideInInspector] public Quaternion initialRotation { get; set; }
	[HideInInspector] public Vector3 forward, right;
    [SerializeField] float speed = 2; 
    [SerializeField] GameObject playerCar;    
    float scaler = 3.5f;


    void Awake()
    {
        InitCamera();
    }

    void FixedUpdate()
    {
        playerCar.transform.position = Vector3.Lerp(playerCar.transform.position, transform.position, speed*Time.deltaTime);
        playerCar.transform.LookAt(transform);
        MoveCar(GetInput());
    }

    void MoveCar(Vector2 input)
	{
		// define diamond workspace
		Vector3 rightMovement = right * input.x; // define the "right" direction
		Vector3 upMovement = forward * input.y; // define the "forward" direction

        Vector3 direction = new Vector3(input.x, 0f, input.y); // Direction wrt perspective camera
        Vector3 isoDirection = rightMovement + upMovement;

        transform.position = Vector3.Lerp(transform.position, isoDirection * scaler, speed*Time.time);

        // Logger.Debug($"x = {input.x}, y = {input.y}");
	}

	public float Remap (float value, float from1, float to1, float from2, float to2) {
    	return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
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