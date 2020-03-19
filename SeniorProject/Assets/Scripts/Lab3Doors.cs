using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lab3Doors : MonoBehaviour
{
    Animator animator3;
    AudioSource audio3;


    public Lever3 lever3;

    // Start is called before the first frame update
    void Start()
    {
        animator3 = GetComponent<Animator>();
        audio3 = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider collider)
    {
        if (lever3.lever3active == true)
        {
            if (collider.gameObject.name == "Player")
            {
                audio3.PlayOneShot(audio3.clip);

                animator3.Play("Door_Open");
            }
        }
    }

    void OnTriggerExit(Collider collider)
    {
        animator3.Play("Door_Close");
        audio3.Play();
    }
}
