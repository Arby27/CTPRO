using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DrivingScript : MonoBehaviour {

     
    public static float fastestSpeed;

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

    float acceleration;
    float force;
    float distance;
    double intialVel;
    double CurrentSpeed;

    Quaternion currentRot;
    Quaternion targetRot;


    Vector3 lastPos;
    Vector3 currentPos;
    int speed;

	// Use this for initialization
	void Start () {
        
        transform.Rotate(new Vector3(0, 90, 0));
        lastPos = currentPos;
	}
	
	// Update is called once per frame
	void Update () {
        
        if (Input.GetButton("Fire1"))
        {
            acceleration += 0.1f;
            backWheel.brakeTorque = 0;
        }
        force = backWheel.mass * acceleration;
       // GetComponent<Rigidbody>().AddRelativeForce(new Vector3(0, 0, force));
       
        GetComponent<Rigidbody>().AddForce(transform.forward * force, ForceMode.Acceleration);

        if (acceleration > 0.0000000000000001f)
        {
            acceleration -= 0.05f;
        }

        if (Input.GetButton("Reload"))
        {
            acceleration -=  0.09f;

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

        currentPos = transform.position;
        distance = Vector3.Distance(currentPos,lastPos);
        lastPos = currentPos;


        CurrentSpeed = GetComponent<Rigidbody>().velocity.magnitude;
        CurrentSpeed = (CurrentSpeed * 1609.344) / 3600;

        if(CurrentSpeed > fastestSpeed)
        {
            fastestSpeed = (float)CurrentSpeed;
        }

        speed = (int)CurrentSpeed;

   

        frontWheel.steerAngle = Input.GetAxis("Horizontal") * turnAngle;

        //zRot -= Input.GetAxis("Horizontal");
        zRot = Mathf.Clamp(zRot, -45, 45);
        yRot -= Input.GetAxis("Horizontal");
        transform.eulerAngles =  new Vector3(0, 90, zRot);
        Bike.transform.eulerAngles = new Vector3(0, -yRot, zRot);

        Speed.text = speed.ToString() + "MPH";


        Bike.transform.rotation = Quaternion.Slerp(Quaternion.Euler(transform.rotation.eulerAngles.x, 
                                                                    transform.rotation.eulerAngles.y,
                                                                    transform.rotation.eulerAngles.z),
                                                                    Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, 0),
                                                                    Time.deltaTime);
        

   

    }

   void OnCollisionEnter(Collision col)
    {
        if(col.collider.tag == "Obstacle")
        print("collision");
        gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        gameObject.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        acceleration = 0;
        force = 0;
       
    }
   
}
