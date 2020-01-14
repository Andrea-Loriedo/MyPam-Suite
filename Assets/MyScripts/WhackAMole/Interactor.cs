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
            TryWhack();
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

                var newInteractable = target.GetComponent<Interactable>(); 
                if (newInteractable != null && newInteractable != activeInteractable)
                {
                    newInteractable.Focus(true);
                    activeInteractable = newInteractable;
                    newInteractable = null;
                }
            else if (newInteractable == null && activeInteractable != null)
                activeInteractable.Focus(false);
        }
    }

    public bool TryWhack()
    {
        if (activeInteractable != null && activeInteractable.CompareTag("Mole"))
        {
            activeInteractable.Interact();
            return true;
        }
        else
        {
            return false;
        }
    }
}