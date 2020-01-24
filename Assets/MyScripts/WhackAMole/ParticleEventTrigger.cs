using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleEventTrigger : MonoBehaviour
{
    [SerializeField] GameObject hammerImpact;
    [SerializeField] Shaker shaker;
	ParticleSystem[] particles;

    void Awake()
	{
		particles = hammerImpact.GetComponentsInChildren<ParticleSystem>();
	}

    public void PlayParticles()
    {
        hammerImpact.transform.position = new Vector3(		gameObject.transform.position.x, 
															hammerImpact.transform.position.y, 
															gameObject.transform.position.z
		);
        foreach (ParticleSystem system in particles)
            system.Play();
        shaker.Shake();
        MyPamSessionManager.Instance.player.score++;
    }
}