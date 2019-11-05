using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floating : MonoBehaviour
{

    public Rigidbody rb;
    public float speed;
    public float randomRotationStrength;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(randomRotationStrength, randomRotationStrength, randomRotationStrength);
         rb.useGravity = false;

        void OnCollisionEnter(Collision col)
        {
            if (col.gameObject.name == "Roof")
            {
                rb.AddRelativeForce(Vector3.down * speed);
                rb.AddRelativeForce(Vector3.up * 0);
            }

            else if (col.gameObject.name == "Wall Left")
            {
                rb.AddRelativeForce(Vector3.right * speed);
                rb.AddRelativeForce(Vector3.left * 0);
            }

            else if (col.gameObject.name == "Wall Right")
            {
                rb.AddRelativeForce(Vector3.left * speed);
                rb.AddRelativeForce(Vector3.right * 0);
            }

            else if (col.gameObject.name == "Wall Back")
            {
                rb.AddRelativeForce(Vector3.back * speed);
                rb.AddRelativeForce(Vector3.forward * 0);
            }

            else if (col.gameObject.name == "Wall Front")
            {
                rb.AddRelativeForce(Vector3.forward * speed);
                rb.AddRelativeForce(Vector3.back * 0);
            }

            else if (col.gameObject.name == "Ground")
            {
                rb.AddRelativeForce(Vector3.up * speed);
                rb.AddRelativeForce(Vector3.down * 0);
            }

        }
    }

    
}
