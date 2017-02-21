using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LapCounter : MonoBehaviour {

    int LapCount;
    float LapTimer;
    string Minutes;
    string Seconds;

    public Text timer;
    public Text counter;

	// Use this for initialization
	void Start () {
   
        counter.text = "Lap: " + LapCount + "/3";
	}
	
	// Update is called once per frame
	void Update () {

        LapTimer += Time.deltaTime;
        Seconds = Mathf.Floor(LapTimer % 60).ToString();
        Minutes = Mathf.Floor(LapTimer / 60).ToString();

        timer.text = Minutes + ":" + Seconds;

    }

    void OnTriggerEnter(Collider checkpoint)
    {
        LapCount++;
        counter.text = "Lap: " + LapCount + "/3";
        print(Minutes + ":" + Seconds);
        LapTimer = 0.0f;
    }

}
