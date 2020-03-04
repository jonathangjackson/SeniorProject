using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleState : MonoBehaviour
{
    public GameObject greenLight, redLight, blueLight, congratsLight;

    public float time;

    private IEnumerator coroutine;
    public Transform teleportTarget;
    public GameObject thePlayer;
    public GameObject VRMovement;

    public GameObject hackingButton, doorLeft, doorRight, doorLeft2, doorRight2, fadeObject2, PostProcessingVolume, checkMark01, checkMark02;

    public GameObject Tablet3, Tablet2, checkMark, redX;


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
        if (greenLight.activeInHierarchy == true && redLight.activeInHierarchy == true && blueLight.activeInHierarchy == true)
        {
            //congratsLight.SetActive(true);
            hackingButton.SetActive(false);
            doorLeft.SetActive(false);
            doorRight.SetActive(false);
            doorLeft2.SetActive(false);
            doorRight2.SetActive(false);
            checkMark01.SetActive(true);
            checkMark02.SetActive(true);
            //VRMovement.GetComponent<VRMovementOculus>().minerSwitchOn = true;                
            coroutine = WaitAndPrint(2.0f);
            StartCoroutine(coroutine);
            time -= Time.deltaTime;
            if (time < 0)
            {
                greenLight.SetActive(false);
                redLight.SetActive(false);
                blueLight.SetActive(false);
                PostProcessingVolume.SetActive(true);

                // Tablet stuff
                Tablet3.SetActive(true);
                Tablet2.SetActive(false);
                checkMark.SetActive(false);
                redX.SetActive(false);
            }
            
        }        
    }

    private IEnumerator WaitAndPrint(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        //VRMovement.GetComponent<VRMovementOculus>().minerSwitchOn = false;
        thePlayer.transform.position = teleportTarget.transform.position;
        fadeObject2.SetActive(true);


    }
}
