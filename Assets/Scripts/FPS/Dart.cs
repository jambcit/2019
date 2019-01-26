using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dart : MonoBehaviour
{

    private const float DART_VELOCITY = 30f;

    private Rigidbody rigidBody;
    private Vector3 startPosition;
    private Vector3 startDirection;
    private bool isStuck;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Dart Start");
        rigidBody = GetComponent<Rigidbody>();
        rigidBody.AddForce(transform.up * DART_VELOCITY, ForceMode.Impulse);
        startPosition = transform.position;
        startDirection = transform.up;
        isStuck = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collided");
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Entered trigger");
        if (other.GetComponent<StickyObject>() != null)
        {
            isStuck = true;
            transform.parent = other.transform.parent;
            rigidBody.isKinematic = true;
        }
    }


}
