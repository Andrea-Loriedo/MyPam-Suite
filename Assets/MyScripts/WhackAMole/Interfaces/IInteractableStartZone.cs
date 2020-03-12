using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IInteractableStartZone : MonoBehaviour, IInteractable
{
    [SerializeField] StartZoneController startZone;

    public void Interact() 
    { 
        if (startZone != null && startZone.state == StartZoneState.WAITING)
            startZone.SetState(StartZoneState.PREPARING);
    }

    public void InteractionFocus(bool focused) { }
}