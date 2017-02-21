using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour {

    float maxHealth = 100f;
    float health;
	// Use this for initialization
	void Start () {
        health = maxHealth;
	}
	
	// Update is called once per frame
	void Update () {

	}


    public void setHealth(float damage)
    {
        health -= damage;
    }
   public float getHealth()
    {
        return health;
    }

}
