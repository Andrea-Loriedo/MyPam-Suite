using System.Collections.Generic;
using UnityEngine;

public class Interactor : MonoBehaviour
{
    // Equivalent to public delegate void OnCanInteract(Interactable target)
    public Interactable activeInteractable { get; private set; }

    void FixedUpdate()
    {
        // In case we move the object, we want to check if there have been any interaction changes.
        if (transform.hasChanged)
        {
            CheckIfInRange();
        }
    }

    public bool TryInteract()
    {
        if (activeInteractable != null)
        {
            activeInteractable.Interact();
            return true;
        }
        else
        {
            return false;
        }
    }

    private void CheckIfInRange()
    {
        Vector3 down = transform.TransformDirection(Vector3.left);
        GameObject target;
        RaycastHit hit;

        if (Physics.Raycast(transform.position, down, out hit, 10))
        {
            Logger.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.blue);
            target = hit.collider.gameObject;

            if (target.CompareTag("Interactable"))
            {
                var newInteractable = target.GetComponent<Interactable>(); 
                if (newInteractable != null && newInteractable != activeInteractable)
                {
                    newInteractable.Focus(true);
                    if(activeInteractable != null)
                        activeInteractable.Focus(false);
                    
                    activeInteractable = newInteractable;
                }
            }
            else if (!target.CompareTag("Interactable") && activeInteractable != null)
                activeInteractable.Focus(false);
        }
    }
}