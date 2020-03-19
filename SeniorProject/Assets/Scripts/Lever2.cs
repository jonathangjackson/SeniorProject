using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever2 : MonoBehaviour
{
    public GameObject lever2;


    public GameObject Door2;
    public GameObject Doortwo;

    public GameObject Door3;
    public GameObject Doorthree;

    Animator animator2;
    AudioSource audio2;
    Animator animatortwo;
    AudioSource audiotwo;

    Animator animator3;
    AudioSource audio3;
    Animator animatorthree;
    AudioSource audiothree;

    public bool lever3active;
    bool lever2active;

    // Start is called before the first frame update
    void Start()
    {
        animator2 = Door2.GetComponent<Animator>();
        audio2 = Door2.GetComponent<AudioSource>();

        animatortwo = Doortwo.GetComponent<Animator>();
        audiotwo = Doortwo.GetComponent<AudioSource>();

        animator3 = Door3.GetComponent<Animator>();
        audio3 = Door3.GetComponent<AudioSource>();

        animatorthree = Doorthree.GetComponent<Animator>();
        audiothree = Doorthree.GetComponent<AudioSource>();

        lever2active = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider collider)
    {
        if (lever3active == false)
        {
            if (collider.gameObject.name == "Hand")
            {
                if (lever3active == false)
                {
                    audio2.PlayOneShot(audio2.clip);

                    animator2.Play("Door_Open");


                    audiotwo.PlayOneShot(audiotwo.clip);

                    animatortwo.Play("Door_Open");


                    animator3.Play("Door_Close");
                    audio3.Play();

                    animatorthree.Play("Door_Close");
                    audiothree.Play();

                    lever2active = true;
                }
            }
        }
    }

    void OnTriggerExit(Collider collider)
    {
        lever2active = false;
    }
}
