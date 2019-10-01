using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Base player contains all global player functions
 */

public class basePlayer
{
    
    public int speed;
    public Vector3 position;
    float gravity;

    public basePlayer()
    {
        speed = 0;
        position = new Vector3(0, 0, 0);
        Debug.Log("Init");
    }

    /*
     * Moves the player in all directions
     */

    public void move(int dir, Vector3 pos)
    {
        Debug.Log(this.speed);
    }

    /*
     * if the player collides with an object that has the same name as the passed string then the collision is true
     */

    public bool collision(string a, GameObject b)
    {
        if (a == b.GetInstanceID().ToString())
            return true;
        return false;
    }

    public void updateGravity()
    {

    }
}

/*
 * Minerva class has all minerva specific funtions and data 
 * 
 */

public class Minerva : basePlayer
{
    List<GameObject> inventory;
    public Minerva(){
        inventory = new List<GameObject>();
        speed = 10;
        position = new Vector3(10, 10, 10);
    }
}

/*
 * Spy Bot Class has all spy bot specific funtions and data
 */

public class SpyBot : basePlayer
{
    public SpyBot()
    {
        speed = 20;
        position = new Vector3(2, 2, 2);
    }
} 

/*
 * player class contains all player data 
 * */

public class Player : MonoBehaviour  {

    Minerva m;
    SpyBot sBot;

    // Use this for initialization
    void Start () {
        m = new Minerva();
        sBot = new SpyBot();
        m.move(0, new Vector3(0, 0, 0));
    }
	
	// Update is called once per frame
	void Update () {

	}
}