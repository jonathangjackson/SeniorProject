using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveManager : MonoBehaviour
{

    public enum ObjectiveType
    {
        MainObjective, SubObjective, ConditionalObjective
    }

    public string objectiveName;
    public string objectiveText;
    public List<ObjectiveManager> nextObjectives;//Objects to set active & Another List of objects to deactivate 
    public bool isObjectiveActive;
    public bool isObjectiveComplete;
    public ObjectiveType objectiveType;


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