using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTower : MonoBehaviour {

    public GameObject playerShip;
    public GameObject sparkBullet;

    public float minYAngle;
    public float maxYAngle;
    public float cooldownTime;
    public float bulletVelocity;
    private float distanceToPlayer;
    private float shootingDistance;
    private float shotCooldown;
    private float aimDamping;

    // Use this for initialization
    void Start () {
        shootingDistance = 50.0f;
        aimDamping = 2.0f;
	}
	
	// Update is called once per frame
	void Update () {
        distanceToPlayer = Vector3.Distance(transform.position, playerShip.transform.position);
        if (distanceToPlayer <= shootingDistance) {
            Aim();
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
}
