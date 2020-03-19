using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever3 : MonoBehaviour
{
    public Lever2 lever2;
    public bool lever3active;

    // Start is called before the first frame update
    void Start()
    {
        lever3active = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider collider)
    {
        if (lever2.lever2active == false)
        {
            if (collider.gameObject.name == "Hand")
            {
                lever3active = true;
            }
        }
    }

    void OnTriggerExit(Collider collider)
    {
        lever3active = false;
    }
}
