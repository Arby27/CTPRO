using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
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
    public Text EnemiesLeft;
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
        //set player starting ammo count
        ammoCount = 8;
        Ammo.text = "Ammo: " + ammoCount + "/8";
       
	}

    // Update is called once per frame
    void Update()
    {
        //updates the amount of enemies left on the UI screen
        EnemiesLeft.text = "Enemies Left " + GameObject.FindGameObjectsWithTag("Enemy").Length.ToString();
        //sets the position of the camera to an predefined anchor position
        PlayerCam.transform.position = anchor.transform.position;
        float transformX = 0;
        //Move the player when they move the left stick
        float transformZ = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;
        //destroys muzzle flash if the player moves
        if(oldZ != transformZ)
        {
            animator.SetBool("isMoving", true);
            Destroy(flash);
            
        }
        //sets a swap between animations for idle and walking
        if (oldX == transformX && oldZ == transformZ)
        {
            animator.SetBool("isMoving", false);

        }
        //updates old stored position for above checks
        oldX  = transformX;
        oldZ  = transformZ;

        //updates the players rotation when they move the right stick
        float rotationY = Input.GetAxis("HorizontalRotation") * rotSpeed;
        float rotationX = Input.GetAxis("VerticalRotation") * rotSpeed;

       
        //applys transformations to the player object
        transform.Translate(transformX, 0, transformZ);
        transform.Rotate(rotationX, rotationY, 0);
        //fires the gun on pressing the right trigger
        if(Input.GetButtonDown("Fire1") && ammoCount > 0 )
        {
            FireGun();
        }
        else
        {

        }
        //resets teh amount of bullets in the clip when pressing square or left trigger
       if(Input.GetButtonDown("Reload"))
        {
            ammoCount = 8;
            Ammo.text = "Ammo: " + ammoCount + "/8";
        }
        
       //when all enemies are destroyed reload to the main menu
       if(GameObject.FindGameObjectsWithTag("Enemy").Length <= 0)
        {
            SceneManager.LoadScene(0);
        }


    }


    void FireGun()
    {
        //when the gun is fired, instantiate a bullet that moves in a straight line from the guns barrell 
        //also create a muzzle flash for a breif amount of time
        //then update teh amount of bullets fired and remain ammo
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
