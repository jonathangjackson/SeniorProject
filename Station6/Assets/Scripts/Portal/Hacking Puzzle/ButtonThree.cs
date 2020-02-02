using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonThree : MonoBehaviour
{
    public GameObject greenLight, redLight;

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
        if (other.tag == "Player")
        {
            greenLight.SetActive(true);
            redLight.SetActive(false);
        }
    }
}
