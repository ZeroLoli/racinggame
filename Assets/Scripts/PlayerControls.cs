using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour {

    public float thrustSpeed = 15000f;
    public float turnSpeed = 1000f;
    public float hoverSpeed = 10f;
    public bool isWeaponized;
    public GameObject laserBullet;

    private float thrustInput;
    private float turnInput;
    private float weaponCD = 0.0f;
    private float cooldown = 0.2f;
    private Rigidbody shipRigidbody;

    // Use this for initialization
    void Start () {
        isWeaponized = true;
        shipRigidbody = GetComponent<Rigidbody>();

    }
	
	// Update is called once per frame
	void Update () {
        thrustInput = Input.GetAxis("Vertical");
        turnInput = Input.GetAxis("Horizontal");
        Shoot();  
	}

    void FixedUpdate()
    {
        Movement();
    }

    void Movement()
    {
        // Turning the ship
        shipRigidbody.AddRelativeTorque(0f, turnInput * turnSpeed, 0f);
        // Brake and gas
        if (Input.GetKey(KeyCode.Space))
        {
            shipRigidbody.drag = 4;
        }            
        else {
            shipRigidbody.drag = 2;
            shipRigidbody.AddRelativeForce(0f, 0f, thrustInput * thrustSpeed);
        }
    }

    void CollMovement()
    {

    }

    void Hover() {
        //GetComponent<Rigidbody>().AddForce(Vector3.up * hoverSpeed / transform.position.y, ForceMode.Acceleration);
    }

    void Shoot()
    {
        if (Input.GetKey(KeyCode.LeftShift) && isWeaponized && weaponCD <= Time.time)
        {
            var bul = Instantiate(laserBullet, transform.position, transform.rotation);
            bul.GetComponent<Rigidbody>().AddForce(transform.forward * 600);
            weaponCD = Time.time + cooldown;
        }
    }
}
