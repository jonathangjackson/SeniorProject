using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratorButton2 : MonoBehaviour
{
    public GeneratorButton3 GeneratorButton3;
    public bool active = false;
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
        if ((other.tag == "Hand" || other.tag == "Claw") && !active)
        {
            buttonPressSound.Play();
            active = true;
            animController.SetBool("Play", true);
            if(GeneratorButton3.active == true)
            {
                GeneratorButton3.reset();
            }
        }
    }


    public void reset()
    {
        active = false;
        Debug.Log(this.gameObject.name + "Button With Error");
        animController.SetBool("Play", false);
    }

}
