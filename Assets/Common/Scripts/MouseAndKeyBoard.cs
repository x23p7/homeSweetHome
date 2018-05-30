using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseAndKeyBoard : MonoBehaviour {
    public bool disabled;

	// Use this for initialization
	void Start () {
	}

	// Update is called once per frame
	void Update () {
		InputManager.instance.actionInputDown = Input.GetKeyDown (KeyCode.E);
		InputManager.instance.actionInputUp = Input.GetKeyUp (KeyCode.E);
		InputManager.instance.actionInputStay = Input.GetKey (KeyCode.E);

		InputManager.instance.horizontalInput = Input.GetAxis ("Horizontal");
		InputManager.instance.verticalInput = Input.GetAxis ("Vertical");
		InputManager.instance.cameraHorizontal = Input.GetAxis ("CameraHorizontal");
		InputManager.instance.cameraVertical = Input.GetAxis ("CameraVertical");

		InputManager.instance.strongInputDown = Input.GetKeyUp (KeyCode.Mouse0);
		InputManager.instance.strongInputDown = Input.GetKeyDown (KeyCode.Mouse0);
		InputManager.instance.strongInputStay = Input.GetKey (KeyCode.Mouse0);

		InputManager.instance.parryInputDown = Input.GetKeyUp (KeyCode.Mouse1);
		InputManager.instance.parryInputUp = Input.GetKeyDown (KeyCode.Mouse1);
		InputManager.instance.parryInputStay = Input.GetKey (KeyCode.Mouse1);
	}
}
