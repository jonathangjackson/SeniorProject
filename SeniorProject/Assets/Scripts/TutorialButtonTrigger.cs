using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialButtonTrigger : MonoBehaviour
{

    public bool active = false;
    public Animator animController;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Hand" && !active)
        {
            active = true;
            animController.SetBool("Play", true);
        }
    }
}
