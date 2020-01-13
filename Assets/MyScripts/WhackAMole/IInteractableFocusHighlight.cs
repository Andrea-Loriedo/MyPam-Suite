using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IInteractableFocusHighlight : MonoBehaviour, IInteractable
{
    [SerializeField] Material baseMaterial;
    [SerializeField] Material highlightMaterial;

    public void Interact() {}

    public void InteractionFocus(bool focused)
    {
        foreach (Transform child in transform)
            child.GetComponent<Renderer>().material = (focused) ? highlightMaterial : baseMaterial;
    }
}