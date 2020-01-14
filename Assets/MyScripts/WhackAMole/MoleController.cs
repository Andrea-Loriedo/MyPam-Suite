using System.Collections.Generic;
using UnityEngine;

public class MoleController : MonoBehaviour
{
    [HideInInspector] public float spawnSpeed = 0.1f;
    [HideInInspector] public float waitTime = 1.0f;
    Vector3 aboveGround = new Vector3(0f, 0.75f, 0f);
    Vector3 belowGround = new Vector3(0f, -0.1f, 0f);
	private float tmpTime = 0;
	[HideInInspector] public MoleState state;

    void Start()
    {
        state = MoleState.BELOW_GROUND;
    }

    void Update () 
	{
        CheckState();
	}

    void CheckState()
    {
        switch (state)
        {
            case MoleState.UP:
                Move(aboveGround);
                state = MoleState.ABOVE_GROUND;
            break;

            case MoleState.ABOVE_GROUND:
                tmpTime += Time.deltaTime;
                
                if (tmpTime > waitTime) 
                    state = MoleState.DOWN;
            break;

            case MoleState.DOWN:
                Move(belowGround);
                state = MoleState.BELOW_GROUND;
            break;
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
		{
            gameObject.setActive(true);
			state = MoleState.UP;
		}
	}

    public bool Whack()
	{
		// Don't whack if the mole is hidden
		if (state == MoleState.BELOW_GROUND) 
		{
			return false;
		}

		// Send back underground
		transform.position = belowGround;
		state = MoleState.BELOW_GROUND;

		return true;
	}


}