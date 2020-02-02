using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleState : MonoBehaviour
{
    public GameObject greenLight, redLight, blueLight;

    // Start is called before the first frame update
    void Start()
    {
        greenLight.SetActive(false);
        redLight.SetActive(false);
        blueLight.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
