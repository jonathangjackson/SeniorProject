using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlAnimations : MonoBehaviour
{
    public Animator animatorController;

    public bool triggerCollision = false;
    public bool externalCollision = false;

    public AudioSource doorOpening;

    // Start is called before the first frame update
    void Start()
    {
        if (animatorController == null && triggerCollision)
            animatorController = this.GetComponent<Animator>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player" && triggerCollision)
        {
            animatorController.SetBool("Play", true);
            doorOpening.Play();
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
