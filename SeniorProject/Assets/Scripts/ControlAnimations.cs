using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlAnimations : MonoBehaviour
{
    public Animator animatorController;
    public bool triggerCollision = false;
    public bool externalCollision = false;
    public bool lockOff = false;
    public AudioSource doorSound;

    // Start is called before the first frame update
    void Start()
    {
        if (animatorController == null && triggerCollision)
            animatorController = this.GetComponent<Animator>();
        if (lockOff)
            animatorController.SetBool("Locked", false);
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player" && triggerCollision)
        {
            animatorController.SetBool("Play", true);
            doorSound.Play();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player" && triggerCollision)
        {
            animatorController.SetBool("Play", false);
        }
    }

    private void Update()
    {
        if (externalCollision)
        {
            animatorController.SetBool("Play", false);
        }
    }
}
