using UnityEngine;
using System.Collections;
using System;
using System.IO;
using System.Text.RegularExpressions;
using UnityEngineInternal;

public class HoleCollisionCheck : MonoBehaviour {

	static bool fall; // Static makes it accessible across different instances of the Hole object
	[HideInInspector] public bool throughHole
	{
		get { return fall; }
		
		set
		{
			fall = value;
			Logger.Debug("Through hole reset to " + fall);
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
