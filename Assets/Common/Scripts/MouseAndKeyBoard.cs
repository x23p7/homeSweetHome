using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseAndKeyBoard : MonoBehaviour {


	// Use this for initialization
	void Start () {
	}

	// Update is called once per frame
	void Update () {
		InputManager.instance.jumpInputDown = Input.GetKeyDown (KeyCode.Space);
		InputManager.instance.jumpInputUp = Input.GetKeyUp (KeyCode.Space);
		InputManager.instance.jumpInputStay = Input.GetKey (KeyCode.Space);

		InputManager.instance.horizontalInput = Input.GetAxis ("Horizontal");
		InputManager.instance.verticalInput = Input.GetAxis ("Vertical");
		//InputManager.instance.colorWheelHorizontal = Input.GetAxis ("ColorWheelHorizontal");
		//InputManager.instance.colorWheelVertical = Input.GetAxis ("ColorWheelVertical");

		InputManager.instance.shootInputUp = Input.GetKeyUp (KeyCode.Mouse0);
		InputManager.instance.shootInputDown = Input.GetKeyDown (KeyCode.Mouse0);
		InputManager.instance.shootInputStay = Input.GetKey (KeyCode.Mouse0);

		InputManager.instance.aimInputUp = Input.GetKeyUp (KeyCode.Mouse1);
		InputManager.instance.aimInputDown = Input.GetKeyDown (KeyCode.Mouse1);
		InputManager.instance.aimInputStay = Input.GetKey (KeyCode.Mouse1);
	}
}
