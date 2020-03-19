using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever3 : MonoBehaviour
{
    public GameObject lever3;

    public GameObject Door3;
    public GameObject Doorthree;

    public GameObject Door2;
    public GameObject Doortwo;

    Animator animator3;
    AudioSource audio3;
    Animator animatorthree;
    AudioSource audiothree;

    Animator animator2;
    AudioSource audio2;
    Animator animatortwo;
    AudioSource audiotwo;

    public bool lever2active;
    bool lever3active;

    // Start is called before the first frame update
    void Start()
    {
        animator3 = Door3.GetComponent<Animator>();
        audio3 = Door3.GetComponent<AudioSource>();

        animatorthree = Doorthree.GetComponent<Animator>();
        audiothree = Doorthree.GetComponent<AudioSource>();

        animator2 = Door2.GetComponent<Animator>();
        audio2 = Door2.GetComponent<AudioSource>();

        animatortwo = Doortwo.GetComponent<Animator>();
        audiotwo = Doortwo.GetComponent<AudioSource>();

        lever3active = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider collider)
    {
        if (lever2active == false)
        {
            if (collider.gameObject.name == "Hand")
            {
                audio3.PlayOneShot(audio3.clip);

                animator3.Play("Door_Open");


                audiothree.PlayOneShot(audiothree.clip);
           
                animatorthree.Play("Door_Open");


                animator2.Play("Door_Close");
                audio2.Play();

                animatortwo.Play("Door_Close");
                audiotwo.Play();

                lever3active = true;
            }
        }
    }

    void OnTriggerExit(Collider collider)
    {
        lever3active = false;
    }
}
