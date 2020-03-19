using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class DoorScript : MonoBehaviour
{

    Animator animator;
    AudioSource audio;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider collider)
    {
        if(collider.gameObject.name == "R_FootAim")
        {
            audio.PlayOneShot(audio.clip);

            animator.Play("Door_Open");
        }
    }

    void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.name == "R_FootAim")
        {
            animator.Play("Door_Close");
            audio.Play();
        }
    }
}
