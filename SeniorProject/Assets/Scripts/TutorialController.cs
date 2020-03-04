using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialController : MonoBehaviour
{
    public TutorialManager tm;
    public bool parentThis = false;
    private bool currentParent = false;
    public bool buttonTrigger = false;
    void Update()
    {
        if (parentThis != currentParent)
        {
            callTutorialManagerParent(parentThis);
            currentParent = parentThis;

        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "tutorial")
        {
            if (this.name == "TutorialTrigger2")
            {
                Debug.Log("Trigger2");
                parentThis = true;
                Debug.Log(parentThis != currentParent);
            }
            tm.triggerNext();
            this.GetComponent<BoxCollider>().enabled = false;
        }
    }

    public void callTutorialManagerParent(bool value)
    {
        Debug.Log("Start Animation");
        tm.parentDuringAnim(value);
    }
}
