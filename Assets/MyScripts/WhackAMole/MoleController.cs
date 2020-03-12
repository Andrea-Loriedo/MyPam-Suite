using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class MoleController : MonoBehaviour
{
    StartZoneController startZone;
    public MoleState state;
    float aboveGroundTime = 30.0f;
    Animator animator;

    void Awake()
    {
        animator = GetComponent<Animator>();
        startZone = GameObject.FindGameObjectsWithTag("StartZone")[0].GetComponent<StartZoneController>();
    }

    IEnumerator StayAboveGround(float timeLeft)
    {
        yield return new WaitForSeconds(timeLeft);
        state = MoleState.DOWN;
    }

    public void SetState(MoleState newState)
    {
        state = newState;

        // modify colour based on state
        switch (state)
        {
            case MoleState.UP:
                AnimateMole("Up");
                SetState(MoleState.ABOVE_GROUND);
                startZone.SetState(StartZoneState.WAITING);
            break;
            case MoleState.ABOVE_GROUND:
                StartCoroutine(StayAboveGround(aboveGroundTime));
            break;
            case MoleState.DOWN:
                AnimateMole("Down");
                state = MoleState.BELOW_GROUND;
                startZone.SetState(StartZoneState.READY);
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

    public bool Whack()
	{
		// Don't whack if the mole is hidden
		if (state == MoleState.BELOW_GROUND) 
			return false;

        SetState(MoleState.DOWN);
        MyPamSessionManager.Instance.player.score++; // Increment player score
        // Logger.Debug($"Whacked {gameObject}!");
		return true;
	}
}