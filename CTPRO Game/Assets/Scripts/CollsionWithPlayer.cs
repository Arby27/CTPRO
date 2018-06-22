using UnityEngine;
using System.Collections;

public class CollsionWithPlayer : MonoBehaviour {

    public static string[] names;
    static int count;
    //This script should be attached to all non floor environment objects
    void Start()
    {
        count = 1;
    }
    void Update()
    {
        names = new string[count];
    }

    void OnCollisionEnter(Collision col)
    {
        //When the player collides with an object, 
        //increse the number of collisions and write the new of the collided object to a string
        if(col.collider.tag == "Player" )
        {
            Globals.numberOfCollisions++;
            names[count-1] = gameObject.name;
            EndState.WriteCollisionTypeToFile();
            count++;
        }
    }
}
