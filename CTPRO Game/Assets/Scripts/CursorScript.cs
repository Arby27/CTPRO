using UnityEngine;
using System.Collections;

public class CursorScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Cursor.visible = false;
	}
	
	// Update is called once per frame
	void Update () {
	    if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (Cursor.visible == false)
            {
                Cursor.visible = true;
            }
           
        }
	}
}
