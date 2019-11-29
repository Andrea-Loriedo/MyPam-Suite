using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine;

public class VirtualJoystick : MonoBehaviour //, IDragHandler, IPointerUpHandler, IPointerDownHandler{
{
	private Image background;
	private Image knob;
	private Vector2 input;

	[SerializeField] public SerialHandler myPam;

	private void Start()
	{
		background = GetComponent<Image>(); 
		knob = transform.GetChild(0).GetComponent<Image>();
	}

	public virtual void OnPointerUp(PointerEventData ped)
	{
		input = Vector2.zero;
		knob.rectTransform.anchoredPosition = Vector2.zero;
	}

	// public virtual void OnDrag(PointerEventData ped)
	// {
	// 	Vector2 position;

	// 	// returns true if the plane of the RectTransform is hit, regardless of whether the point is inside the rectangle
	// 	if (RectTransformUtility.ScreenPointToLocalPointInRectangle(background.rectTransform, // the RectTransform to find a point inside
	// 																			ped.position, // screen space position
	// 																			ped.pressEventCamera, // the camera associated with the screen space position.
	// 																			out position)) // point in local space of the rect transform
	// 	{

	// 		// get position value between -1 and 0 
	// 		position.x = (position.x / background.rectTransform.sizeDelta.x);
	// 		position.y = (position.y / background.rectTransform.sizeDelta.y);

	// 		// correct joystick "0" position
	// 		input = new Vector2(position.x*2 + 1, position.y*2 - 1); 

	// 		if(input.magnitude > 1.0f)
	// 		{
	// 			input = input.normalized;
	// 		}
	// 		else
	// 		{
	// 			input = input;
	// 		}
			
	// 		// float x = myPam.BallInputPosition.x;
	// 		// float z = myPam.BallInputPosition.z;

	// 		// move knob in the rectangle local space
	// 		knob.rectTransform.anchoredPosition = new Vector2(input.x * (background.rectTransform.sizeDelta.x/2), input.y * (background.rectTransform.sizeDelta.x/2));
	// 	}
	// }

	public void Update()
	{
		input = new Vector2(myPam.myPamInput.x, myPam.myPamInput.y); 
		Debug.Log(input);

		// constrain the joystick to its frame
		if(input.magnitude > 1.0f)
		{
			input = input.normalized;
		}
		else
		{
			input = input;
		}

		knob.rectTransform.anchoredPosition = new Vector2(input.x * (background.rectTransform.sizeDelta.x/2), input.y * (background.rectTransform.sizeDelta.x/2));
		// Debug.Log(knob.rectTransform.anchoredPosition);
	}

	public virtual void OnPointerDown(PointerEventData ped)
	{
		// OnDrag(ped);
	}


	public float MyPamHorizontal()
	{
		if(input.x != 0)
			return input.x;
		else
			return Input.GetAxis("Horizontal");
	}

	public float MyPamVertical()
	{
		if(input.y != 0)
			return input.y;
		else
			return Input.GetAxis("Horizontal");
	}

	public float MouseHorizontal()
	{
		if(input.x != 0)
			return input.x;
		else
			return Input.GetAxis("Horizontal");
	}

	public float MouseVertical()
	{
		if(input.y != 0)
			return input.y;
		else
			return Input.GetAxis("Vertical");
	}
}
