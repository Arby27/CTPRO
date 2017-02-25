using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Enemy : MonoBehaviour
{
    public static int nearMiss;
    public static int bulletshit;
    public Slider Health;
    public Camera camera;
    public GameObject hitmarker;
    public GameObject hitMarkerTransform;

    Health enemHealth;
    float thisHealth;
    // Use this for initialization
    void Start()
    {
        thisHealth = 100f;
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<BoxCollider>().transform.LookAt(camera.transform);
        transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
        Health.transform.LookAt(camera.transform);
        Health.value = thisHealth;
        if (thisHealth <= 0)
        {
            //Destroy(gameObject);
            gameObject.SetActive(false);
        }

    }

    void OnCollisionEnter(Collision bulletCol)
    {
        if (bulletCol.collider.tag == "Bullet")
        {
            bulletshit++;
            thisHealth -= 25f;
            print(thisHealth);
           GameObject hitMarkClone = Instantiate(hitmarker, hitMarkerTransform.transform, false) as GameObject;
            Destroy(hitMarkClone, 0.2f);

        }
    }

    void OnTriggerEnter(Collider bulletcol)
    {
        if (bulletcol.tag == "Bullet")
        {
            nearMiss++;
            print("So close");
        }
    }
}
