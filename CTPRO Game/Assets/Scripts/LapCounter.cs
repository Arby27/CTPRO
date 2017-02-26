using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class LapCounter : MonoBehaviour {

    public static int LapCount;
    float LapTimer;
    string Minutes;
    string Seconds;

    string Minutes2;
    string Seconds2;


    public static float[] Lap = new float[4];
    float previousLap;

    public Text timer;
    public Text counter;
    public Text[] lapTimes;

    bool cheater;

	// Use this for initialization
	void Start () {
        LapCount = 0;
        counter.text = "Lap: " + LapCount + "/3";
	}
	
	// Update is called once per frame
	void Update () {

        LapTimer += Time.deltaTime;
        Seconds = Mathf.Floor(LapTimer % 60).ToString();
        Minutes = Mathf.Floor(LapTimer / 60).ToString();

        timer.text = Minutes + ":" + Seconds;

        if(LapCount > 3)
        {
            SceneManager.LoadScene(0);
        }

    }

    void OnTriggerEnter(Collider checkpoint)
    {
        Lap[LapCount] = LapTimer - previousLap;
        print(Lap[LapCount]);
        previousLap = Lap[LapCount]+ previousLap;

        Minutes2 = Mathf.Floor(Lap[LapCount] % 60).ToString();
        Seconds2 = Mathf.Floor(Lap[LapCount] / 60).ToString();

        lapTimes[LapCount].text = Minutes2 + ":" + Seconds2;
        counter.text = "Lap: " + LapCount + "/3";
        print(Minutes + ":" + Seconds);


        if(Globals.shortcutByte == 5)
        {
            cheater = true;
        }
        else
        {
            cheater = false;
        }
        if (LapCount > 0)
        {
            EndState.ShortCutRecord(cheater);
        }
        Globals.shortcutByte = 0;
        LapCount++;
        
    }

}
