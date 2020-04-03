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
    public AudioSource buttonPressSound;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Claw" && !active)
        {
            active = true;
            light.lightOn();
            buttonPressSound.Play();
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
        active = false;
        light.lightOff();
        Debug.Log(this.gameObject.name + "Button With Error");
        animController.SetBool("Play", false);
    }

    public string getLightName()
    {
        return light.name;
    }
}
