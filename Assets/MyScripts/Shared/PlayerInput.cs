using UnityEngine;

public class PlayerInput : IPlayerInput
{
   public VirtualJoystick input;

   void Awake()
   {
      input = GameObject.FindGameObjectsWithTag("Joystick")[0].GetComponent<VirtualJoystick>();
   }

   public Vector2 Input => input.GetInput();
}