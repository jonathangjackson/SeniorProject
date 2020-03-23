using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetLevel3 : MonoBehaviour
{
    public AeviumLab3 lab;//lab03
    public AveiumInteraction interaction;//lab02
    public GameObject hackingManager;
    public Animator animatorController;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(lab.Lab03Done && interaction.Lab02Done)
        {
            this.transform.GetChild(0).gameObject.SetActive(true);
            this.enabled = false;
        }
        if (hackingManager.GetComponent<LoadHackingWorld>().isHacked)
        {
            animatorController.SetBool("Play", true);
        }
    }
}
