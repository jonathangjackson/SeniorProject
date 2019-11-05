using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{

    float inputX;
    float inputZ;
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        inputX = Input.GetAxis("Horizontal") * speed;
        inputZ = Input.GetAxis("Vertical") * speed;

        if(inputX != 0)
        {
            rotateCamera();
        }

        if(inputZ != 0)
        {
            moveCamera();
        }

    }

    private void moveCamera()
    {
        transform.position += transform.forward * inputZ * Time.deltaTime;
    }

    private void rotateCamera()
    {
        transform.Rotate(new Vector3(0f, inputX * Time.deltaTime, 0f));

    }
}
