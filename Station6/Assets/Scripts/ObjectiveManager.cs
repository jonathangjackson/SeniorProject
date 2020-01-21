using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveManager : MonoBehaviour
{

    public string objectiveName;
    public string objectiveText;
    public List<ObjectiveManager> subObjectives;
    public bool isObjectiveActive;
    public bool isObjectiveComplete;

    /*public string ObjectiveName
    {
        get
        {
            return objectiveName;
        }

        set
        {
            ObjectiveName = value;
        }
    }

    public string ObjectiveText
    {
        get
        {
            return objectiveText;
        }

        set
        {
            objectiveText = value;
        }
    }*/

}
