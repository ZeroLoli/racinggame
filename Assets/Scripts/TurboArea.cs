using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurboArea : MonoBehaviour {
    public GameObject player;
    public float turboAmount;
    public float turboDur;
    private float lastTurboAt;
    private bool isBoosting;

	// Use this for initialization
	void Start () {
        lastTurboAt = 0.0f;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void FixedUpdate () {
        if (isBoosting && Time.time - lastTurboAt <= turboDur)
        {
            player.GetComponent<Rigidbody>().AddForce(transform.forward * turboAmount);
        }
        else
        {
            isBoosting = false;
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (Time.time - lastTurboAt >= turboDur) {
            if (other.tag == "Player") {
                isBoosting = true;
                lastTurboAt = Time.time;
            }
        }
    }
}
