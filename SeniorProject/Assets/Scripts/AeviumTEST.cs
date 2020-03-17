using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AeviumTEST : MonoBehaviour
{
    public GameObject AeviumParticleSystem, AeviumDrip, AeviumDrip2, AeviumCube;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            AeviumParticleSystem.SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            AeviumParticleSystem.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            AeviumDrip.SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            AeviumDrip.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            AeviumDrip2.SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            AeviumDrip2.SetActive(false);
        }

    }
}
