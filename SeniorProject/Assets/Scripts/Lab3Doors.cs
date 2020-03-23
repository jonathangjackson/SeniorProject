using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lab3Doors : MonoBehaviour
{
    Animator animator3;
    AudioSource audio3;


    public GeneratorButton3 button3;

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
        if (button3.active == true)
        {
            if (collider.gameObject.tag == "Player")
            {
                audio3.PlayOneShot(audio3.clip);

                animator3.SetTrigger("Open");
                animator3.ResetTrigger("Close");
            }
        }
    }

    void OnTriggerExit(Collider collider)
    {
        if (button3.active == true)
        {
            animator3.SetTrigger("Close");
            animator3.ResetTrigger("Open");
            audio3.Play();
        }
    }
}
