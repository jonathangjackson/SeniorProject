using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialController : MonoBehaviour
{
    public TutorialManager tm;
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "tutorial")
        {
            tm.triggerNext();
            Destroy(this);
        }
    }
}
