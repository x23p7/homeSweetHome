using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    public Camera currentCam;
    public GameObject player;
    public float rotationSpeed = 3;

    playerMovement playerMoveScript;
    Transform camTrans;
	// Use this for initialization
	void Start () {
        playerMoveScript = player.GetComponent<playerMovement>();
        camTrans = currentCam.transform;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (Input.GetKey(KeyCode.Q))
        {
            currentCam.transform.RotateAround(player.transform.position, Vector3.up, -rotationSpeed);
            playerMoveScript.currentForward = new Vector3(camTrans.forward.x, 0, camTrans.forward.z).normalized;
        }
        else if (Input.GetKey(KeyCode.E))
        {
            currentCam.transform.RotateAround(player.transform.position, Vector3.up, rotationSpeed);
            playerMoveScript.currentForward = new Vector3(camTrans.forward.x, 0, camTrans.forward.z).normalized;
        }
    }
}
