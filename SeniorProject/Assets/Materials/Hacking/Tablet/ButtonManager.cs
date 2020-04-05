using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    private LightManager light;
    public bool active = false;
    public int buttonID = 0;
    public HackingWorldManager hackingWorld;
    public Animator animController;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Claw" && !active)
        {
            active = true;
            light.lightOn();
            hackingWorld.buttonPressed(buttonID);
            animController.SetBool("Play", true);
        }
    }
    
    public void getLight(LightManager l)
    {
        light = l;
    }

    public void reset()
    {
        Debug.Log("LIGHT RESET: " + this.buttonID);
        active = false;
        light.lightOff();
        animController.SetBool("Play", false);
    }

    public string getLightName()
    {
        return light.name;
    }
}