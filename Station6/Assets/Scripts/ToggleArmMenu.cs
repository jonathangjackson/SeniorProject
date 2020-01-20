using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using OVR;
using UnityEngine.UI;

public class ToggleArmMenu : MonoBehaviour
{

    public GameObject armMenu;
    bool menuOn;

    public GameObject objectives;
    bool objectivesOn;

    public GameObject bodyNameText;

    public GameObject minerva;
    public GameObject ant;
    public GameObject rig;
    public GameObject Fire;

    private Vector3 oldMinervaPos;
    private Quaternion oldMinervaRot;
    private Vector3 oldBotPos;

    Texture2D blk;
    public bool fade;
    public float alph;
    public bool click;

    bool minervaActive = true;
    bool antActive = false;
    // Start is called before the first frame update
    void Start()
    {
        menuOn = false;
        objectivesOn = false;

        blk = new Texture2D(1, 1);
        blk.SetPixel(0, 0, new Color(0, 0, 0, 0));
        blk.Apply();
        oldBotPos = GameObject.Find("Ant").transform.position;
        bodyNameText.GetComponent<Text>().text = "Minerva";
    }

    void OnGUI()
    {
        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), blk);
    }

    // Update is called once per frame
    void Update()
    {

        if (OVRInput.GetDown(OVRInput.Button.Three)){
            Debug.Log("Menu button pressed.");
            menuOn = !menuOn;

            armMenu.SetActive(menuOn);
        }

        if (OVRInput.GetDown(OVRInput.Button.Four))
        {
            Debug.Log("Objectives button pressed.");

            objectivesOn = !objectivesOn;
            Debug.Log(objectivesOn);


            objectives.SetActive(objectivesOn);
        }

        Fade();

        if (OVRInput.GetDown(OVRInput.Button.Two) && antActive)
        {
            if(!Fire.activeSelf)
                Fire.SetActive(true);
            else
                Fire.SetActive(false);
        }

        if (Input.GetKeyDown("1"))
        {
            oldMinervaRot = this.transform.rotation;
            oldMinervaPos = this.transform.position;
            ChangeToBot();
        }

        if (Input.GetKeyDown("2"))
        {
            oldBotPos = this.transform.position;
            ChangeToMinerva();
        }

    }

    public void ChangeToMinerva()
    {

        fade = !fade;

        menuOn = false;
        armMenu.SetActive(menuOn);

        bodyNameText.GetComponent<Text>().text = "Minerva";

        Vector3 MinervaPos = oldMinervaPos;

        minervaActive = true;
        antActive = false;
        rig.GetComponent<CharacterController>().height = 1.76f;
        ant.transform.parent = null;
        rig.transform.position = MinervaPos;
        rig.transform.rotation = oldMinervaRot;
        minerva.transform.parent = rig.transform;
    }

    public void ChangeToBot()
    {

        fade = !fade;

        menuOn = false;
        armMenu.SetActive(menuOn);

        bodyNameText.GetComponent<Text>().text = "S5-ANT";

        Vector3 AntPos = oldBotPos;

        minervaActive = false;
        antActive = true;
        rig.GetComponent<CharacterController>().height = 0.04f;
        minerva.transform.parent = null;
        rig.transform.position = AntPos;
        ant.transform.parent = rig.transform;
    }

    public void ChangeToHacking()
    {
        SceneManager.LoadScene("HackingScene");
    }

    void Fade()
    {

        click = true;

        if (!fade)
        {
            if (alph > 0)
            {
                alph -= Time.deltaTime * .5f;
                if (alph < 0) { alph = 0f; }
                blk.SetPixel(0, 0, new Color(0, 0, 0, alph));
                blk.Apply();
                fade = fade;
            }
        }
        if (fade)
        {
            if (click == true)
            {
                fade = !fade;
            }
            if (alph < 1)
            {
                alph = 0.6f;
                if (alph > 1) { alph = 1f; }
                blk.SetPixel(0, 0, new Color(0, 0, 0, alph));
                blk.Apply();
            }
        }
    }
}
