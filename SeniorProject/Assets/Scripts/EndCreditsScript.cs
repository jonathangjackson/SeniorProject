using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EndCreditsScript : MonoBehaviour

{
    public RawImage Station6Logo;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("FadeOut", 5.0f);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FadeOut()
    {
        Station6Logo.CrossFadeAlpha(0, 3, false);
    }
}
