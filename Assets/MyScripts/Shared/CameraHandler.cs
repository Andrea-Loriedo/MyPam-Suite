using UnityEngine;

public class CameraHandler : MonoBehaviour {

	[SerializeField] KeyCode isoToggle;
	[SerializeField] KeyCode topDownToggle;

	[SerializeField] GameObject isoCam;
	[SerializeField] GameObject topDownCam;

	[HideInInspector] public string activeCamera;

	// Use this for initialization
	void Awake()
    {
		DontDestroyOnLoad(gameObject);
		isoCam.SetActive(true);
		topDownCam.SetActive(false);
		activeCamera = "isometric";
    }

	void Update () {
		if (Input.GetKeyDown(isoToggle))
		{
			activeCamera = "isometric";
			isoCam.SetActive(true);
			topDownCam.SetActive(false);
		}
		else if (Input.GetKeyDown(topDownToggle))
		{
			activeCamera = "perspective";
			isoCam.SetActive(false);
			topDownCam.SetActive(true);
		}
	}
}
