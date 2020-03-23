using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeltePowerCell : MonoBehaviour
{
    public GameObject powerCell;
    public List<Animator> animController = new List<Animator>();

    private void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "PowerCell")
        {
            other.gameObject.SetActive(false);
            powerCell.SetActive(true);
            for(int i = 0; i < animController.Count; i++)
            {
                animController[i].SetBool("Play", true);
            }
        }
    }
}
