using UnityEngine;
using System.Collections;

public class CollsionWithPlayer : MonoBehaviour {

    public static string[] names;
    static int count;

    void Update()
    {
        names = new string[count];
    }

    void OnCollisionEnter(Collision col)
    {
        if(col.collider.tag == "Player" )
        {
            Globals.numberOfCollisions++;
           
           // names[count] = this.gameObject.name;
            count++;
            print(Globals.numberOfCollisions + " " + this.gameObject.name);

        }
    }
}
