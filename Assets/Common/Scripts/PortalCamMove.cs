using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalCamMove : MonoBehaviour {
    Transform playerCam;
    public Transform portal;
    Transform targetPoint;
    public LoadPortalScene loadingScript;
    // Use this for initialization
    void Start()
    {
        this.gameObject.SetActive(false);
        playerCam = GlobalGameStateManager.instance.playerCam.transform;
    }
    private void OnEnable()
    {
       targetPoint = loadingScript.targetPoint;
    }
    // Update is called once per frame
    void FixedUpdate() {
        if (targetPoint != null) { 
        Vector3 playerOffSetFromPortal = playerCam.position - portal.position;
        transform.position = targetPoint.position + playerOffSetFromPortal + (portal.transform.position.y - GlobalGameStateManager.instance.player.transform.position.y + GlobalGameStateManager.instance.player.transform.localScale.y/2) * Vector3.up;
        float angularDifference = Quaternion.Angle(targetPoint.parent.rotation, portal.parent.rotation) -180;
        Quaternion rotationalDifference = Quaternion.AngleAxis(angularDifference, Vector3.up);
        Vector3 newCamDir = rotationalDifference * playerCam.forward;
        transform.rotation = Quaternion.LookRotation(newCamDir, Vector3.up);
        }
    }
}
