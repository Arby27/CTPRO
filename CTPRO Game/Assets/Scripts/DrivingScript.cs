using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DrivingScript : MonoBehaviour {

   public  WheelCollider frontWheel;

   public  WheelCollider backWheel;

   public Rigidbody bikeRB;
   public GameObject Bike;

   public Text Speed;

   public int moveSpeed;
   public int brakeSpeed;
   public int turnAngle;

    float zRot;
    float yRot;

    Quaternion currentRot;
    Quaternion targetRot;

	// Use this for initialization
	void Start () {
        
        transform.Rotate(new Vector3(0, 90, 0));
        
	}
	
	// Update is called once per frame
	void Update () {
        
            if (Input.GetButtonDown("Fire1")|| OVRInput.Get(OVRInput.Axis1D.SecondaryIndexTrigger)> 0.0f)
        {
            print("pressed");
            backWheel.motorTorque += moveSpeed ;
            backWheel.brakeTorque = 0;
        }
        if(Input.GetButtonDown("Reload"))
        {
            if (backWheel.motorTorque < 0)
            {
                backWheel.brakeTorque = brakeSpeed;
            }
            else if(backWheel.motorTorque == 0)
            {
                backWheel.brakeTorque = brakeSpeed;
            }
            else
            {
               
                backWheel.motorTorque -= moveSpeed;
            }
        }


        frontWheel.steerAngle = Input.GetAxis("Horizontal") * turnAngle;

        //zRot -= Input.GetAxis("Horizontal");
        zRot = Mathf.Clamp(zRot, -45, 45);
        yRot -= Input.GetAxis("Horizontal");
        transform.eulerAngles =  new Vector3(0, 90, zRot);
        Bike.transform.eulerAngles = new Vector3(0, -yRot, zRot);

        Speed.text = backWheel.motorTorque.ToString() + "MPH";


        Bike.transform.rotation = Quaternion.Slerp(Quaternion.Euler(transform.rotation.eulerAngles.x, 
                                                                    transform.rotation.eulerAngles.y,
                                                                    transform.rotation.eulerAngles.z),
                                                                    Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, 0),
                                                                    Time.deltaTime);
        

    
        

     


    }
}
