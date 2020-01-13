using System.Collections.Generic;
using UnityEngine;

public class Interactor : MonoBehaviour
{
    // Equivalent to public delegate void OnCanInteract(Interactable target)
    public Interactable activeInteractable { get; private set; }

    void Update()
    {
        // In case we move the object, we want to check if there have been any interaction changes.
        // if (transform.hasChanged)
        // {
            CheckIfInRange();
        // }
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
        Vector3 down = transform.TransformDirection(Vector3.down);
        GameObject target;
        RaycastHit hit;

        if (Physics.Raycast(transform.position, down, out hit, 10))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.blue);
            target = hit.collider.gameObject;
                            Logger.Debug("Interact!");


            if (target.CompareTag("Interactable"))
            {
                var interactable = target.GetComponent<Interactable>(); 
                if (interactable != null)
                {
                    activeInteractable = interactable;
                    activeInteractable.Focus(true);
                }
            }
        }
    }
}