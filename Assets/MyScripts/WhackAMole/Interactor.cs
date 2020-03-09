using System.Collections.Generic;
using UnityEngine;

public class Interactor : MonoBehaviour
{
    // Equivalent to public delegate void OnCanInteract(Interactable target)
    public Interactable activeInteractable { get; private set; }
    HammerController hammer;

    void Start()
    {
        hammer = GetComponentInParent<HammerController>();
    }

    void FixedUpdate()
    {
        CheckIfAtStartPoint();
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
            // Logger.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.blue);
            target = hit.collider.gameObject; // The object hit by the ray cast
            var newInteractable = target.GetComponent<Interactable>(); 
            
            if (newInteractable != null)
            {
                newInteractable.Focus(true);
                activeInteractable = newInteractable;
                newInteractable = null;
            }
            else if (newInteractable == null)
            activeInteractable.Focus(false);
        }
    }

    public bool TryWhack()
    {
        if (activeInteractable != null && activeInteractable.CompareTag("Mole"))
        {
            activeInteractable.Interact();
            if (hammer != null)
                hammer.Animate();

            return true;
        }
        else
        {
            return false;
        }
    }

    void CheckIfAtStartPoint()
    {
        if (activeInteractable != null && activeInteractable.CompareTag("StartZone"))
        {
            activeInteractable.Interact();
        }
    }
}