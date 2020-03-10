using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableWhack : MonoBehaviour, IInteractable
{
    MoleController mole;

    void Start()
    {
        mole = gameObject.GetComponent<MoleController>();
    }

    public void Interact() 
    {
        if (mole != null)
            mole.Whack();
    }

    public void InteractionFocus(bool focused) {}
}