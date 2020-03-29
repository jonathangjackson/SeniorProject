using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AeviumLab3 : MonoBehaviour
{
    public GameObject AeviumParticleSystem, AeviumDrip, AeviumCube, Tube;

    public Material CubeDissolve, Tube01, Tube02, Tube03;

    public float time = 5f;

    private IEnumerator coroutine;

    public bool ButtonPressed = false;
    public bool Lab03Done = false;

    public int ButtonPressCounter = 0;

    public CapsuleCollider button;

    public float dissolveState = 0.0f;

    public AudioSource tubeSound;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {     
        if (Input.GetKeyDown(KeyCode.N) && ButtonPressCounter < 3)

            {
                ButtonPressed = true;
                ButtonPressCounter += 1;
            }

        if (ButtonPressed == true)
        {
            AeviumParticleSystem.SetActive(true);
            button.GetComponent<CapsuleCollider>().enabled = false;

            coroutine = WaitAndPrint(5.0f);
            StartCoroutine(coroutine);
            time -= Time.deltaTime;
            if (time < 4)
            {
                AeviumDrip.SetActive(true);
            }

            if (time < 0 && ButtonPressCounter < 3)
            {
                AeviumParticleSystem.SetActive(false);
                AeviumDrip.SetActive(false);                
                //ButtonPressCounter += 1;
                ButtonPressed = false;
                button.GetComponent<CapsuleCollider>().enabled = true; ;
                time = 7.5f;
            }

            if (time < 0 && ButtonPressCounter == 3)
            {
                AeviumParticleSystem.SetActive(false);
                AeviumDrip.SetActive(false);
                //ButtonPressCounter += 1;
                ButtonPressed = false;
                button.GetComponent<CapsuleCollider>().enabled = true; ;
            }

            if (ButtonPressCounter == 1 && time < 1.8)
            {
                Tube.GetComponent<Renderer>().material = Tube01;
                tubeSound.Play();
            }

            if (ButtonPressCounter == 2 && time < 1.8)
            {
                Tube.GetComponent<Renderer>().material = Tube02;
                tubeSound.Play();
            }

            if (ButtonPressCounter == 3 && time < 1.8)
            {
                Tube.GetComponent<Renderer>().material = Tube03;
                //AeviumCube.GetComponent<Renderer>().material = CubeDissolve;
                Lab03Done = true;
                tubeSound.Play();
            }

        }

        if (ButtonPressCounter == 3 && time < 0.1 && dissolveState < 1.0f)
        {
            dissolveState += (0.3f) * Time.deltaTime;
            AeviumCube.GetComponent<Renderer>().material = CubeDissolve;
            CubeDissolve.SetFloat("Vector1_5D1B6B4A", dissolveState);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Hand" && ButtonPressCounter < 3)
        {
            ButtonPressed = true;
            ButtonPressCounter += 1;
        }
    }

    private IEnumerator WaitAndPrint(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
    }
}
