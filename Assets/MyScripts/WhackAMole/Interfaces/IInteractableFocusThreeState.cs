using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IInteractableFocusThreeState : MonoBehaviour, IInteractable
{
    [SerializeField] Material baseMaterial;
    [SerializeField] Material highlightMaterial;
    [SerializeField] StartZoneController startZone;
    [ColorUsage(true, true)] public Color waitingColor;
    [ColorUsage(true, true)] public Color preparingColor = Color.red;
    [ColorUsage(true, true)] public Color goColor = Color.green;

    public void Interact() 
    { 
        if (startZone != null && startZone.state == StartZoneState.WAITING)
            startZone.SetState(StartZoneState.PREPARING);
    }

    public void InteractionFocus(bool focused)
    {
        foreach (Transform child in transform)
        {
            Material mat = child.GetComponent<Renderer>().material = (focused) ? highlightMaterial : baseMaterial;

            if (focused)
            {
                switch(startZone.state)
                {
                    case StartZoneState.PREPARING:
                        mat.SetColor("_EmissionColor", preparingColor);
                        mat.EnableKeyword("_EMISSION"); 
                        Logger.Debug("Prepare colour");
                        break;
                    case StartZoneState.GO:
                        mat.SetColor("_EmissionColor", goColor);
                        mat.EnableKeyword("_EMISSION"); 
                        Logger.Debug("Go colour");
                        break;
                }
            }
            else {
                mat.SetColor("_EmissionColor", waitingColor);
                mat.EnableKeyword("_EMISSION"); 
            }
        }
    }
}