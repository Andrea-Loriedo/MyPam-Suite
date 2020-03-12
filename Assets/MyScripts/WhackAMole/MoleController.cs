using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class MoleController : MonoBehaviour
{
    StartZoneController startZone;
    public MoleState state;
    float aboveGroundTime = 5.0f;
    Animator animator;

    void Awake()
    {
        animator = GetComponent<Animator>();
        startZone = GameObject.FindGameObjectsWithTag("StartZone")[0].GetComponent<StartZoneController>();
    }

    IEnumerator StayAboveGround(float timeLeft)
    {
        yield return new WaitForSeconds(timeLeft);
        SetState(MoleState.DOWN); // Send mole back under ground once timer has ran out
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
                startZone.SetState(StartZoneState.WAITING);
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
		if (state == MoleState.BELOW_GROUND) 
			return false; // Don't whack if the mole is hidden

        SetState(MoleState.DOWN); // Send mole back underground when whacked
        MyPamSessionManager.Instance.player.score++; // Increment player score
        // Logger.Debug($"Whacked {gameObject}!");
		return true;
	}
}