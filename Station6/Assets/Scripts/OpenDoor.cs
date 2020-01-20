using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{

    public GameObject DoorA;
    public GameObject DoorB;
    public bool open = false;

    float speed;
    float translation;
    bool anim = false;
    // Start is called before the first frame update
    void Start()
    {
        speed = 0.01f;
        translation = 15f;
    }
    private void OnTriggerEnter(Collider other)
    {
        open = false;
        anim = true;
    }
    private void OnTriggerExit(Collider other)
    {
        open = true;
        anim = true;
    }

    void Update()
    {
        if (anim)
        {
            if (open)
            {//then close the door
                DoorA.GetComponent<Transform>().Translate(0, 0, -speed);
                DoorB.GetComponent<Transform>().Translate(0, 0, speed);
                translation += 0.2f;
            }
            else
            {//then open the door
                DoorA.GetComponent<Transform>().Translate(0, 0, speed);
                DoorB.GetComponent<Transform>().Translate(0, 0, -speed);
                translation -= 0.2f;
            }
            if(translation <= 0)
            {
                translation = 0;
                anim = false;
            }
            if(translation>= 15)
            {
                translation = 15;
                anim = false;
            }
        }
    }
}
