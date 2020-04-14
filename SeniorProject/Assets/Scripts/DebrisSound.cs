using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebrisSound : MonoBehaviour
{
    public finalLevel aviumCheck;
    public AudioSource Audio;
    int counter;

    // Start is called before the first frame update
    void Start()
    {
        counter = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void onTriggerEnter(Collider other)
    {
        if(aviumCheck.aviumAcquired == true && other.tag == "Player" && counter == 0)
        {
            counter = 1;
            Audio.Play();
        }
    }
}
