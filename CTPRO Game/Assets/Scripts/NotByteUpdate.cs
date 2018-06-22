using UnityEngine;
using System.Collections;

public class NotByteUpdate : MonoBehaviour {
    //legacy?
void OnTriggerEnter(Collider collision)
    {
        if(collision.tag == "player")
        {
            Globals.shortcutByte |= 1 << 2;
            print(Globals.shortcutByte);
        }
    }
}
