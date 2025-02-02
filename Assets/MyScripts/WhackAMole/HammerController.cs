using System.Collections;
using UnityEngine;
using UXF;

[RequireComponent(typeof(Rigidbody))]
public class HammerController : MonoBehaviour
{
	[HideInInspector] public Vector3 initialPosition { get; set; }
    [HideInInspector] public Quaternion initialRotation { get; set; }
	[HideInInspector] public Vector3 forward, right;
	[HideInInspector] public Rigidbody rb;
	[SerializeField] float speed = 15f; 
	[SerializeField] Animator animator;
	[SerializeField] GameObject hammerImpact;
	[SerializeField] CameraHandler cameraHandler;
	[SerializeField] WorkspaceManager workspace;
	ParticleSystem[] particles;

	public Transform perspectiveCursor;

	public Session session;

    float radius = 0;

	void Awake()
	{
		InitPhysics();
		particles = hammerImpact.GetComponentsInChildren<ParticleSystem>();
	}

	void Start () 
    {
		InitCamera();
		initialPosition = transform.position;
        initialRotation = transform.rotation;
		DeActivate();
 	}

	void FixedUpdate () 
    {
 		MoveHammer(GetInput());
	}

	public void Activate()
	{
		gameObject.SetActive(true);
		radius = session.settings.GetFloat("workspace_radius_cm");
	}

	public void DeActivate()
	{
		gameObject.SetActive(false);
	}

	void MoveHammer(Vector2 input)
	{
		// define diamond workspace
		Vector3 rightMovement = right * input.x; // define the "right" direction
		Vector3 upMovement = forward * input.y; // define the "forward" direction
		Vector3 isoDirection = rightMovement + upMovement + new Vector3(0, 0.7f, 0); // Direction wrt isometric camera
		Vector3 direction = new Vector3(input.x, 0.7f, input.y); // Direction wrt perspective camera

		Quaternion perspOffset = Quaternion.Euler(0, -45, 0);

		if (cameraHandler.activeCamera.Equals("isometric"))
        {
            // Move to destination in the correct isometric direction using linear interpolation
            transform.position = Vector3.Lerp(transform.position, isoDirection * workspace.referenceScale, speed * Time.time);
            perspectiveCursor.position = Vector3.Lerp(transform.position, direction * workspace.referenceScale, speed * Time.time);
        } else
        {
            // If not using isometric camera, move cursor wrt the standard Unity coordinate frame
            transform.position = Vector3.Lerp(transform.position, direction * workspace.referenceScale, speed * Time.time);
            perspectiveCursor.position = Vector3.Lerp(transform.position, (perspOffset*direction) * workspace.referenceScale, speed * Time.time);
        }
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

	void PlayParticles()
    {
        foreach (ParticleSystem system in particles)
            system.Play();
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

	public Vector2 GetInput()
	{
		return MyPamSessionManager.Instance.player.PlayerInput.Input;
	}
}
