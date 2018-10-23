using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour {
    
    public float thrustSpeed = 6000f, turnSpeed = 1500f, hoverSpeed = 10f;
    public bool weaponized;
    public GameObject laserBullet;

    private float thrustInput, turnInput, weaponCD = 0.0f, cooldown = 0.2f;
    private bool hover;
    private Rigidbody shipRigidBody;

    // Use this for initialization
    void Start () {
        weaponized = true;
        hover = false;
        shipRigidBody = GetComponent<Rigidbody>();

    }
	
	// Update is called once per frame
	void Update () {
        thrustInput = Input.GetAxis("Vertical");
        turnInput = Input.GetAxis("Horizontal");
    }

    void FixedUpdate() {
        Shoot();
        Movement();
    }

    void Movement()
    {
        // Turning the ship
        shipRigidBody.AddRelativeTorque(0f, turnInput * turnSpeed, 0f);
        // Brake and gas
        if (Input.GetKey(KeyCode.Space))
        {
            shipRigidBody.drag = 4;
        }            
        else {
            shipRigidBody.drag = 2;
            shipRigidBody.AddRelativeForce(0f, 0f, thrustInput * thrustSpeed);
        }
    }

    void Hover() {
        //GetComponent<Rigidbody>().AddForce(Vector3.up * hoverSpeed / transform.position.y, ForceMode.Acceleration);
    }

    void Shoot()
    {
        if (Input.GetKey(KeyCode.LeftShift) && weaponized && weaponCD <= Time.time)
        {
            var bul = Instantiate(laserBullet, transform.position, transform.rotation);
            bul.GetComponent<Rigidbody>().AddForce(transform.forward * 2000);
            weaponCD = Time.time + cooldown;
        }
    }
}
