using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController instance;
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
    [HideInInspector]
    public Vector3 playerHead;
    Transform camTrans;
    float distanceVectorDelta;
    float cameraHeadVectorDelta;
    [HideInInspector]
    public Vector3 camPointer;
    Collider[] colls;
    Vector3 camPos;
    float maxDistance;
    float camDistance;
    public float camSideCheckDist = 0.5f;
    Vector3 playerPos;
    List<float> rayHitDist;
    float centerDist;
    float lowestDist;
    public float pentaRayMoreThanLowest;
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
        rayHitDist = new List<float>();
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
        rayHitDist.Clear();
        centerDist = maxDistance;
        lowestDist = maxDistance;
        //new
        if (Physics.Linecast(playerHead, playerHead + camPointer * maxDistance, out camBackWall, camPushMask))
        {
            rayHitDist.Add(camBackWall.distance - minCamWallDist);
            centerDist = camBackWall.distance - minCamWallDist;
        }
        if (Physics.Linecast(playerHead, playerHead + camPointer * maxDistance + camTrans.right*camSideCheckDist, out camBackWall, camPushMask))
        {
            rayHitDist.Add(Mathf.Clamp(camBackWall.distance - minCamWallDist,0,centerDist));
        }
        if (Physics.Linecast(playerHead, playerHead + camPointer * maxDistance - camTrans.right * camSideCheckDist, out camBackWall, camPushMask))
        {
            rayHitDist.Add(Mathf.Clamp(camBackWall.distance - minCamWallDist, 0, centerDist));
        }
        if (Physics.Linecast(playerHead, playerHead + camPointer * maxDistance + camTrans.up * camSideCheckDist, out camBackWall, camPushMask))
        {
            rayHitDist.Add(Mathf.Clamp(camBackWall.distance - minCamWallDist, 0, centerDist));
        }
        if (Physics.Linecast(playerHead, playerHead + camPointer * maxDistance - camTrans.up * camSideCheckDist, out camBackWall, camPushMask))
        {
            rayHitDist.Add(Mathf.Clamp(camBackWall.distance - minCamWallDist, 0, centerDist));
        }

        if (rayHitDist.Count > 0)
        {
            camDistance = 0;
            for (int i = rayHitDist.Count; i > 0; i--)
            {
                lowestDist = Mathf.Min(lowestDist, rayHitDist[i - 1]);
                camDistance += rayHitDist[i - 1];
            }
            camDistance /= rayHitDist.Count;
            camDistance = Mathf.Clamp(camDistance, 0, lowestDist * pentaRayMoreThanLowest - minCamWallDist);
        }
        else
        {
            camDistance = Mathf.Lerp(camDistance,maxDistance,camMoveSmooth/5f);
        }
        //alt
        /*if (Physics.Linecast(playerHead, playerHead + camPointer * maxDistance, out camBackWall, camPushMask))
        {
            camDistance = Mathf.Clamp(camBackWall.distance - minCamWallDist, 0, maxDistance);
        }
        else
        {
            camDistance = maxDistance;
        }*/
        camTrans.position = Vector3.Lerp(camTrans.position,playerHead + camPointer * camDistance, camMoveSmooth);
    }
    }
