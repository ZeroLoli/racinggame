using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour {

    public int checkpointNo;
    public bool currCP = false;
    private CheckpointController CPControl;
    private Light spotlight;

	// Use this for initialization
	void Start () {
        spotlight = GetComponentInChildren<Light>();
        CPControl = transform.GetComponentInParent<CheckpointController>();
    }
	
	// Update is called once per frame
	void Update () {
        if (currCP) {
            spotlight.color = Color.red;
            spotlight.intensity = 4 + Mathf.Sin(Time.time * 2);
        }
        else {
            spotlight.intensity -= 0.1f;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (currCP && (other.name == "Player")) {
            CPControl.CPIncrease();
            spotlight.intensity = 8.0f;
            spotlight.color = Color.green;
            currCP = false;
        }
    }
}
