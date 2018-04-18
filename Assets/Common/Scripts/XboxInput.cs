using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class XboxInput : MonoBehaviour {

		
	// Use this for initialization
	void Start () {
	}

	// Update is called once per frame
	void Update () {
		InputManager.instance.jumpInputDown = Input.GetKeyDown (KeyCode.JoystickButton0);
		InputManager.instance.jumpInputUp = Input.GetKeyUp (KeyCode.JoystickButton0);
		InputManager.instance.jumpInputStay = Input.GetKey (KeyCode.JoystickButton0);

		InputManager.instance.horizontalInput = Input.GetAxis ("HorizontalXBOX");
		InputManager.instance.verticalInput = Input.GetAxis ("VerticalXBOX");
		InputManager.instance.colorWheelHorizontal = Input.GetAxis ("ColorWheelHorizontalXBOX");
		InputManager.instance.colorWheelVertical = Input.GetAxis ("ColorWheelVerticalXBOX");

		InputManager.instance.shootInputUp = Input.GetKeyUp (KeyCode.JoystickButton5);
		InputManager.instance.shootInputDown = Input.GetKeyDown (KeyCode.JoystickButton5);
		InputManager.instance.shootInputStay = Input.GetKey (KeyCode.JoystickButton5);

		InputManager.instance.aimInputUp = Input.GetKeyUp (KeyCode.JoystickButton4);
		InputManager.instance.aimInputDown = Input.GetKeyDown (KeyCode.JoystickButton4);
		InputManager.instance.aimInputStay = Input.GetKey (KeyCode.JoystickButton4);
	}
}
