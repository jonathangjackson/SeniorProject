using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class thermalVision : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            this.GetComponent<ParticleSystem>().Clear();
            this.GetComponent<ParticleSystem>().Stop();
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            this.GetComponent<ParticleSystem>().Play();
        }
    }
}
