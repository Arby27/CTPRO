using UnityEngine;
using System.Collections;

public class MiniMap : MonoBehaviour {

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
