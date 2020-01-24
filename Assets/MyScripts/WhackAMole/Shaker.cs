using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shaker : MonoBehaviour {

	Vector3 defaultPos;
	public float magnitude = 0.1f;

    void Start () 
	{
		defaultPos = transform.position;
	}

	public void Shake()
	{		
		StartCoroutine(ShakeRoutine());
	}

	IEnumerator ShakeRoutine()
	{
		for (int i = 0; i <= 360; i += 60) 
		{
			transform.position = new Vector3(    defaultPos.x, 
                                defaultPos.y + magnitude * Mathf.Sin (i * Mathf.Deg2Rad), 
                                defaultPos.z
            );
			yield return null;
		}
	}
}