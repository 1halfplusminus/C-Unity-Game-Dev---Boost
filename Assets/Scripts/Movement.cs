using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour, IUseInput, IUsePhysics
{
    
    private Vector3 inputDirection;
    Vector3 IUseInput.inputDirection { get => inputDirection; set => inputDirection = value; }
    new Rigidbody rigidbody;
    Rigidbody IUsePhysics.rigidbody => rigidbody;

    public Vector3 speed;


    public
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        this.UpdateInput();
    }
    void FixedUpdate() {
        ProcessThrust();
        ProcessRotate();
    }
    void ProcessThrust() {
        rigidbody.AddRelativeForce(inputDirection * speed.x * Time.deltaTime);
    }

    void ProcessRotate() {
        rigidbody.freezeRotation = true; // Blocking the rigidbody rotation
        transform.Rotate(new Vector3(0,0,inputDirection.z* speed.z * Time.deltaTime));
        rigidbody.freezeRotation = false;
    }
}
