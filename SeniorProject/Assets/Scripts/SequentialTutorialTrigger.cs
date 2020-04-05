using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SequentialTutorialTrigger : MonoBehaviour
{
    public enum TriggerType
    {
        Collision,
        Controller,
        UIButton,
        Animation
    }

    public TriggerType triggerType;
    public string collisionTag;
    public bool triggered = false;

    public void setActive()
    {
        this.gameObject.SetActive(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(triggerType == TriggerType.Collision)
        {
            if (other.tag == collisionTag)
                triggered = true;
        }
    }

}
