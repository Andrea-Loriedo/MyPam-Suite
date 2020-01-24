using System.Collections.Generic;
using UnityEngine;

public class MoleController : MonoBehaviour
{
    [HideInInspector] public MoleState state;
    [HideInInspector] public float spawnSpeed = 0.001f;
    [HideInInspector] public float waitTime = 3f;
    Vector3 aboveGround = new Vector3(0f, 0.75f, 0f);
    Vector3 belowGround = new Vector3(0f, -0.15f, 0f);
	private float timer = 0;

    void Update () 
	{
        CheckState();
        // Logger.Debug($"State: {state}");
	}

    void CheckState()
    {
        if(state == MoleState.UP)
        {
            Move(aboveGround);
            state = MoleState.ABOVE_GROUND;
        }

        if(state == MoleState.ABOVE_GROUND)
        {
            timer += Time.deltaTime;
            
            if (timer > waitTime) 
                state = MoleState.DOWN;
        }

        if(state == MoleState.DOWN)
        {
            Move(belowGround);
            state = MoleState.BELOW_GROUND;
            Destroy(gameObject);
        }
    }

    void Move(Vector3 target)
    {
        transform.position = Vector3.MoveTowards(   transform.position, 
                                                    target, 
                                                    spawnSpeed * Time.deltaTime
        );

        if (Vector3.Distance(transform.position, target) < 0.001f)
            transform.position = target;
    }

    public void Up()
	{
		if (state == MoleState.BELOW_GROUND) 
			state = MoleState.UP;
	}

    public bool Whack()
	{
		// Don't whack if the mole is hidden
		if (state == MoleState.BELOW_GROUND) 
			return false;

        Logger.Debug($"Whacked {gameObject}!");
		// Send back underground
        state = MoleState.DOWN;
        // Destroy(gameObject);
		return true;
	}

}