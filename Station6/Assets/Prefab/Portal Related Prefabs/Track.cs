using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Track : MonoBehaviour
{
    //If the final position includes rotation
    public bool rotate;
    //If the final position includes translation
    public bool translate;
    //If the object snaps back to it's original position or not 
    public bool snapBack;


    //StartPosition
    public GameObject A;
    //End Position
    public GameObject B;

   // public Light GreenLight;
    public GameObject Greenlight;

    bool finalPos = false;

    // Start is called before the first frame update
    void Start()
    {

        this.gameObject.GetComponent<Rigidbody>().centerOfMass = Vector3.zero;

       // GreenLight.GetComponent<Light>().enabled = false;
        Greenlight.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        /*
         * 
         * 
         * 
         * 
         * 
         * 
         *  Yo Jon: 
         *   so I commented this part out temporarily cause it was not working.
         *   The issue is that the button will always be at a pressed in state at this scaling.
         *   
         *   
         *   
         *   
         *   
         * 
        if(this.transform.position.z > B.transform.position.z)
        {
            //this.GetComponent<Rigidbody>().isKinematic = true;
            this.transform.position = B.transform.position;
        }
        if((this.transform.rotation.z > A.transform.rotation.z || this.transform.rotation.z < B.transform.rotation.z) && rotate)//
        {
            this.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
            this.gameObject.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        }
        */

        if (Input.GetKey(KeyCode.A))
            Greenlight.SetActive(true);
        //    GreenLight.GetComponent<Light>().enabled = true;
        if (Input.GetKey(KeyCode.D))
            Greenlight.SetActive(false);
        //   GreenLight.GetComponent<Light>().enabled = false;
    }

    //returns finalPos value 
    public bool triggerHit()
    {
        return finalPos;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("HIT");
            finalPos = true;
            //GreenLight.GetComponent<Light>().enabled = true;
            Greenlight.SetActive(true);
        }      
    }

    
    private void OnTriggerStay(Collider other)
    {
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (snapBack)
        {
            if (rotate)
            {
                this.gameObject.GetComponent<Rigidbody>().velocity = -this.gameObject.GetComponent<Rigidbody>().velocity;
                this.gameObject.GetComponent<Rigidbody>().angularVelocity = -this.gameObject.GetComponent<Rigidbody>().angularVelocity;
                //this.transform.rotation = A.transform.rotation;
            }
            if (transform)
            {
                this.gameObject.GetComponent<Rigidbody>().velocity = -this.gameObject.GetComponent<Rigidbody>().velocity;
                this.gameObject.GetComponent<Rigidbody>().angularVelocity = -this.gameObject.GetComponent<Rigidbody>().angularVelocity;
            }
        }
    }
}
