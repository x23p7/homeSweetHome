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
    public float camMoveSmooth = 0.5f;
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
    Vector3 playerHead;
    Transform camTrans;
    float distanceVectorDelta;
    float cameraHeadVectorDelta;
    Vector3 camPointer;
    Collider[] colls;
    Vector3 camPos;
    float maxDistance;
    float camDistance;
    Vector3 playerPos;
    // Use this for initialization
    void Start()
    {
        playerMoveScript = player.GetComponent<playerMovement>();
        playerRig = player.GetComponent<Rigidbody>();
        camTrans = currentCam.transform;
        origXRotation = camTrans.rotation.eulerAngles.x;
        camOffSetOrig = camOffSet;
        camTrans.position = player.transform.position + camOffSet.x * camTrans.forward + camOffSet.y * camTrans.up + camOffSet.z * camTrans.right;
        playerHead = player.transform.position + player.transform.up * player.transform.localScale.y / 2;
        camPointer = (camTrans.position - playerHead).normalized;
        maxDistance = (camTrans.position - playerHead).magnitude;
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        Rotate();
        Translate();
    }

    void Rotate()
    {
        playerPos = player.transform.position;
        playerHead = playerPos + player.transform.up * player.transform.localScale.y / 2;
        camTrans.position = playerHead + camPointer * maxDistance;
        if (Mathf.Abs(InputManager.instance.cameraHorizontal) > 0) // horizontal rotation
        {
            camTrans.RotateAround(playerHead, Vector3.up, horizontalRotSpeed * InputManager.instance.cameraHorizontal);
            playerMoveScript.currentForward = new Vector3(camTrans.forward.x, 0, camTrans.forward.z).normalized;
            playerMoveScript.currentRight = new Vector3(camTrans.right.x, 0, camTrans.right.z).normalized;
            Debug.DrawRay(playerPos + camOffSet.y * camTrans.up, (camOffSet.x * camTrans.forward), Color.red, 5f);
        }
        if (Mathf.Abs(InputManager.instance.cameraVertical) > camUpInputMin) // vertical rotation
        {
            if (InputManager.instance.cameraVertical > 0 &&
                camTrans.rotation.eulerAngles.x + InputManager.instance.cameraVertical * verticalRotSpeed < origXRotation + maxXRotation.y)
            {
                camTrans.RotateAround(playerHead, camTrans.right, InputManager.instance.cameraVertical * verticalRotSpeed);
            }
            if ((InputManager.instance.cameraVertical < 0 &&(
                camTrans.rotation.eulerAngles.x + InputManager.instance.cameraVertical * verticalRotSpeed > origXRotation + maxXRotation.x &&
                camTrans.rotation.eulerAngles.x + InputManager.instance.cameraVertical * verticalRotSpeed < origXRotation + maxXRotation.y) ||
                   camTrans.rotation.eulerAngles.x + InputManager.instance.cameraVertical * verticalRotSpeed > 360 + maxXRotation.x + origXRotation))
            {
                camTrans.RotateAround(playerHead, camTrans.right, InputManager.instance.cameraVertical * horizontalRotSpeed);
            }
        }
         else if (origXRotation != camTrans.rotation.eulerAngles.x && playerRig.velocity.magnitude>0.1)
         {
             if (camTrans.rotation.eulerAngles.x < origXRotation + maxXRotation.y)
             {
                 camTrans.RotateAround(playerHead, camTrans.right, (origXRotation - camTrans.rotation.eulerAngles.x) * XRotReturnSpeed);
            }
             else
             {
                 camTrans.RotateAround(playerHead, camTrans.right, (360 - camTrans.rotation.eulerAngles.x + origXRotation) * XRotReturnSpeed);
            }
        }
        camPointer = (camTrans.position - playerHead).normalized;
    }

    void Translate()
    {
        
        camPos = camTrans.position;
        Debug.DrawRay(playerHead, camPointer * maxDistance, Color.green, 5f);
        /* if (!Physics.Raycast(camTrans.position, -camTrans.forward, camBackwardsCheckRange + 4*camPushSpeed, camPushMask) && camOffSet.x + camPushSpeed >= camOffSetOrig.x)
         {
             camOffSet = new Vector3(camOffSet.x - camPushSpeed, camOffSet.y, camOffSet.z + camPushSpeed);
         }
         if (Physics.Raycast(camTrans.position, -camTrans.forward , out camBackWall, camBackwardsCheckRange, camPushMask) && camOffSet.x - camPushSpeed <=0)
         {
             if ((camBackWall.point - camTrans.position).magnitude > minCamWallDist)
             { 
                 camOffSet = new Vector3(camOffSet.x + camPushSpeed, camOffSet.y, camOffSet.z - camPushSpeed);
             }
             else
             {
                 camOffSet = new Vector3(camOffSet.x + minCamWallDist - (camBackWall.point - camTrans.position).magnitude, camOffSet.y, camOffSet.z);
             }
         }
         camTrans.position = player.transform.position + camOffSet.x * camTrans.forward + camOffSet.y * camTrans.up + camOffSet.z * camTrans.right;*/
        /*colls = Physics.OverlapSphere(camPos, minCamWallDist, camPushMask); // overlapsphere um alle collider innerhalb der no-comfort-zone der cam zu erfassen
        if (colls.Length > 0) {
            Physics.Raycast(camPos, colls[0].ClosestPoint(camPos) - camPos, out camBackWall, (colls[0].ClosestPoint(camPos) - camPos).magnitude, camPushMask); //colls[0] sollte der zuerst erfasste und damit naheliegendste collider sein
            distanceVectorDelta = Vector3.Dot(camBackWall.normal * minCamWallDist, camTrans.position - camBackWall.point);
            cameraHeadVectorDelta = Vector3.Dot(camBackWall.normal * minCamWallDist * distanceVectorDelta, playerHead - camTrans.position);
            camOffSet += cameraHeadVectorDelta * (playerHead - camTrans.position);
            print((playerHead - camTrans.position));
            print("closing in at " + cameraHeadVectorDelta * (playerHead - camTrans.position));
        }
        else
        {
            colls = Physics.OverlapSphere(camPos, minCamWallDist + camPushSpeed, camPushMask);
            if (colls.Length == 0) { 
            if (camOffSet.x - camPushSpeed* XZRatio >= camOffSetOrig.x) { 
            camOffSet = new Vector3(camOffSet.x - camPushSpeed*XZRatio, camOffSet.y, camOffSet.z + camPushSpeed * (1/XZRatio));
            print("returning");
            }
            }

        }*/
        if (Physics.Linecast(playerHead, playerHead + camPointer * maxDistance, out camBackWall, camPushMask))
        {
            print("closer" + (camBackWall.distance));
            camDistance = Mathf.Clamp(camBackWall.distance - minCamWallDist, 0, maxDistance);
        }
        else
        {
            camDistance = maxDistance;
        }
        print(camDistance);
        camTrans.position = Vector3.Lerp(camTrans.position,playerHead + camPointer * camDistance, camMoveSmooth);
    }

}
