﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour {
    public Camera currentCam;
    public float speedForce = 750;
    public float rotationSpeed = 3;
    public float maxSpeed = 5;
    public bool kickStart;
    public float kickStartFactor = 3;
    [Range (0f,1f)]
    public float velocityRotationFollow = 0.1f;
    Quaternion desiredRot;

    Vector3 currentVelocity;
    [HideInInspector]
    public Vector3 currentForward;
    Rigidbody myRig;
	// Use this for initialization
	void Start () {
        myRig = GetComponent<Rigidbody>();
        currentForward = transform.forward;
	}

    // Update is called once per frame
    void FixedUpdate() {
        currentVelocity = myRig.velocity;
        Move();
        RotateTowardsVelocity();

    }

    private void Move()
    {
        if (Mathf.Sign(InputManager.instance.verticalInput) != Mathf.Sign(Vector3.Dot(currentVelocity.normalized, currentForward)))
        { // keep this if you want crisp movement controls
            myRig.velocity = new Vector3(0, currentVelocity.y, 0);
        }
        if (currentVelocity.magnitude < maxSpeed / 10)
        {

            myRig.AddForce(currentForward * speedForce * InputManager.instance.verticalInput * kickStartFactor);

        }
        else if (currentVelocity.magnitude < maxSpeed)
        {
            myRig.AddForce(currentForward * speedForce * InputManager.instance.verticalInput);
        }
    }

    private void RotateTowardsVelocity()
    {
        print(Vector3.Lerp(transform.forward, myRig.velocity, velocityRotationFollow) != transform.forward);
        desiredRot.SetLookRotation(Vector3.Lerp(transform.forward, myRig.velocity, velocityRotationFollow));
        transform.rotation = desiredRot;
    }
}