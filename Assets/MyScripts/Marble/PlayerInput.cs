using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine;

public class PlayerInput : IPlayerInput
{
   #if !ENABLE_TESTING
   public VirtualJoystick input;
   public Vector2 Input { get { return input.GetInput(); } }
   #else
   public Vector2 Input { get { return new Vector2(0, 0); } }
   #endif

   public PlayerInput()
   {
      #if !ENABLE_TESTING
      input = GameObject.FindGameObjectsWithTag("Joystick")[0].GetComponent<VirtualJoystick>();
      #endif
   }
}