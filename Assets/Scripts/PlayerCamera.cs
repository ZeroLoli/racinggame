using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour {

    public GameObject playerShip;

    private float height = 4.0f;
    private float distance = 8.0f;
    private float followDamping = 8f;
    private float rotationDamping = 100.0f;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        transform.position = Vector3.Lerp(transform.position, playerShip.transform.TransformPoint(0f, height, -distance), Time.deltaTime * followDamping);
        // transform.position = playerShip.transform.position + new Vector3(0f, height, distance * playerShip.transform.forward.z);
        Quaternion rotation = Quaternion.LookRotation(playerShip.transform.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * rotationDamping);
    }
}
