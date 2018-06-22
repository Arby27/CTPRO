using UnityEngine;
using System.Collections;

public class FirstGate : MonoBehaviour {

    public byte index;

    // allows for keeping track as to waether th eplayer took a shortcut on a lap, 
    //and only took the shortcut, so didnt change their mind or have an incredibly wide turn radius
    void OnTriggerEnter(Collider collision)
    {
        if (collision.tag == "Player")
        {
            Globals.shortcutByte |= (byte)(1 << index);
        }
    }
}

