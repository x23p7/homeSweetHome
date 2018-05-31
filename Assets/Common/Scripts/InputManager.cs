using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
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

    public bool choiceOne;
    public bool choiceTwo;
    public bool choiceThree;
    public bool choiceFour;

    public bool disabled;
    public MonoBehaviour activeInputScript;
    // Use this for initialization

    void Awake()
    {
        if (instance != null)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }
    void Start()
    {
        if (Input.GetJoystickNames().Length > 0)
        {
            controllerName = Input.GetJoystickNames()[0];
            if (controllerName == "Controller (XBOX 360 For Windows)")
            {
                XboxControles.enabled = true;
                KeyBoardControles.enabled = false;
                PS4Input.enabled = false;
                activeInputScript = XboxControles;
            }
            else if (controllerName == "Wireless Controller")
            {
                XboxControles.enabled = false;
                KeyBoardControles.enabled = false;
                PS4Input.enabled = true;
                activeInputScript = PS4Input;
            }
        }
        else
        {
            XboxControles.enabled = false;
            KeyBoardControles.enabled = true;
            PS4Input.enabled = false;
            activeInputScript = KeyBoardControles;
        }
    }

    public void Reset()
    {
        actionInputDown = false;
        actionInputUp = false;
        actionInputStay = false;

        backStepInputDown = false;
        backStepInputUp = false;
        backStepInputStay = false;

        itemInputDown = false;
        itemInputUp = false;
        itemInputStay = false;

        vialInputDown = false;
        vialInputUp = false;
        vialInputStay = false;

        horizontalInput = 0;
        verticalInput = 0;
        cameraHorizontal = 0;
        cameraVertical = 0;

        strongInputDown = false;
        strongInputUp = false;
        strongInputStay = false;

        parryInputDown = false;
        parryInputUp = false;
        parryInputStay = false;

        choiceOne = false;
        choiceTwo = false;
        choiceThree = false;
        choiceFour = false;
    }
}
