using UnityEngine;
using System.Collections;

public class MiniMap : MonoBehaviour {

    // a simple toggleable minimap that uses a secondary camera that only renders the environment and playuer/enemy location tags,
    //to aid player in loacting enemies
    public GameObject Map;
    bool mapUp;


	// Use this for initialization
	void Start () {

        mapUp = false;
        Map.SetActive(false);

	}
	
	// Update is called once per frame
	void Update () {

        if(Input.GetButtonDown("MiniMapUp"))
        {
            if(mapUp == false)
            {
                Map.SetActive(true);
                mapUp = true;
                
            }

            else
            {
                Map.SetActive(false);
                mapUp = false;
                
            }
        }
	
	}
}
