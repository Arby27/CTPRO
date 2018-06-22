using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DrivingScript : MonoBehaviour {

    //This script attaches to player bike objects within the driving scenes
  public static float fastestSpeed;
  public  WheelCollider backWheel;
  public Rigidbody bikeRB;
  public GameObject bike;
  public Text speed;
  float yRot;
  float acceleration;
  float force;
  double CurrentSpeed;
  int speedMPH;

	// Use this for initialization
	void Start () {
        
        transform.Rotate(new Vector3(0, 90, 0));

	}
	
	// Update is called once per frame
	void Update () {
        
        //when trigger is held accelrate
        if (Input.GetButton("Fire1"))
        {
            acceleration += 0.15f;
      
        }

        //Give a mass based force addition to make the vehicle move
        force = backWheel.mass * acceleration;
        GetComponent<Rigidbody>().AddForce(transform.forward * force, ForceMode.Acceleration);

        //a constant deceleration, when not accelerating
        if (acceleration > 0.0000000000000001f)
        {
            acceleration -= 0.05f;
        }
        //a break and eventually reverse for the bike
        if (Input.GetButton("Reload"))
        {
            acceleration -=  0.15f;

        }

        //Gets teh current speed and converts it to MPH
        CurrentSpeed = GetComponent<Rigidbody>().velocity.magnitude;
        CurrentSpeed = (CurrentSpeed * 3600) / 1609.344;

        //if the current speed is faster than fastest overwrite current fastest speed, this is static so it can be used in file writer
        if(CurrentSpeed > fastestSpeed)
        {
            fastestSpeed = (float)CurrentSpeed;
        }

        speedMPH = (int)CurrentSpeed;
        speed.text = speedMPH.ToString() + "MPH";
        //allows the bike to turn
        yRot -= Input.GetAxis("Horizontal");
        bike.transform.eulerAngles = new Vector3(0, -yRot, 0);
        bike.transform.rotation = Quaternion.Slerp(Quaternion.Euler(transform.rotation.eulerAngles.x, 
                                                                    transform.rotation.eulerAngles.y,
                                                                    transform.rotation.eulerAngles.z),
                                                                    Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, 0),
                                                                    Time.deltaTime);
        

    }

    //Slows the bike down on collision
   void OnCollisionEnter(Collision col)
    {
        if(col.collider.tag == "Obstacle")
        gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        gameObject.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        acceleration = 0;
        force = 0;
       
    }
   
}
