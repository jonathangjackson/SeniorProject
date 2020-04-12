using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadHackingWorld : MonoBehaviour
{
    public List<Sprite> spritesList;
    public LightManager[] lights = new LightManager[4];
    public ButtonManager[] buttons = new ButtonManager[4];
    public HackingWorldManager hackingWorld;
    public int[] buttonSequence = new int[4];
    public int[] lightToButton = new int[4];
    public List<Animator> unlockDoorAnims = new List<Animator>();

    public Transform teleportTarget;
    public GameObject thePlayer;
    public GameObject VRMovement;
    public Transform respwanPoint;

    private IEnumerator coroutine;
    private bool isCollided = false;
    private GameObject terminalAccessUI;
    private GameObject terminal;
    public bool isHacked = false;

    public AudioSource teleportSound;
    public AudioSource hackingSuccessSound;

    void Start()
    {
        terminal = this.gameObject;
        //Get the UI Which is the first gameobject underneath the terminal
        terminalAccessUI = terminal.transform.GetChild(0).gameObject;
    }

    void Update()
    {
        if (hackingWorld.isHacked)
        {
            for(int i = 0; i < unlockDoorAnims.Count; i++)
            {
                unlockDoorAnims[i].SetBool("Locked", false);
            }
            Debug.Log("HACKED");
            hackingSuccessSound.Play();
            //Then the user completed the sequence
            //teleport them back
            //reset hacking world
            //set this component to inactive 

            VRMovement.GetComponent<VRMovementOculus>().minerSwitchOn = true;
            coroutine = pauseMinerva(0.1f, respwanPoint);
            StartCoroutine(coroutine);
            teleportSound.Play();
            nextSprite(2);
            //this.GetComponent<LoadHackingWorld>().enabled = false;
            hackingWorld.isHacked = false;
            isHacked = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && !isCollided)
        {
            this.gameObject.transform.GetChild(0).gameObject.SetActive(true);
            isCollided = true;
            nextSprite(1);
        }
    }

    private void OnTriggerExit(Collider other)
    {

        if (other.tag == "Player" && isCollided)
        {
            isCollided = false;
            nextSprite(0);
            this.gameObject.transform.GetChild(0).gameObject.SetActive(false);
        }
    }

    private void nextSprite(int pos)
    {
        //goes to the given position in the sprite list 
        terminal.GetComponent<SpriteRenderer>().sprite = spritesList[pos];
    }

    public void declineEntryToHackingWorld()
    {
        nextSprite(0);
    }

    public void acceptEntryToHackingWorld()
    {
        Debug.Log("ACCEPT ENTRY");
        //Go To Hacking World
        for(int i = 0; i < 4; i++)
        {
            buttons[i].getLight(lights[lightToButton[i]]);//ex. i = 0 button1.getLight(green) lightToButton[0] = 1, lights[1] = green 
            buttons[i].reset();
        }

        hackingWorld.loadSequence(buttonSequence);
        //hackingWorld.resetSequencePress();

        VRMovement.GetComponent<VRMovementOculus>().minerSwitchOn = true;
        coroutine = pauseMinerva(0.1f, teleportTarget.transform);
        //thePlayer.transform.position = teleportTarget.transform.position;
        teleportSound.Play();
        StartCoroutine(coroutine);
        
    }

    private IEnumerator pauseMinerva(float waitTime, Transform pos)
    {
        Debug.Log("PAUSE MINERVA");
        yield return new WaitForSeconds(waitTime);
        thePlayer.transform.position = pos.position;
        VRMovement.GetComponent<VRMovementOculus>().minerSwitchOn = false;
    }
}

