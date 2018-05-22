using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PS4Input : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		InputManager.instance.actionInputDown = Input.GetKeyDown (KeyCode.JoystickButton1);
		InputManager.instance.actionInputUp = Input.GetKeyUp (KeyCode.JoystickButton1);
		InputManager.instance.actionInputStay = Input.GetKey (KeyCode.JoystickButton1);

		InputManager.instance.horizontalInput = Input.GetAxis ("HorizontalPS4");
		InputManager.instance.verticalInput = Input.GetAxis ("VerticalPS4");
		InputManager.instance.cameraHorizontal = Input.GetAxis ("CameraHorizontalPS4");
		InputManager.instance.cameraVertical = Input.GetAxis ("CameraVerticalPS4");

		InputManager.instance.strongInputDown = Input.GetKeyUp (KeyCode.JoystickButton5);
		InputManager.instance.strongInputDown = Input.GetKeyDown (KeyCode.JoystickButton5);
		InputManager.instance.strongInputStay = Input.GetKey (KeyCode.JoystickButton5);

		InputManager.instance.parryInputDown = Input.GetKeyUp (KeyCode.JoystickButton4);
		InputManager.instance.parryInputUp = Input.GetKeyDown (KeyCode.JoystickButton4);
		InputManager.instance.parryInputStay = Input.GetKey (KeyCode.JoystickButton4);
	}
}
