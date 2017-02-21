using UnityEngine;
using System.Collections;

public class RotateDirectionArrow : MonoBehaviour {

    public GameObject arrow;

    public Collider checkPoint;

	void OnTriggerEnter( Collider theCheckPoint)
    {
        arrow.transform.Rotate(new Vector3(0, 180, 0));
    }
    
    
}
