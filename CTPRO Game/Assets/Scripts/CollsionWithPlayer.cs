using UnityEngine;
using System.Collections;

public class CollsionWithPlayer : MonoBehaviour {

    public static string[] names;
    static int count;

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
        if(col.collider.tag == "Player" )
        {
            Globals.numberOfCollisions++;
            names[count-1] = gameObject.name;
            EndState.WriteCollisionTypeToFile();
           //names[count] = this.gameObject.name;
            count++;
           print(names[count-2]);

        }
    }
}
