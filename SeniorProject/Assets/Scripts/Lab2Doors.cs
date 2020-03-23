using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lab2Doors : MonoBehaviour
{

    Animator animator2;
    AudioSource audio2;


    public GeneratorButton2 button2;

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
        if (button2.active == true)
        {
            if (collider.gameObject.tag == "Player")
            {
                audio2.PlayOneShot(audio2.clip);

                animator2.SetTrigger("Open");
                animator2.ResetTrigger("Close");
            }
        }
    }

    void OnTriggerExit(Collider collider)
    {
        if (button2.active == true)
        {
            animator2.SetTrigger("Close");
            animator2.ResetTrigger("Open");
            audio2.Play();
        }
    }
}
