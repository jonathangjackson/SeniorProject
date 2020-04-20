using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratorButton1 : MonoBehaviour
{
    public bool active = false;
    public Animator animController;

    public AudioSource buttonPressSound;
    public DeltePowerCell power;
    public GameObject tube;

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
        if ((other.tag == "Hand" || other.tag == "Claw") && !active && power.generatorPower == true)
        {
            buttonPressSound.Play();
            active = true;
            animController.SetBool("Play", true);
            tube.GetComponent<Renderer>().material.EnableKeyword("_EMISSION");
        }
    }


    public void reset()
    {
        tube.GetComponent<Renderer>().material.DisableKeyword("_EMISSION");
        active = false;
        Debug.Log(this.gameObject.name + "Button With Error");
        animController.SetBool("Play", false);
    }

}
