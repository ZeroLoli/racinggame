using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{

    [SerializeField]
    Vector3 rotationAxis = new Vector3(1f, 0f, 0f);
    [SerializeField]
    float speed = 360f;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(rotationAxis.normalized, speed * Time.deltaTime, Space.Self);
    }
}
