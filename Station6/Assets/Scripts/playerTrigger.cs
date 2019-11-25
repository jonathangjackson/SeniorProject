using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerTrigger : MonoBehaviour
{
    public GameObject roomNameText;
    public GameObject objectiveText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider collision)
    {
        objectiveTrigger(collision);
        if(collision.gameObject.tag == "Door")
        {
            collision.gameObject.GetComponent<Animator>().SetBool("open", true);
        }
        /*
        if (collision.gameObject.name == "PreparedOVRCameraRig")
        {
            Debug.Log("entered");
            SceneManager.LoadScene("HackingScene");
        }*/
    }
    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.tag == "Door")
        {
            collision.gameObject.GetComponent<Animator>().SetBool("open", false);
        }
    }
    

    private void objectiveTrigger(Collider collision)
    {
        //
        if (collision.gameObject.tag == "Airlock")
        {
            Debug.Log("Airlock Trigger");
            roomNameText.GetComponent<Text>().text = "Air Lock";
            objectiveText.GetComponent<Text>().text = "Head to the Storage Room";

        }
        else if (collision.gameObject.tag == "StorageRoom")
        {
            Debug.Log("Storage Trigger");
            roomNameText.GetComponent<Text>().text = "Storage Room";
            objectiveText.GetComponent<Text>().text = "Find the Generator Room";

        }
        else if (collision.gameObject.tag == "Generator")
        {
            Debug.Log("Generator Trigger");
            roomNameText.GetComponent<Text>().text = "Generator Room";
            objectiveText.GetComponent<Text>().text = "Find a way to open the door to the Generator Room so Minerva can enter it.";
        }
        else if (collision.gameObject.name == "GeneratorRoomDoor")
        {
            roomNameText.GetComponent<Text>().text = "Storage Room";
            objectiveText.GetComponent<Text>().text = "Use the S5-ANT to gain access to the Generator Room.";
        }
    }
}
