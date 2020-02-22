using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class rotateIcon : MonoBehaviour
{

    public Image icon;

    float speed = 1.0f;

    public Transform canvas;

    Animator animator;

    public Image informationBackground;

    public Text infoText;

    public Text meters;


    // Start is called before the first frame update
    void Start()
    {
        animator = canvas.GetComponent<Animator>();

        informationBackground.enabled = false;
        infoText.enabled = false;

    }

    // Update is called once per frame
    void Update()
    {

    }

    void swapIcon()
    {
       print("Change Icons");

        icon.enabled = false;
        meters.enabled = false;



        informationBackground.enabled = true;
        infoText.enabled = true;
    }

    void walkOut()
    {
        print("Change Back");

        icon.enabled = true;
        meters.enabled = true;



        informationBackground.enabled = false;
        infoText.enabled = false;
    }

    void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "MainCamera")
        {
            print("Collision, can spin icon");
            animator.SetTrigger("Spin");
            animator.ResetTrigger("Reverse");

        }
        else
        {
            print("No collision, cant spin icon");
        }

}

    void OnTriggerStay(Collider other)
    {

    }

    void OnTriggerExit(Collider other)
    {
        print(gameObject.name + " and trigger object " + other.name + " are no longer colliding");

        animator.ResetTrigger("Spin");

        animator.SetTrigger("Reverse");
    }
}
