using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveManager : MonoBehaviour
{
    public bool objective1 = false;
    public bool objective2 = false;
    public bool objective3 = false;

    string text1;
    string text2;
    string text3;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnGUI()
    {
        if (objective1 == true)
        {
            GUI.Label(new Rect(10, 10, 270, 150), text1);
            objectiveDestroy();
        }

        if (objective2 == true)
        {
            GUI.Label(new Rect(10, 10, 270, 150), text2);
        }

        if (objective3 == true)
        {
            GUI.Label(new Rect(10, 10, 270, 150), text3);
        }
    }

    IEnumerator objectiveDestroy()
    {
        yield return new WaitForSeconds (2);
        objective1 = false;

    }
}
