﻿using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour {

    // Public vars for movement
    [SerializeField]
    float thrustSpeed = 6000f;
    [SerializeField]
    float turnSpeed = 1500f;
    public bool isBoosted;
    // Public vars for shooting
    [SerializeField]
    bool weaponized;
    [SerializeField]
    float weaponCooldownDur = 0.2f;
    [SerializeField]
    GameObject laserBullet;

    [SerializeField]
    GameObject[] trailObjects;
    [SerializeField]
    float minimumSpeedForTrails = 2.5f;

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

        // Check current speed. If speed is high, enable trail effect.
        if (shipRigidBody.velocity.magnitude > minimumSpeedForTrails)
            SetTrailsEnabled(true);
        else
            SetTrailsEnabled(false);

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

    void SetTrailsEnabled(bool areTrailsEnabled)
    {
        for ( int i = 0; i < trailObjects.Length; i++)
        {
            TrailRenderer trail = trailObjects[i].GetComponent<TrailRenderer>();
            if (trail != null)
            {
                trail.emitting = areTrailsEnabled;
            }

        }
    }

    public void TurboBoost() {
        if (!isBoosted) {
            thrustSpeed = 10000;
            isBoosted = !isBoosted;
        }
    }
}
