using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour {

    public GameObject playerShip;

    private float height = 3.0f;
    private float distance = -5.0f;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = playerShip.transform.position + new Vector3(0f, height, distance * playerShip.transform.forward.z);
	}
}
