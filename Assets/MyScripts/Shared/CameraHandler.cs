using UnityEngine;

public class CameraHandler : MonoBehaviour {

	[SerializeField] KeyCode isoToggle;
	[SerializeField] KeyCode perspToggle;
	[SerializeField] KeyCode topDownToggle;
	[SerializeField] KeyCode sideToggle;
	[SerializeField] GameObject isoCam;
	[SerializeField] GameObject perspectiveCam;
	[SerializeField] GameObject sideCam;
	[SerializeField] GameObject topDownCam;
	[HideInInspector] public string activeCamera;

	// Use this for initialization
	void Awake()
    {
		DontDestroyOnLoad(gameObject);
		isoCam.SetActive(true);
		perspectiveCam.SetActive(false);
		topDownCam.SetActive(false);
		sideCam.SetActive(false);
		activeCamera = "isometric";
    }

	void Update () {
		if (Input.GetKeyDown(isoToggle))
		{
			activeCamera = "isometric";
			isoCam.SetActive(true);
			perspectiveCam.SetActive(false);
			topDownCam.SetActive(false);
			sideCam.SetActive(false);
		}
		else if (Input.GetKeyDown(perspToggle))
		{
			activeCamera = "isometric";
			isoCam.SetActive(false);
			perspectiveCam.SetActive(true);
			topDownCam.SetActive(false);
			sideCam.SetActive(false);
		}
		else if (Input.GetKeyDown(topDownToggle))
		{
			activeCamera = "perspective";
			isoCam.SetActive(false);
			perspectiveCam.SetActive(false);
			topDownCam.SetActive(true);
			sideCam.SetActive(false);
		}
		else if (Input.GetKeyDown(sideToggle))
		{
			activeCamera = "perspective";
			isoCam.SetActive(false);
			perspectiveCam.SetActive(false);
			topDownCam.SetActive(false);
			sideCam.SetActive(true);
		}
	}
}
