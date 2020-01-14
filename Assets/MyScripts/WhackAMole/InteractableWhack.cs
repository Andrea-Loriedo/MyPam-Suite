using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableWhack : MonoBehaviour, IInteractable
{
    MoleController mole;
    public void Interact() 
    {
        mole = gameObject.GetComponent<MoleController>();
        if (mole != null)
            mole.Whack();
    }

    public void InteractionFocus(bool focused) {}
}