using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverEngine : MonoBehaviour {

    private float hoverPower = 10.0f, hoverHeight = 1.0f;
    private Rigidbody engineBody;
	// Use this for initialization
	void Start () {
        //engineBody = GetComponent<Rigidbody>();
        engineBody = transform.parent.GetComponent<Rigidbody>();
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        Ray ray = new Ray(transform.position, -transform.up);
        RaycastHit hit;
        Debug.Log("vitun perse");
        if (Physics.Raycast(ray, out hit, hoverHeight))
        {
            float proportionalHeight = (hoverHeight - hit.distance) / hoverHeight;
            Vector3 appliedHoverForce = Vector3.up * proportionalHeight * hoverPower;
            engineBody.AddForce(appliedHoverForce, ForceMode.Acceleration);
        }
    }
}
