using System;
using UnityEngine;

public class PlayerCursorController : MonoBehaviour {

	[HideInInspector] public Vector3 initialPosition { get; set; }
    [HideInInspector] public Quaternion initialRotation { get; set; }
	[HideInInspector] public Vector3 forward, right;
    [SerializeField] float speed = 2; 
    [SerializeField] GameObject playerCar;    
    [SerializeField] Transform perspectiveCursor;
    [SerializeField] CameraHandler cameraHandler;

    float workspaceEdge = 3.5f;

    void Awake()
    {
        InitCamera();
    }

    void FixedUpdate()
    {
        // Make the car follow the cursor using linear interpolation
        playerCar.transform.position = Vector3.Lerp(playerCar.transform.position, transform.position, speed*Time.deltaTime);
        if(transform != playerCar.transform)
            playerCar.transform.LookAt(transform); // Make the car face the cursor
        MoveCursor(GetInput()); 
    }

    void MoveCursor(Vector2 input)
	{
		// define diamond workspace
		Vector3 rightMovement = right * input.x; // define the "right" direction
		Vector3 upMovement = forward * input.y; // define the "forward" direction

        Vector3 direction = new Vector3(input.x, 0f, input.y); // Direction wrt perspective camera
        Vector3 isoDirection = rightMovement + upMovement; // Direction wrt isometric camera
        Quaternion perspOffset = Quaternion.Euler(0, -45, 0);

        if (cameraHandler.activeCamera.Equals("isometric"))
        {
            // Move to destination in the correct isometric direction using linear interpolation
            transform.position = Vector3.Lerp(transform.position, isoDirection * workspaceEdge, speed * Time.time);
            perspectiveCursor.position = Vector3.Lerp(transform.position, direction * workspaceEdge, speed * Time.time);
        } else
        {
            // If not using isometric camera, move cursor wrt the standard Unity coordinate frame
            transform.position = Vector3.Lerp(transform.position, direction * workspaceEdge, speed * Time.time);
            perspectiveCursor.position = Vector3.Lerp(transform.position, (perspOffset*direction) * workspaceEdge, speed * Time.time);
        }
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