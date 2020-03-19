using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever2 : MonoBehaviour
{
    public Lever3 lever3;
    public bool lever2active;

    // Start is called before the first frame update
    void Start()
    {
        lever2active = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider collider)
    {
        if (lever3.lever3active == false)
        {
            if (collider.gameObject.name == "Hand")
            {
                lever2active = true;
            }
        }
    }

    void OnTriggerExit(Collider collider)
    {
        lever2active = false;
    }
}
