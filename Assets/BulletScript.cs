﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour {

    private float lifetime = 1.0f, spawnTime;

	// Use this for initialization
	void Start () {
         spawnTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.time > spawnTime + lifetime) {
            Destroy(gameObject);
        }
	}

    private void OnTriggerEnter(Collider other) {
        Debug.Log("HIT!");
        // other.gameObject.hp -= 1;
        if (other.name != "Player") Destroy(gameObject);
    }
}
