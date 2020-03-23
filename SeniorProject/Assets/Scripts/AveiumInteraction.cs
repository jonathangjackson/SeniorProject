using System.Collections;
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

    Collider theCollider;

    // Start is called before the first frame update
    void Start()
    {
        theCollider = GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {


        if (Input.GetKeyDown("B") && ButtonPressCounter < 3)
        {
            ButtonPressed = true;
            ButtonPressCounter += 1;
        }

        if (ButtonPressed == true)
        {
            AeviumParticleSystem.SetActive(true);
            theCollider.GetComponent<Collider>().enabled = false;

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

            if (time < 0)
            {
                AeviumParticleSystem.SetActive(false);
                AeviumDrip.SetActive(false);
                AeviumDrip2.SetActive(false);
                //ButtonPressCounter += 1;
                ButtonPressed = false;
                theCollider.GetComponent<Collider>().enabled = true; ;
                time = 7.5f;
            }

            if (ButtonPressCounter == 1 && time < 2.32)
            {
                Tube.GetComponent<Renderer>().material = Tube01;
            }

            if (ButtonPressCounter == 2 && time < 2.32)
            {
                Tube.GetComponent<Renderer>().material = Tube02;
            }

            if (ButtonPressCounter == 3 && time < 2.32)
            {
                Tube.GetComponent<Renderer>().material = Tube03;
                AeviumCube.GetComponent<Renderer>().material = CubeDissolve;
                Lab02Done = true;
            }
            
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && ButtonPressCounter < 3)
        {
            ButtonPressed = true;
        }
    }

    private IEnumerator WaitAndPrint(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
    }
}
