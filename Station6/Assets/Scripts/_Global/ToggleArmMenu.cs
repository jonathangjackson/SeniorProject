using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using OVR;

public class ToggleArmMenu : MonoBehaviour
{

    public GameObject armMenu;
    bool menuOn;

    public GameObject minerva;
    public GameObject ant;
    public GameObject rig;
    public GameObject Fire;

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

        blk = new Texture2D(1, 1);
        blk.SetPixel(0, 0, new Color(0, 0, 0, 0));
        blk.Apply();
    }

    void OnGUI()
    {
        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), blk);
    }

    // Update is called once per frame
    void Update()
    {

        if (OVRInput.GetDown(OVRInput.Button.Three)){
            menuOn = !menuOn;

            armMenu.SetActive(menuOn);
        }

        Fade();

        if (OVRInput.GetDown(OVRInput.Button.Two) && antActive)
        {
            if(!Fire.activeSelf)
                Fire.SetActive(true);
            else
                Fire.SetActive(false);
        }


    }

    public void ChangeToMinerva()
    {

        fade = !fade;

        Vector3 MinervaPos = GameObject.Find("Minerva").transform.position;


        minervaActive = true;
        antActive = false;
        rig.GetComponent<CharacterController>().height = 4.4f;
        ant.transform.parent = null;
        rig.transform.position = MinervaPos;
        minerva.transform.parent = rig.transform;
    }

    public void ChangeToBot()
    {

        fade = !fade;

        Vector3 AntPos = GameObject.Find("Ant").transform.position;

        minervaActive = false;
        antActive = true;
        rig.GetComponent<CharacterController>().height = 0.4f;
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
