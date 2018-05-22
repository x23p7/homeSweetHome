using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour {
	public static InputManager instance;

    string controllerName;
	public MonoBehaviour XboxControles;
	public MonoBehaviour KeyBoardControles;
	public MonoBehaviour PS4Input;

	public bool actionInputDown;
	public bool actionInputUp;
	public bool actionInputStay;

    public bool backStepInputDown;
    public bool backStepInputUp;
    public bool backStepInputStay;

    public bool itemInputDown;
    public bool itemInputUp;
    public bool itemInputStay;

    public bool vialInputDown;
    public bool vialInputUp;
    public bool vialInputStay;

	public float horizontalInput;
	public float verticalInput;
	public float cameraHorizontal;
	public float cameraVertical;

	public bool strongInputDown;
	public bool strongInputUp;
	public bool strongInputStay;

	public bool parryInputDown;
	public bool parryInputUp;
	public bool parryInputStay;

	// Use this for initialization

	void Awake(){
		if (instance != null) {
			Destroy (this);
		} else {
			instance = this;
		}
	}
	void Start () {
        if (Input.GetJoystickNames().Length > 0) { 
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
        }
        else {
			XboxControles.enabled = false;
			KeyBoardControles.enabled = true;
			PS4Input.enabled = false;

		}
	}
}
