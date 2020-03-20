using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lab1Door : MonoBehaviour
{
    Animator animator1;
    AudioSource audio1;

    // Start is called before the first frame update
    void Start()
    {
        animator1 = GetComponent<Animator>();
        audio1 = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.name == "Player")
        {
            audio1.PlayOneShot(audio1.clip);

            animator1.SetTrigger("Open");
            animator1.ResetTrigger("Reverse");
        }
    }

    void OnTriggerExit(Collider collider)
    {
        animator1.SetTrigger("Close");
        animator1.ResetTrigger("Open");
        audio1.Play();
    }
}
