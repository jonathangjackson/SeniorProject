using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Float : MonoBehaviour
{

    public float FloatStrength;
    public float randomRotationStrength;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < GameObject.Find("Cube (16)").transform.position.y)
        {
            Debug.Log("force being applied upwards to move object up");
            transform.GetComponent<Rigidbody>().AddForce(Vector3.up * FloatStrength);
            transform.Rotate(randomRotationStrength, randomRotationStrength, randomRotationStrength);
        }
        if (transform.position.y >= GameObject.Find("Cube (16)").transform.position.y)
        {
            Debug.Log("force applied is less than the gravitational force so that the object comes down. Here mass of object is 2.  ");
            transform.GetComponent<Rigidbody>().AddForce(Vector3.up * 11.0f);
            transform.Rotate(randomRotationStrength, randomRotationStrength, randomRotationStrength);
        }
    }
}
