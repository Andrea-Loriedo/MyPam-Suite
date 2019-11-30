using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine;

public class VirtualJoystick : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler {

	Image background;
	Image knob;
	Vector2 input;

	public SerialHandler myPam;

	private void Start ()
	{
		input = myPam.myPamInput;
		background = GetComponent<Image>(); 
		knob = transform.GetChild(0).GetComponent<Image>();
	}

	public void Update ()
	{
		MoveJoystick(input);
	}

	public virtual void OnPointerUp(PointerEventData ped)
	{
		input = myPam.myPamInput; // myPam override mouse control
		knob.rectTransform.anchoredPosition = Vector2.zero;
	}

	public virtual void OnPointerDown(PointerEventData ped)
	{
		OnDrag(ped);
	}

	public virtual void OnDrag(PointerEventData ped)
	{
		GetMouseInput(ped);
	}

	void MoveJoystick(Vector2 controllerInput)
	{
		// clamp the joystick to its frame
		if(input.magnitude > 1.0f)
		{
			input = input.normalized;
		}
		else
		{
			input = input;
		}

		knob.rectTransform.anchoredPosition = new Vector2(input.x * (background.rectTransform.sizeDelta.x/2), input.y * (background.rectTransform.sizeDelta.x/2));
	}

	void GetMouseInput(PointerEventData ped)
	{
		Vector2 mousePos;
		// returns true if the plane of the RectTransform is hit, regardless of whether the point is inside the rectangle
		if (RectTransformUtility.ScreenPointToLocalPointInRectangle(background.rectTransform, // the RectTransform to find a point inside
																				ped.position, // screen space mouse position
																				ped.pressEventCamera, // the camera associated with the screen space mousePos.
																				out mousePos)) // mouse position in local space of the rect transform
		{
			// get mousePos value between -1 and 0 
			mousePos.x = (mousePos.x / background.rectTransform.sizeDelta.x);
			mousePos.y = (mousePos.y / background.rectTransform.sizeDelta.y);

			// correct joystick "0" mousePos and override myPam control
			input = new Vector2(mousePos.x*2 + 1, mousePos.y*2 - 1); 
		}
	}

	// returns the horizontal component for marble direction based on the control input
	public float Horizontal()
	{
		if(input.x != 0)
			return input.x; 
		else
			return Input.GetAxis("Horizontal");
	}

	// returns the vertical component for marble direction based on the control input
	public float Vertical()
	{
		if(input.y != 0)
			return input.y;
		else
			return Input.GetAxis("Vertical");
	}
}
