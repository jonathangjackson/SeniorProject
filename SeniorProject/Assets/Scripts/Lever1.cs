using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever1 : MonoBehaviour
{

    public GameObject lever1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.name == "Hand")
        {

        }
    }

    void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.name == "Hand")
        {
            
        }
    }
}
