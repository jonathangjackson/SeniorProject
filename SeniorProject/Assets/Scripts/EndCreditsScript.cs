using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EndCreditsScript : MonoBehaviour

{
    //public RawImage Station6Logo;
    public Animator anim;
    public ControlAnimations bS;

    // Start is called before the first frame update
    void Start()
    {
        //Invoke("FadeOut", 5.0f);
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (bS.gameFinished)
        {
            if (!this.GetComponent<Canvas>().enabled)
                this.GetComponent<Canvas>().enabled = true;
            anim.Play("EndCreditsAnimation2");
        }
    }

    //void FadeOut()
   // {
       // Station6Logo.CrossFadeAlpha(0, 3, false);
    //}


}
