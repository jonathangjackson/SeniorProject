using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lab2Doors : MonoBehaviour
{

    Animator animator2;
    AudioSource audio2;


    public SVLever2 lever2;

    // Start is called before the first frame update
    void Start()
    {
        animator2 = GetComponent<Animator>();
        audio2 = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider collider)
    {
        if (lever2.leverIsOn == true)
        {
            if (collider.gameObject.name == "Player")
            {
                audio2.PlayOneShot(audio2.clip);

                animator2.Play("Door_Open");
            }
        }
    }

    void OnTriggerExit(Collider collider)
    {
        animator2.Play("Door_Close");
        audio2.Play();
    }
}
