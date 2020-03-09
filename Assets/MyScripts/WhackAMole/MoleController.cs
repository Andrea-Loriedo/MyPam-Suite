using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class MoleController : MonoBehaviour
{
    [HideInInspector] public MoleState state;
    float timeLeft = 30.0f;
    float resetTimer;
    Animator animator;

    void Awake()
    {
        animator = GetComponent<Animator>();
        resetTimer = timeLeft;
    }

    void Update () 
	{
        CheckState();
	}

    void CheckState()
    {
        switch(state)
        {
            case MoleState.UP:
                AnimateMole("Up");
                state = MoleState.ABOVE_GROUND;
                break;
            case MoleState.ABOVE_GROUND:
                timeLeft -= Time.deltaTime;
                if (timeLeft < 0) 
                {
                    state = MoleState.DOWN;
                    timeLeft = resetTimer;
                }
                break;
            case MoleState.DOWN:
                AnimateMole("Down");
                state = MoleState.BELOW_GROUND;
                Destroy(gameObject);
                break;
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