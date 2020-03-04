using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonThree : MonoBehaviour
{
    public GameObject greenLight, redLight;

    public int counter = 0;

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
        counter += 1;

        if (other.tag == "Player" && counter % 2 == 1)
        {
            greenLight.SetActive(true);
            redLight.SetActive(false);
        }

        if (other.tag == "Player" && counter % 2 == 0)
        {
            greenLight.SetActive(false);
            //redLight.SetActive(false);
        }
    }
}
