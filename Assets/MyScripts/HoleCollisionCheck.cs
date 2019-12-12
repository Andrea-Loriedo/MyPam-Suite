using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoleCollisionCheck : MonoBehaviour {

	[HideInInspector] public bool throughHole
	{
		get; set;
	}

	void OnTriggerExit(Collider other)
	{
		if(other.CompareTag("Marble"))
		throughHole = true;
	}
}
