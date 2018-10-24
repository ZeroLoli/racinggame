using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTower : MonoBehaviour {

    public GameObject playerShip;
    public GameObject sparkBullet;

    public float minAimAngle;
    public float maxAimAngle;
    public float cooldownTime;
    public float bulletVelocity;
    private float distanceToPlayer;
    private float shootingDistance;
    private float shotCooldown;
    private float aimDamping;
    private float hitPoints;
    private bool isActive;

    // Use this for initialization
    void Start () {
        isActive = true;
        shootingDistance = 50.0f;
        aimDamping = 2.0f;
        hitPoints = 3.0f;
	}
	
	// Update is called once per frame
	void Update () {
        if (isActive) { 
            distanceToPlayer = Vector3.Distance(transform.position, playerShip.transform.position);
            if (distanceToPlayer <= shootingDistance) {
                Laser();
            }
        }
	}

    void Aim() {
        // transform.LookAt(playerShip.transform);
        Quaternion rotation = Quaternion.LookRotation(playerShip.transform.position - transform.position /* + (playerShip.transform.forward * playerShip.transform.GetComponent<Rigidbody>().velocity.z)*/);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * aimDamping);
        if (shotCooldown <= Time.time) {
            var bul = Instantiate(sparkBullet, transform.position, transform.rotation);
            bul.GetComponent<Rigidbody>().AddForce(transform.forward * 2000);
            shotCooldown = Time.time + cooldownTime;
        }
    }

    void Laser() {
        Quaternion rotation = Quaternion.LookRotation(playerShip.transform.position - transform.position /* + (playerShip.transform.forward * playerShip.transform.GetComponent<Rigidbody>().velocity.z)*/);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * aimDamping);
        transform.rotation = Quaternion.Euler(Mathf.Clamp(transform.rotation.eulerAngles.x, minAimAngle, maxAimAngle), transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
    }

    void OnTriggerEnter (Collider other)
    {
        if (other.tag == "Player")
        {
            
        }
    }
}
