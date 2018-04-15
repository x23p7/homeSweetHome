using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomCam : MonoBehaviour
{
    public Camera currentCam;
    public Vector3 offSet;
    [Range(0f, 1f)]
    public float cameraRollSpeed;
    [Range(0f, 1f)]
    public float scrollSpeed;
    public LayerMask selection;
    public string roomTag;
    Transform camTrans;
    GameObject target;
    GameObject targetPlayer;
    Vector3 desiredPos;
    Vector3 origPos;
    RaycastHit roomHit;
    float scrollDelta;
    Vector3 toPlayerVector;
    private void Start()
    {
        origPos = currentCam.transform.position;
        desiredPos = origPos;
    }
    void FixedUpdate()
    {
        camTrans = currentCam.transform;
        if (camTrans.position != desiredPos)
        {
            camTrans.position = Vector3.Lerp(camTrans.position, desiredPos, cameraRollSpeed);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (Physics.Raycast(currentCam.ScreenPointToRay(Input.mousePosition), out roomHit))
            {
                target = roomHit.transform.parent.gameObject;
                if (targetPlayer != null)
                {
                    targetPlayer.GetComponent<PlayerMove>().enabled = false;
                }
                targetPlayer = target.transform.Find("Player").gameObject;
                targetPlayer.GetComponent<PlayerMove>().enabled = true;
                if (roomHit.transform.parent.gameObject.CompareTag(roomTag))
                {
                    desiredPos = target.transform.position + offSet.x * target.transform.forward + offSet.z * target.transform.right + offSet.y * target.transform.up;
                }
            }
        }
        scrollDelta = Input.GetAxis("Mouse ScrollWheel") / Mathf.Abs(Input.GetAxis("Mouse ScrollWheel"));
        if (Mathf.Abs(scrollDelta) > 0)
        {
            /*if (scrollDelta > 0 &&
                 target != null &&
                desiredPos != target.transform.position + offSet.x * target.transform.forward + offSet.z * target.transform.right + offSet.y * target.transform.up)
            {
                desiredPos = currentCam.transform.position + ( target.transform.position + offSet.x * target.transform.forward + offSet.z * target.transform.right + offSet.y * target.transform.up - origPos) * scrollSpeed;
            }*/
            if (scrollDelta < 0 &&
                 target != null &&
                Vector3.Dot(origPos - currentCam.transform.position , origPos + currentCam.transform.forward) > 0)
            {
                targetPlayer.GetComponent<PlayerMove>().enabled = false;
                desiredPos = currentCam.transform.position + (origPos - target.transform.position + offSet.x * target.transform.forward + offSet.z * target.transform.right + offSet.y * target.transform.up) * scrollSpeed;
            }
        }
    }
}
