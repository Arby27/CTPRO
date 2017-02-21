using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class playerMovement : MonoBehaviour {

    public static int bulletsFired;
    public float moveSpeed;
    public float bulletSpeed;
    public float rotSpeed;
    public GameObject bulletprefab;
    public GameObject muzzleFlash;
    public Transform bulletTransform;
    public Transform muzzleTransform;
    public Text Ammo;
    public Animator animator;

    public Camera PlayerCam;
    public GameObject anchor;

    GameObject bullet;
    GameObject flash;
    float oldX;
    float oldZ;
    int ammoCount;
    // Use this for initialization
    void Start () {

        ammoCount = 8;
        Ammo.text = "Ammo: " + ammoCount + "/8";
       
	}

    // Update is called once per frame
    void Update()
    {
        PlayerCam.transform.position = anchor.transform.position;
        float transformX = 0;
        float transformZ = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;

        if(oldZ != transformZ)
        {
            animator.SetBool("isMoving", true);
            Destroy(flash);
            
        }

        if (oldX == transformX && oldZ == transformZ)
        {
            animator.SetBool("isMoving", false);

        }

        oldX  = transformX;
       oldZ  = transformZ;


        float rotationY = Input.GetAxis("HorizontalRotation") * rotSpeed;
        float rotationX = Input.GetAxis("VerticalRotation") * rotSpeed;

       

        transform.Translate(transformX, 0, transformZ);
       transform.Rotate(rotationX, rotationY, 0);

        if(Input.GetButtonDown("Fire1") && ammoCount > 0 )
        {
            FireGun();
        }
        else
        {

        }

       if(Input.GetButtonDown("Reload"))
        {
            ammoCount = 8;
            Ammo.text = "Ammo: " + ammoCount + "/8";
        }
        

    }


    void FireGun()
    {
        bullet = Instantiate(bulletprefab, bulletTransform.position , bulletTransform.rotation) as GameObject;
        flash = Instantiate(muzzleFlash, muzzleTransform.position, muzzleTransform.rotation) as GameObject;
        bullet.GetComponent<Rigidbody>().velocity = bulletTransform.forward * bulletSpeed;
        bulletsFired++;
        Destroy(flash, 0.05f);
        Destroy(bullet, 5.0f);
        ammoCount--;
        Ammo.text = "Ammo: " + ammoCount + "/8";
    }
}
