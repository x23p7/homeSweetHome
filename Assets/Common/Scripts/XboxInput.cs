using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class XboxInput : MonoBehaviour
{
    public bool disabled;

    // Use this for initialization
    private void Update()
    {
        UpdateInput();
    }
    void UpdateInput()
    {
        if (!InputManager.instance.disabled)
        {
            InputManager.instance.actionInputDown = Input.GetKeyDown(KeyCode.JoystickButton1);
            InputManager.instance.actionInputUp = Input.GetKeyUp(KeyCode.JoystickButton1);
            InputManager.instance.actionInputStay = Input.GetKey(KeyCode.JoystickButton1);

            InputManager.instance.horizontalInput = Input.GetAxis("HorizontalXBOX");
            InputManager.instance.verticalInput = Input.GetAxis("VerticalXBOX");
            InputManager.instance.cameraHorizontal = Input.GetAxis("CameraHorizontalPS4");
            InputManager.instance.cameraVertical = Input.GetAxis("CameraVerticalPS4");

            InputManager.instance.strongInputUp = Input.GetKeyUp(KeyCode.JoystickButton5);
            InputManager.instance.strongInputDown = Input.GetKeyDown(KeyCode.JoystickButton5);
            InputManager.instance.strongInputStay = Input.GetKey(KeyCode.JoystickButton5);

            InputManager.instance.parryInputDown = Input.GetKeyUp(KeyCode.JoystickButton4);
            InputManager.instance.parryInputUp = Input.GetKeyDown(KeyCode.JoystickButton4);
            InputManager.instance.parryInputStay = Input.GetKey(KeyCode.JoystickButton4);

            InputManager.instance.backStepInputDown = Input.GetKeyDown(KeyCode.JoystickButton2);
            InputManager.instance.backStepInputUp = Input.GetKeyUp(KeyCode.JoystickButton2);
            InputManager.instance.backStepInputStay = Input.GetKey(KeyCode.JoystickButton2);

            InputManager.instance.itemInputDown = Input.GetKeyDown(KeyCode.JoystickButton0);
            InputManager.instance.itemInputUp = Input.GetKeyUp(KeyCode.JoystickButton0);
            InputManager.instance.itemInputStay = Input.GetKey(KeyCode.JoystickButton0);

            InputManager.instance.vialInputDown = Input.GetKeyDown(KeyCode.JoystickButton3);
            InputManager.instance.vialInputUp = Input.GetKeyUp(KeyCode.JoystickButton3);
            InputManager.instance.vialInputStay = Input.GetKey(KeyCode.JoystickButton3);
        }
        else
        {
            InputManager.instance.choiceOne = Input.GetKeyDown(KeyCode.JoystickButton1);
            InputManager.instance.choiceTwo = Input.GetKeyDown(KeyCode.JoystickButton2);
            InputManager.instance.choiceThree = Input.GetKeyDown(KeyCode.JoystickButton0);
            InputManager.instance.choiceFour = Input.GetKeyDown(KeyCode.JoystickButton3);
        }
    }
}
