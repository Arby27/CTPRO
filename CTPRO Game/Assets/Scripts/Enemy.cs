using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Enemy : MonoBehaviour {

    public static int bulletshit;
    public Slider Health;
    public Camera camera;

    Health enemHealth;
    float thisHealth;
    // Use this for initialization
    void Start () {
        thisHealth = 100f;
	}
	
	// Update is called once per frame
	void Update () {
        GetComponent<BoxCollider>().transform.LookAt(camera.transform);
        transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
        Health.transform.LookAt(camera.transform);
	    Health.value = thisHealth;
        if(thisHealth <= 0)
        {
            Destroy(this.gameObject);
        }

	}

    void OnCollisionEnter(Collision bulletCol)
    {
        if(bulletCol.collider.tag == "Bullet")
        {
            bulletshit++;
            thisHealth -= 25f;
            print(thisHealth);
            
        }
    }
}
