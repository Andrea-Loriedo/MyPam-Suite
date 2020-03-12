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
        CheckIfInStartZone();
        CheckIfInHoleRange();
        TryWhack();
    }

    private void CheckIfInHoleRange()
    {
        Vector3 down = transform.TransformDirection(Vector3.left);
        RaycastHit hit;

        if (Physics.Raycast(transform.position, down, out hit, 10))
        {
            Logger.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.red);
            GameObject target = hit.collider.gameObject; // The object hit by the ray cast
            var newInteractable = target.GetComponent<Interactable>(); 
            
            if (newInteractable != activeInteractable)
            {
                if (activeInteractable != null)
                {
                    activeInteractable.Focus(false); // Notify the interfaces of the last active interactable that it is no longer being focussed
                }

                if (newInteractable != null)
                {
                    newInteractable.Focus(true); // Notify the new interactable that it is being focussed
                }

                activeInteractable = newInteractable; // Update active interactable to new interactable
            }
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

    void CheckIfInStartZone()
    {
        if (activeInteractable != null && activeInteractable.CompareTag("StartZone"))
            activeInteractable.Interact();
    }
}