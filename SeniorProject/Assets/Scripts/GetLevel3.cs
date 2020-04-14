using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetLevel3 : MonoBehaviour
{
    public AeviumLab3 lab;//lab03
    public AveiumInteraction interaction;//lab02
    //public GameObject hackingManager;
    public Animator animatorController;
    // Start is called before the first frame update
    public rotateIcon tooltip;
    public AudioSource Audio;

    private bool played = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //if(lab.Lab03Done && interaction.Lab02Done)
        //{
            //animatorController.SetBool("Play", true);
            //this.transform.GetChild(0).gameObject.SetActive(true);
            //this.enabled = false;
        //}
       // if (hackingManager.GetComponent<LoadHackingWorld>().isHacked)
        //{
        //    animatorController.SetBool("Play", true);
      //  }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (played == false && other.tag == "Player" && lab.Lab03Done && interaction.Lab02Done)
        {
            played = true;
            animatorController.SetBool("Play", true);
            Audio.Play();
            tooltip.infoText.text = "This is Aevium, the unstable material researchers on this ship were studying. Bring this material back to Station One for further research";
        }
    }
}
