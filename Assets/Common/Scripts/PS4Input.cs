using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PS4Input : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		InputManager.instance.jumpInputDown = Input.GetKeyDown (KeyCode.JoystickButton1);
		InputManager.instance.jumpInputUp = Input.GetKeyUp (KeyCode.JoystickButton1);
		InputManager.instance.jumpInputStay = Input.GetKey (KeyCode.JoystickButton1);

		InputManager.instance.horizontalInput = Input.GetAxis ("HorizontalPS4");
		InputManager.instance.verticalInput = Input.GetAxis ("VerticalPS4");
		InputManager.instance.cameraHorizontal = Input.GetAxis ("CameraHorizontalPS4");
		InputManager.instance.cameraVertical = Input.GetAxis ("CameraVerticalPS4");

		InputManager.instance.shootInputUp = Input.GetKeyUp (KeyCode.JoystickButton5);
		InputManager.instance.shootInputDown = Input.GetKeyDown (KeyCode.JoystickButton5);
		InputManager.instance.shootInputStay = Input.GetKey (KeyCode.JoystickButton5);

		InputManager.instance.aimInputUp = Input.GetKeyUp (KeyCode.JoystickButton4);
		InputManager.instance.aimInputDown = Input.GetKeyDown (KeyCode.JoystickButton4);
		InputManager.instance.aimInputStay = Input.GetKey (KeyCode.JoystickButton4);
	}
}
