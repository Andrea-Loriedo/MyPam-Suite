using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IInteractableFocusHighlight : MonoBehaviour, IInteractable
{
    [SerializeField] Material baseMaterial;
    [SerializeField] Material highlightMaterial;
    [SerializeField] StartZoneController startZone;

    public void Interact() 
    { 
        if(startZone != null)
            startZone.SetState(StartZoneState.GO);
    }

    public void InteractionFocus(bool focused)
    {
        foreach (Transform child in transform)
            child.GetComponent<Renderer>().material = (focused) ? highlightMaterial : baseMaterial;
    }
}