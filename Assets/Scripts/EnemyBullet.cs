using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour {

    private float lifetime = 3.0f;
    private float spawnTime;

    // Use this for initialization
    void Start() {
        spawnTime = Time.time;
    }

    // Update is called once per frame
    void Update() {
        if (Time.time > spawnTime + lifetime) {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.tag == "Player")
        {
            // other.hp -= 1;
            Destroy(gameObject);
        }
    }
}
