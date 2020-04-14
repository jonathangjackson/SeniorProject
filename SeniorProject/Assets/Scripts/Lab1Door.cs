using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lab1Door : MonoBehaviour
{
    Animator animator1;
    AudioSource audio1;
    public AudioSource doorOpen;
    public DeltePowerCell power;

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
        if (this.enabled)
        {
            if (collider.gameObject.tag == "Player")
            {
                if (power.generatorPower == true)
                {
                    audio1.PlayOneShot(audio1.clip);
                    doorOpen.Play();

                    animator1.SetTrigger("Open");
                    animator1.ResetTrigger("Close");
                }
            }
        }
    }

    void OnTriggerExit(Collider collider)
    {
        if (this.enabled)
        {
            animator1.SetTrigger("Close");
            animator1.ResetTrigger("Open");
            audio1.Play();
        }
    }
}
