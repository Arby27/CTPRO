using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Enemy : MonoBehaviour
{
    //This script should be attached to all enemies in a shooter scene
    public static int nearMiss;
    public static int bulletsHit;
    public Slider Health;
    public Camera camera;
    public GameObject hitMarker;
    public GameObject hitMarkerTransform;
    Health enemHealth;
    float thisHealth;
    // Use this for initialization
    void Start()
    {
        //sets teh enemy inital health
        thisHealth = 100f;
    }

    // Update is called once per frame
    void Update()
    {
        //Sets the enemyies to look at the players, for the sake of the near miss colliders
        GetComponent<BoxCollider>().transform.LookAt(camera.transform);
        transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
        //Updates the enemy health bar for player visulisation
        Health.transform.LookAt(camera.transform);
        Health.value = thisHealth;
        if (thisHealth <= 0)
        {
            //if the health is <= 0 deativates the enemy object
            gameObject.SetActive(false);
        }

    }

    void OnCollisionEnter(Collision bulletCol)
    {
        //if the enemy is hit with a bullet reduce their health an create a hit marker
        if (bulletCol.collider.tag == "Bullet")
        {
            bulletsHit++;
            thisHealth -= 25f;
            GameObject hitMarkClone = Instantiate(hitMarker, hitMarkerTransform.transform, false) as GameObject;
            Destroy(hitMarkClone, 0.2f);

        }
    }

    void OnTriggerEnter(Collider bulletcol)
    {
        //if the bullet is a near miss increase the near miss counter
        if (bulletcol.tag == "Bullet")
        {
            nearMiss++;
        }
    }
}
