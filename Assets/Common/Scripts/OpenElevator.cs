using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenElevator : MonoBehaviour {
    public Animator leftDoorAnim;
    public Animator rightDoorAnim;
    public ElevatorScript elevatorScript;
    // Use this for initialization
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (InputManager.instance.actionInputDown && elevatorScript.rideComplete)
            {
                leftDoorAnim.SetTrigger("playerInteraction");
                rightDoorAnim.SetTrigger("playerInteraction");
            }
        }
    }
}
