using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Camera currentCam;
    public GameObject player;
    public float rotationSpeed = 3;
    public Vector3 camOffSet;
    float origXRotation;
    Vector3 origForward;
    public float camUpInputMin = 0.3f;
    public Vector2 maxXRotation;
    [Range (0f,1f)]
    public float XRotReturnSpeed = 0.1f;
    playerMovement playerMoveScript;
    Transform camTrans;
    // Use this for initialization
    void Start()
    {
        playerMoveScript = player.GetComponent<playerMovement>();
        camTrans = currentCam.transform;
        origXRotation = camTrans.rotation.eulerAngles.x;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Rotate();
        Translate();
    }

    void Rotate()
    {
        if (Mathf.Abs(InputManager.instance.cameraHorizontal) > 0) // horizontal rotation
        {
            camTrans.RotateAround(player.transform.position, Vector3.up, rotationSpeed * InputManager.instance.cameraHorizontal);
            playerMoveScript.currentForward = new Vector3(camTrans.forward.x, 0, camTrans.forward.z).normalized;
            playerMoveScript.currentRight = new Vector3(camTrans.right.x, 0, camTrans.right.z).normalized;
        }
        if (Mathf.Abs(InputManager.instance.cameraVertical) > camUpInputMin) // vertical rotation
        {
            if (InputManager.instance.cameraVertical > 0 &&
                camTrans.rotation.eulerAngles.x + InputManager.instance.cameraVertical * rotationSpeed < origXRotation + maxXRotation.y)
            {
                camTrans.RotateAround(camTrans.position, camTrans.right, InputManager.instance.cameraVertical * rotationSpeed);
            }
            if ((InputManager.instance.cameraVertical < 0 &&(
                camTrans.rotation.eulerAngles.x + InputManager.instance.cameraVertical * rotationSpeed > origXRotation + maxXRotation.x &&
                camTrans.rotation.eulerAngles.x + InputManager.instance.cameraVertical * rotationSpeed < origXRotation + maxXRotation.y) ||
                    camTrans.rotation.eulerAngles.x + InputManager.instance.cameraVertical * rotationSpeed > 360 + maxXRotation.x + origXRotation))
            {
                camTrans.RotateAround(camTrans.position, camTrans.right, InputManager.instance.cameraVertical * rotationSpeed);
            }
        }
         else if (origXRotation != camTrans.rotation.eulerAngles.x)
         {
             if (camTrans.rotation.eulerAngles.x < origXRotation + maxXRotation.y)
             {
                 camTrans.RotateAround(camTrans.position, camTrans.right, (origXRotation - camTrans.rotation.eulerAngles.x) * XRotReturnSpeed);
             }
             else
             {
                 camTrans.RotateAround(camTrans.position, camTrans.right, (360 - camTrans.rotation.eulerAngles.x + origXRotation) * XRotReturnSpeed);
             }
         }
    }

    void Translate()
    {
        camTrans.position = player.transform.position + camOffSet.x * camTrans.forward + camOffSet.y * camTrans.up + camOffSet.z * camTrans.right;
    }
}
