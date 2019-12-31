using UnityEngine;
using System.Collections;
using System;
using System.IO;
using System.Text.RegularExpressions;
using UnityEngineInternal;

public class HoleCollisionCheck : MonoBehaviour {

	static bool _fall; // Static makes it accessible across different instances of the Hole object

	[HideInInspector] public bool throughHole
	{
		get { return _fall; }
		
		set
		{
			_fall = value;
			Logger.Debug("Through hole reset to " + _fall);
		}
	}

	public void Construct(bool fall)
	{
		_fall = fall;
	}

	void Awake()
	{
		throughHole = false;
	}

	void OnTriggerExit(Collider other)
	{
		if(other.CompareTag("Marble"))
			throughHole = true;
	}
}
