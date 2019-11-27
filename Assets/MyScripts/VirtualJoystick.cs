using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine;

public class VirtualJoystick : MonoBehaviour , IDragHandler, IPointerUpHandler, IPointerDownHandler{

	private Image background;
	private Image knob;
	private Vector3 input;

	private void Start()
	{
		background = GetComponent<Image>(); 
		knob = transform.GetChild(0).GetComponent<Image>();
	}

	public virtual void OnPointerDown(PointerEventData ped)
	{
		OnDrag(ped);
	}

	public virtual void OnPointerUp(PointerEventData ped)
	{
		input = Vector2.zero;
		knob.rectTransform.anchoredPosition = Vector2.zero;
	}

	public virtual void OnDrag(PointerEventData ped)
	{
		Vector2 position;

		if (RectTransformUtility.ScreenPointToLocalPointInRectangle(background.rectTransform, // the RectTransform to find a point inside
																				ped.position, // screen space position
																				ped.pressEventCamera, // the camera associated with the screen space position.
																				out position)) // point in local space of the rect transform
		{
			position.x = (position.x / background.rectTransform.sizeDelta.x);
			position.y = (position.y / background.rectTransform.sizeDelta.x);

			input = new Vector2(position.x*2 + 1, position.y*2 - 1); 

			if(input.magnitude > 1.0f)
			{
				input = input.normalized;
			}
			else
			{
				input = input;
			}

			knob.rectTransform.anchoredPosition = new Vector3(input.x * (background.rectTransform.sizeDelta.x/2), input.y * (background.rectTransform.sizeDelta.x/2));
		}
	}

	public float Horizontal()
	{
		if(input.x != 0)
			return input.x;
		else
			return Input.GetAxis("Horizontal");
	}
	public float Vertical()
	{
		if(input.y != 0)
			return input.y;
		else
			return Input.GetAxis("Vertical");
	}
}
