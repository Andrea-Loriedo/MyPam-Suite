using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoleCollisionCheck : MonoBehaviour {

	static bool fall;
	[HideInInspector] public bool throughHole
	{
		get { return fall; }
		
		set
		{
			fall = value;
			Debug.Log("Through hole reset to " + fall);
		}
	}

	void Awake()
	{
		throughHole = false;
	}

	void OnTriggerExit(Collider other)
	{
		if(other.CompareTag("Marble"))
		{
			throughHole = true;
		}
	}
}
