using UnityEngine;
using System.Collections;

public class FirstGate : MonoBehaviour {

    public byte index;

    void OnTriggerEnter(Collider collision)
    {
        if (collision.tag == "Player")
        {
            Globals.shortcutByte |= (byte)(1 << index);
            print(Globals.shortcutByte);
        }
    }
}

