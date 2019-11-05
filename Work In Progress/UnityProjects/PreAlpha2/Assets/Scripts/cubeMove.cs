using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OVR;

public class cubeMove : MonoBehaviour
{
    Vector3 current_position;
    Vector3 stopped_position;
    float direction = 1.0f;
    float speed = 9f;
    float heightlimit = 10f;
    float timecount = 0.0f;
    float timelimit = 0.0f;

    bool press = false;


    public float radius = 10.0f;

    void Start()
    {
        current_position = this.transform.position;
    }

    void Update()
    {

        transform.Translate(0, direction * speed * Time.deltaTime * 1, 0);


        if (transform.position.y > current_position.y + heightlimit)
        {
            direction = -1;
        }
        if (transform.position.y < current_position.y)
        {
            direction = 0;
            timecount = timecount + Time.deltaTime;

            if (timecount > timelimit)
            {
                direction = 1;
                timecount = 0;
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {   
        if (other.gameObject.CompareTag("Player"))
        {
            print("collided");
            Collider[] colliders = Physics.OverlapSphere(current_position, radius);
            foreach (Collider col in colliders)
            {
                if (col.tag == "Object")
                {
                    if (OVRInput.GetDown(OVRInput.Button.One))//Input.GetKeyDown(KeyCode.K)
                    {
                        speed = 0.0f;
                    }

                    if (OVRInput.GetUp(OVRInput.Button.One))//Input.GetKeyUp(KeyCode.K)
                    {
                        speed = 9f;
                    }
                }
            }
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            print("collided");
            Collider[] colliders = Physics.OverlapSphere(current_position, radius);
            foreach (Collider col in colliders)
            {
                if (col.tag == "Object")
                {
                    if (OVRInput.GetDown(OVRInput.Button.One))//Input.GetKeyDown(KeyCode.K)
                    {
                        speed = 0.0f;
                    }

                    if (OVRInput.GetUp(OVRInput.Button.One))//Input.GetKeyUp(KeyCode.K)
                    {
                        speed = 9f;
                    }
                }
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        speed = 9f;
    }
}
