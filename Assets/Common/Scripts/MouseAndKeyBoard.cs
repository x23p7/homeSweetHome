using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseAndKeyBoard : MonoBehaviour {
    public bool disabled;

    private void Update()
    {
        UpdateInput();
    }

    // Update is called once per frame
    void UpdateInput()
    {
        if (!InputManager.instance.disabled)
        {
            InputManager.instance.actionInputDown = Input.GetKeyDown(KeyCode.Space);
            InputManager.instance.actionInputUp = Input.GetKeyUp(KeyCode.Space);
            InputManager.instance.actionInputStay = Input.GetKey(KeyCode.Space);

            InputManager.instance.horizontalInput = Input.GetAxis("Horizontal");
            InputManager.instance.verticalInput = Input.GetAxis("Vertical");
            InputManager.instance.cameraHorizontal = Input.GetAxis("CameraHorizontal");
            InputManager.instance.cameraVertical = Input.GetAxis("CameraVertical");

            InputManager.instance.strongInputUp = Input.GetKeyUp(KeyCode.LeftControl);
            InputManager.instance.strongInputDown = Input.GetKeyDown(KeyCode.LeftControl);
            InputManager.instance.strongInputStay = Input.GetKey(KeyCode.LeftControl);

            InputManager.instance.parryInputDown = Input.GetKeyUp(KeyCode.LeftShift);
            InputManager.instance.parryInputUp = Input.GetKeyDown(KeyCode.LeftShift);
            InputManager.instance.parryInputStay = Input.GetKey(KeyCode.LeftShift);

            InputManager.instance.backStepInputDown = Input.GetKeyDown(KeyCode.G);
            InputManager.instance.backStepInputUp = Input.GetKeyUp(KeyCode.G);
            InputManager.instance.backStepInputStay = Input.GetKey(KeyCode.G);

            InputManager.instance.itemInputDown = Input.GetKeyDown(KeyCode.I);
            InputManager.instance.itemInputUp = Input.GetKeyUp(KeyCode.I);
            InputManager.instance.itemInputStay = Input.GetKey(KeyCode.I);

            InputManager.instance.vialInputDown = Input.GetKeyDown(KeyCode.V);
            InputManager.instance.vialInputUp = Input.GetKeyUp(KeyCode.V);
            InputManager.instance.vialInputStay = Input.GetKey(KeyCode.V);
        }
        else
        {
            InputManager.instance.choiceOne = Input.GetKeyDown(KeyCode.DownArrow);
            InputManager.instance.choiceTwo = Input.GetKeyDown(KeyCode.RightArrow);
            InputManager.instance.choiceThree = Input.GetKeyDown(KeyCode.LeftArrow);
            InputManager.instance.choiceFour = Input.GetKeyDown(KeyCode.UpArrow);
        }
    }
}
