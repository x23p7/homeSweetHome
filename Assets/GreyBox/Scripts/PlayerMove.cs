using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float moveForce;
    public Material activeMat;
    public Material inactiveMat;
    public Light myLight;
    Rigidbody myRig;
    // Use this for initialization
    void OnEnable()
    {
        myLight.enabled = true;
        GetComponent<MeshRenderer>().material = activeMat;
    }

    private void OnDisable()
    {
        myLight.enabled = false;
        GetComponent<MeshRenderer>().material = inactiveMat;
    }

    void Start()
    {
        enabled = false;
        myRig = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        /* if (Mathf.Abs(Input.GetAxis("Horizontal")) > 0)
         {
             myRig.AddForce(moveForce * Input.GetAxis("Horizontal") * Vector3.right);
         }
         if (Mathf.Abs(Input.GetAxis("Vertical")) > 0)
         {
             myRig.AddForce(moveForce * Input.GetAxis("Vertical") * Vector3.forward);
         }*/
        myRig.velocity = moveForce * (Input.GetAxis("Vertical") * Vector3.forward + Input.GetAxis("Horizontal") * Vector3.right);
    }
}
