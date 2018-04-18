using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour {
	public static InputManager instance;

    string controllerName;
	public MonoBehaviour XboxControles;
	public MonoBehaviour KeyBoardControles;
	public MonoBehaviour PS4Input;

	public bool jumpInputDown;
	public bool jumpInputUp;
	public bool jumpInputStay;

	public float horizontalInput;
	public float verticalInput;
	public float colorWheelHorizontal;
	public float colorWheelVertical;

	public bool shootInputUp;
	public bool shootInputDown;
	public bool shootInputStay;

	public bool aimInputUp;
	public bool aimInputDown;
	public bool aimInputStay;

	// Use this for initialization

	void Awake(){
		if (instance != null) {
			Destroy (this);
		} else {
			instance = this;
		}
	}
	void Start () {/*
		controllerName = Input.GetJoystickNames()[0];
		if (controllerName == "Controller (XBOX 360 For Windows)") {
			XboxControles.enabled = true;
			KeyBoardControles.enabled = false;
			PS4Input.enabled = false;
		} else if (controllerName == "Wireless Controller") {
			XboxControles.enabled = false;
			KeyBoardControles.enabled = false;
			PS4Input.enabled = true;
		}
		else {*/
			XboxControles.enabled = false;
			KeyBoardControles.enabled = true;
			PS4Input.enabled = false;

		//}
	}
	
	// Update is called once per frame
	void Update () {
	}
}
