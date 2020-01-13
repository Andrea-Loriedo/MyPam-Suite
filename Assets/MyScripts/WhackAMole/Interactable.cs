using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// Gathers all IInteractable interfaces and notifies all components that implement them
// This way, it's not necessary to create multiple references to the interactable script
public class Interactable : MonoBehaviour
{
    private List<IInteractable> interfaces = new List<IInteractable>();

    private void Awake()
    {
        GetComponents<IInteractable>(interfaces); // Gather all interactable interfaces
    }

    public void Focus(bool state)
    {
        for (int i = interfaces.Count - 1; i >= 0; i--)
        {
            if (interfaces[i] == null)
            {
                interfaces.RemoveAt(i);
            }

            interfaces[i].InteractionFocus(state);
        }
    }

    public void Interact()
    {
        for (int i = interfaces.Count - 1; i >= 0; i--)
        {
            if (interfaces[i] == null)
            {
                interfaces.RemoveAt(i);
            }

            interfaces[i].Interact();
        }
    }
}