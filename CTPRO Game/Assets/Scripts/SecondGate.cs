using UnityEngine;
using System.Collections;

public class SecondGate : MonoBehaviour {

    void OnTriggerEnter(Collider collision)
    {
        if (collision.tag == "player")
        {
            Globals.shortcutByte |= 1 << 3;
            print(Globals.shortcutByte);         
        }
    }
}

