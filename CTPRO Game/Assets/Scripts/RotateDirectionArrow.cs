using UnityEngine;
using System.Collections;

public class RotateDirectionArrow : MonoBehaviour {

    public GameObject arrow;

    public Collider checkPoint;

    //rotates a UI arrow when collided with a checkpoint as to allow for the player to know which direction to take on a figure 8-esque track
	void OnTriggerEnter( Collider theCheckPoint)
    {
        arrow.transform.Rotate(new Vector3(0, 180, 0));
    }
    
    
}
