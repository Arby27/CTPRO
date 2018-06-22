using UnityEngine;
using System.Collections;

public class CursorScript : MonoBehaviour {

    //hides the cursor from veiw whilst playing the game, as to not distract or confuse the player

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
