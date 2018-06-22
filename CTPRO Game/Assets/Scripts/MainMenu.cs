using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class MainMenu : MonoBehaviour {

    public Button[] menubuttons;
    public GameObject cursor;

    int i = 0;
    // a simple main menu so all the demos can be loaded frim 1 exe and so taht the order of demos played can be chosesn
	// Use this for initialization
	void Start () {
        cursor.transform.position = menubuttons[0].transform.position;
    }
	
	// Update is called once per frame
	void Update () {

        if(Input.GetButtonDown("Fire1"))
        {
            i++;
        }
        cursor.transform.position = menubuttons[i].transform.position + new Vector3(50,0,0);

        if(i > 3)
        {
            i = 0;
        }


        if (Input.GetButtonDown("Reload"))
        {
            if (i == 0)
            {
                FirsPersonShooter();
            }
            if (i == 1)
            {
                ThirdPersonShooter();
            }
            if (i == 2)
            {
                FirstPersonDriver();
            }
            if (i == 3)
            {
                ThirdPersonDriver();
            }
        }
        
    }

    public void FirsPersonShooter()
    {
        EndState.thisDemo = EndState.DemoType.FirstPersonShooter;
        SceneManager.LoadScene(1);
    }

    public void FirstPersonDriver()
    {
        EndState.thisDemo = EndState.DemoType.FirstPersonDriving;
        LapCounter.LapCount = 0;
        SceneManager.LoadScene(2);
    }


    public void ThirdPersonDriver()
    {
        EndState.thisDemo = EndState.DemoType.ThirdPersonDriving;
        LapCounter.LapCount = 0;
        SceneManager.LoadScene(3);
    }

    public void ThirdPersonShooter()
    {
        EndState.thisDemo = EndState.DemoType.ThirdPersonShooter;
        SceneManager.LoadScene(4);
    }

}
