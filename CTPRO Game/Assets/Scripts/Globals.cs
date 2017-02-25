using UnityEngine;
using System.Collections;

public class Globals : MonoBehaviour {

    public static int numberOfCollisions;
    public static DemoType demoType;

    public static byte shortcutByte;
    
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public enum DemoType
    {
        FirstPersonShooter,
        ThirdPersonShooter,
        FirstPersonDriving,
        ThirdPersonDriving
    }

}
