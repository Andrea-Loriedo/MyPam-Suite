using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class MoleController : MonoBehaviour
{
    [HideInInspector] public MoleState state;
    [HideInInspector] public float waitTime = 3f;
    Animator animator;
	private float timer = 0;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void Update () 
	{
        CheckState();
	}

    void CheckState()
    {
        if(state == MoleState.UP)
        {
            AnimateMole("Up");
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
            AnimateMole("Down");
            state = MoleState.BELOW_GROUND;
            Destroy(gameObject);
        }
    }

    IEnumerator AnimateMole(string trigger)
    {
        animator.SetTrigger(trigger);
		yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
		animator.SetTrigger("Idle");
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