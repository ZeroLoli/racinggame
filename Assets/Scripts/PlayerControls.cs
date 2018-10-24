using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour {

    // Public vars for movement
    public float thrustSpeed = 6000f;
    public float turnSpeed = 1500f;
    // Public vars for shooting
    public bool weaponized;
    public float weaponCooldownDur = 0.2f;
    public GameObject laserBullet;

    // Private vars for movement
    private Rigidbody shipRigidBody;
    private float thrustInput;
    private float turnInput;
    // Private vars for shooting
    private float weaponShotTime;
    private float hitPoints;
    
    

    // Use this for initialization
    void Start () {
        weaponShotTime = 0.0f;
        hitPoints = 10.0f;
        shipRigidBody = GetComponent<Rigidbody>();
    }
	
	// Update is called once per frame
	void Update () {
        thrustInput = Input.GetAxis("Vertical");
        turnInput = Input.GetAxis("Horizontal");
        if (hitPoints <= 0)
        {

        }
    }
    
    void FixedUpdate() {
        //if () {
            Movement();
        
            if (weaponized) {
                Shoot();
            }
        //}
    }

    // Contains both ship's movement, both automatic and player-controlled
    void Movement()
    {
        // Turning the ship
        shipRigidBody.AddRelativeTorque(0f, turnInput * turnSpeed, 0f);
        // Moving the ship
        shipRigidBody.AddRelativeForce(0f, 0f, thrustInput * thrustSpeed);
        /* Brake, if we want it:
         * if (Input.GetKey(KeyCode.Space))
        {
            shipRigidBody.drag = 4;
        }            
        else {
            shipRigidBody.drag = 2;
            shipRigidBody.AddRelativeForce(0f, 0f, thrustInput * thrustSpeed);
        }*/

        // Rotate back to 0 on Z-axis
        transform.rotation = Quaternion.Slerp(transform.rotation,
            Quaternion.Euler(0.0f,
            transform.localEulerAngles.y, 0), Time.deltaTime * 2.0f);

    }

    // Shooting bullets from the ship
    void Shoot() {
        if (Input.GetKey(KeyCode.LeftShift) && weaponShotTime <= Time.time)
        {
            var bul = Instantiate(laserBullet, transform.position + transform.forward, transform.rotation);
            bul.GetComponent<Rigidbody>().AddForce(transform.forward * 2000);
            weaponShotTime = Time.time + weaponCooldownDur;
        }
    }
}
