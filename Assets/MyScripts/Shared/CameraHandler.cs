using System.Collections;
using System.Collections.Generic;
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

	// Use this for initialization
	void Awake()
    {
		DontDestroyOnLoad(gameObject);
		isoCam.SetActive(true);
		perspectiveCam.SetActive(false);
		topDownCam.SetActive(false);
		sideCam.SetActive(false);
    }

	void Update () {
		if (Input.GetKeyDown(isoToggle))
		{
			isoCam.SetActive(true);
			perspectiveCam.SetActive(false);
			topDownCam.SetActive(false);
			sideCam.SetActive(false);
		}
		else if (Input.GetKeyDown(perspToggle))
		{
			isoCam.SetActive(false);
			perspectiveCam.SetActive(true);
			topDownCam.SetActive(false);
			sideCam.SetActive(false);
		}
		else if (Input.GetKeyDown(topDownToggle))
		{
			isoCam.SetActive(false);
			perspectiveCam.SetActive(false);
			topDownCam.SetActive(true);
			sideCam.SetActive(false);
		}
		else if (Input.GetKeyDown(sideToggle))
		{
			isoCam.SetActive(false);
			perspectiveCam.SetActive(false);
			topDownCam.SetActive(false);
			sideCam.SetActive(true);
		}
	}
}
