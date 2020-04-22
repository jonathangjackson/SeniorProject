using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeltePowerCell : MonoBehaviour
{
    public GameObject powerCell;
    public List<Animator> animController = new List<Animator>();
    public bool generatorPower;
    public Animator labDoor;
    public AudioSource generatorOnSound;

    void Start()
    {
        generatorPower = false;
    }

    private void Update()
    {
       
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "PowerCell")
        {
            other.gameObject.SetActive(false);
            powerCell.SetActive(true);
            generatorPower = true;
            generatorOnSound.Play();
            for(int i = 0; i < animController.Count; i++)
            {
                animController[i].SetBool("Play", true);
            }
            labDoor.SetBool("Locked", false);
        }
    }
}
