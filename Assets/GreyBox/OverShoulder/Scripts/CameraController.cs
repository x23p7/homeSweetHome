using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Camera currentCam;
    public GameObject player;
    public float horizontalRotSpeed = 3;
    public float verticalRotSpeed = 1;
    public Vector3 camOffSet;
    public float camBackwardsCheckRange = 0.5f;
    public LayerMask camPushMask;
    public float camPushSpeed;
    public float minCamWallDist = 0.3f;
    RaycastHit camBackWall;
    Vector3 camOffSetOrig;
    float origXRotation;
    Vector3 origForward;
    public float camUpInputMin = 0.3f;
    public Vector2 maxXRotation;
    [Range (0f,1f)]
    public float XRotReturnSpeed = 0.1f;
    playerMovement playerMoveScript;
    Rigidbody playerRig;
    Transform camTrans;
    // Use this for initialization
    void Start()
    {
        playerMoveScript = player.GetComponent<playerMovement>();
        playerRig = player.GetComponent<Rigidbody>();
        camTrans = currentCam.transform;
        origXRotation = camTrans.rotation.eulerAngles.x;
        camOffSetOrig = camOffSet;
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
            camTrans.RotateAround(player.transform.position, Vector3.up, horizontalRotSpeed * InputManager.instance.cameraHorizontal);
            playerMoveScript.currentForward = new Vector3(camTrans.forward.x, 0, camTrans.forward.z).normalized;
            playerMoveScript.currentRight = new Vector3(camTrans.right.x, 0, camTrans.right.z).normalized;
            Debug.DrawRay(player.transform.position + camOffSet.y * camTrans.up + camOffSet.z*camTrans.right, (camOffSet.x * camTrans.forward), Color.red, 5f);
            if (Physics.Raycast(player.transform.position + camOffSet.y*camTrans.up, camOffSet.x * camTrans.forward, out camBackWall, (camOffSet.x * camTrans.forward).magnitude, camPushMask))
            {
                camOffSet = new Vector3(camOffSet.x + minCamWallDist + (camBackWall.point- camTrans.position).magnitude, camOffSet.y, camOffSet.z);
            }
        }
        if (Mathf.Abs(InputManager.instance.cameraVertical) > camUpInputMin) // vertical rotation
        {
            if (InputManager.instance.cameraVertical > 0 &&
                camTrans.rotation.eulerAngles.x + InputManager.instance.cameraVertical * verticalRotSpeed < origXRotation + maxXRotation.y)
            {
                camTrans.RotateAround(camTrans.position, camTrans.right, InputManager.instance.cameraVertical * verticalRotSpeed);
            }
            if ((InputManager.instance.cameraVertical < 0 &&(
                camTrans.rotation.eulerAngles.x + InputManager.instance.cameraVertical * verticalRotSpeed > origXRotation + maxXRotation.x &&
                camTrans.rotation.eulerAngles.x + InputManager.instance.cameraVertical * verticalRotSpeed < origXRotation + maxXRotation.y) ||
                    camTrans.rotation.eulerAngles.x + InputManager.instance.cameraVertical * verticalRotSpeed > 360 + maxXRotation.x + origXRotation))
            {
                camTrans.RotateAround(camTrans.position, camTrans.right, InputManager.instance.cameraVertical * horizontalRotSpeed);
            }
        }
         else if (origXRotation != camTrans.rotation.eulerAngles.x && playerRig.velocity.magnitude>0.1)
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
        if (!Physics.Raycast(camTrans.position, -camTrans.forward, camBackwardsCheckRange + 4*camPushSpeed, camPushMask) && camOffSet.x + camPushSpeed >= camOffSetOrig.x)
        {
            camOffSet = new Vector3(camOffSet.x - camPushSpeed, camOffSet.y, camOffSet.z);
        }
        if (Physics.Raycast(camTrans.position, -camTrans.forward , out camBackWall, camBackwardsCheckRange, camPushMask) && camOffSet.x - camPushSpeed <=0)
        {
            if ((camBackWall.point - camTrans.position).magnitude > minCamWallDist)
            { 
                camOffSet = new Vector3(camOffSet.x + camPushSpeed, camOffSet.y, camOffSet.z);
            }
            else
            {
                camOffSet = new Vector3(camOffSet.x + minCamWallDist - (camBackWall.point - camTrans.position).magnitude, camOffSet.y, camOffSet.z);
            }
        }
        camTrans.position = player.transform.position + camOffSet.x * camTrans.forward + camOffSet.y * camTrans.up + camOffSet.z * camTrans.right;
    }
}
