using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerTrigger : MonoBehaviour
{
    public GameObject roomNameText;
    public GameObject objectiveText;
    string roomName;
    string objective;
    public List<ObjectiveManager> objectives ;

    // Start is called before the first frame update
    void Start()
    {
        //ObjectiveManager objective = new ObjectiveManager();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider collision)
    {
        RoomTrigger(collision);

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

    private void RoomTrigger(Collider collision)
    {
        if (collision.gameObject.tag == "Airlock")
        {
            Debug.Log("Airlock Trigger");
            roomName = "Airlock";

        }
        else if (collision.gameObject.tag == "StorageRoom")
        {
            Debug.Log("Storage Trigger");
            roomName = "Storage Room";

        }
        else if (collision.gameObject.tag == "Generator")
        {
            Debug.Log("Generator Trigger");
            roomName = "Generator Room";

        }

        roomNameText.GetComponent<Text>().text = roomName;

    }

    private void objectiveTrigger(Collider collision)
    {

        if(collision.name == objectives[0].objectiveName)
        {
            objective = objectives[0].objectiveText;
            objectiveText.GetComponent<Text>().text = objective;
            objectives.RemoveAt(0);

        }

        /*if (collision.gameObject.tag == "Airlock")
        {
            Debug.Log("Airlock Trigger");
            objective = "Head to the Storage Room";

        }
        else if (collision.gameObject.tag == "StorageRoom")
        {
            Debug.Log("Storage Trigger");
            objective = "Find the Generator Room";

        }
        else if (collision.gameObject.tag == "Generator")
        {
            Debug.Log("Generator Trigger");
            objective = "Find a way to open the door to the Generator Room so Minerva can enter it.";
        }
        else if (collision.gameObject.name == "GeneratorRoomDoor")
        {

            objective = "Use the S5-ANT to gain access to the Generator Room.";
        }*/


    }
}


