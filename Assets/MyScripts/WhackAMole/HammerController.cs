using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class HammerController : MonoBehaviour
{
	[HideInInspector] public Vector3 initialPosition { get; set; }
    [HideInInspector] public Quaternion initialRotation { get; set; }
	[HideInInspector] public Vector3 forward, right;
	[HideInInspector] public Rigidbody rb;
	[SerializeField] Interactor interactor;
	[SerializeField] float speed = 15f; 
	[SerializeField] Animator animator;

    public float radius = 3f;

	void Awake()
	{
		InitPhysics();
	}

	void Start () 
    {
		InitCamera();
		initialPosition = transform.position;
        initialRotation = transform.rotation;
		// anim = gameObject.GetComponent<Animation>();
 	}

	void FixedUpdate () 
    {
 		MoveHammer(GetInput());
		if (Input.GetKeyDown("space"))
			StartCoroutine(PlayWhackAnimation());
	}

	void MoveHammer(Vector2 input)
	{
		// define diamond workspace
		Vector3 rightMovement = right * input.x; // define the "right" direction
		Vector3 upMovement = forward * input.y; // define the "forward" direction
		Vector3 heading = Vector3.Normalize(rightMovement + upMovement) * speed;
        Vector2 pos2D = new Vector2(transform.position.x, transform.position.z);

        Vector3 direction = new Vector3(input.x, 0f, input.y); 

		if (direction != Vector3.zero) {
			transform.LookAt(heading); // transform the world forward vector into the orthographic forward vector
            transform.Rotate(65, 0, 0);
		}

        rb.AddForce(heading * speed);

        if (input == Vector2.zero)
        {
            transform.position = initialPosition;
            transform.rotation = initialRotation;
        }
    
        else if (pos2D.magnitude >= radius)
            transform.position = ConstrainToCircle();
	}

    Vector3 ConstrainToCircle()
    {
		Vector3 circleCentre = new Vector3(0f, 3f, 0f);
        Vector3 offset = transform.position - circleCentre;
        offset.Normalize();
        offset = offset * radius;
        return circleCentre + offset;
    }

	IEnumerator PlayWhackAnimation()
	{
		animator.SetTrigger("Whack");
		yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
		animator.SetTrigger("Idle");
	}

	public void Animate()
	{
		StartCoroutine(PlayWhackAnimation());
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

// https://free3d.com/3d-model/hand-v2--144793.html hand asset