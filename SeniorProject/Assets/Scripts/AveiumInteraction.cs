﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AveiumInteraction : MonoBehaviour
{
    public GameObject AeviumParticleSystem, AeviumDrip, AeviumDrip2, AeviumCube, Tube;

    public Material CubeDissolve, Tube01, Tube02, Tube03;

    public float time = 7.5f;

    private IEnumerator coroutine;

    public bool ButtonPressed = false;
    public bool Lab02Done = false;

    public int ButtonPressCounter = 0;

    public CapsuleCollider button;

    public float dissolveState = 0.0f;

    public AudioSource tubeSound;
    public AudioSource dissolveSound;
    public List<Sprite> spritesList;
    public GameObject charges;

    // Start is called before the first frame update
    void Start()
    {
        
    }


    private void nextSprite()
    {
        //goes to the given position in the sprite list 
        charges.GetComponent<SpriteRenderer>().sprite = spritesList[ButtonPressCounter];
    }

    // Update is called once per frame
    void Update()
    {    
        if (Input.GetKeyDown(KeyCode.B) && ButtonPressCounter < 3)

        {
            ButtonPressed = true;
            ButtonPressCounter += 1;
            nextSprite();
        }

        if (ButtonPressed == true)
        {
            AeviumParticleSystem.SetActive(true);
            button.GetComponent<CapsuleCollider>().enabled = false;

            coroutine = WaitAndPrint(7.5f);
            StartCoroutine(coroutine);
            time -= Time.deltaTime;
            if (time < 7)
            {
                AeviumDrip.SetActive(true);
            }

            if (time < 5.5)
            {
                AeviumDrip2.SetActive(true);
            }

            if (time < 0 && ButtonPressCounter < 3)
            {
                AeviumParticleSystem.SetActive(false);
                AeviumDrip.SetActive(false);
                AeviumDrip2.SetActive(false);
                //ButtonPressCounter += 1;
                ButtonPressed = false;
                button.GetComponent<CapsuleCollider>().enabled = true; ;
                time = 7.5f;
            }

            if (time < 0 && ButtonPressCounter == 3)
            {
                AeviumParticleSystem.SetActive(false);
                AeviumDrip.SetActive(false);
                AeviumDrip2.SetActive(false);
                //ButtonPressCounter += 1;
                ButtonPressed = false;
                button.GetComponent<CapsuleCollider>().enabled = true; ;
            }

                if (ButtonPressCounter == 1 && time < 2.32)
            {
                Tube.GetComponent<Renderer>().material = Tube01;
                tubeSound.Play();
            }

            if (ButtonPressCounter == 2 && time < 2.32)
            {
                Tube.GetComponent<Renderer>().material = Tube02;
                tubeSound.Play();
            }

            if (ButtonPressCounter == 3 && time < 2.32)
            {
                Tube.GetComponent<Renderer>().material = Tube03;
                //AeviumCube.GetComponent<Renderer>().material = CubeDissolve;
                tubeSound.Play();
                Lab02Done = true;
            }          

        }

        if (ButtonPressCounter == 3 && time < 0.1 && dissolveState < 1.0f)
        {
            dissolveState += (0.3f) * Time.deltaTime;
            AeviumCube.GetComponent<Renderer>().material = CubeDissolve;
            CubeDissolve.SetFloat("Vector1_5D1B6B4A", dissolveState);
            dissolveSound.Play();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Hand" && ButtonPressCounter < 3)
        {
            ButtonPressed = true;
            ButtonPressCounter += 1;
            nextSprite();
        }
    }

    private IEnumerator WaitAndPrint(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
    }
}
