using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.IO;
using System.Text;

public class EndState : MonoBehaviour {

    int enemiesLeft;
    bool isPrinted;
  static  StreamWriter sw;
    float timer;
    string Minutes;
    string Seconds;
    string newString;
    public static DemoType thisDemo;

    static int TargetNumber;
    static int currentNumber;


	// Use this for initialization
	void Start () {

        if (thisDemo == DemoType.FirstPersonShooter)
        {
            newString = "/DataTest/FirstPersonShooterParticipant.txt";
        }
        else if(thisDemo == DemoType.ThirdPersonShooter)
        {
            newString = "/DataTest/ThirdPersonShooterParticipant.txt";
        }
        else if(thisDemo == DemoType.FirstPersonDriving)
        {
            newString = "/DataTest/FirstPersonDrivingParticipant.txt";
        }
        else
        {
            newString = "/DataTest/ThirdPersonDrivingParticipant.txt";
        }

        TargetNumber = 1;
        currentNumber = TargetNumber;



       newString = GetNextFile(newString);
      //  print(newString);
        isPrinted = false;
        sw =  new StreamWriter(File.Open(newString, FileMode.CreateNew));


    }
	
	// Update is called once per frame
	void Update () {

    
        if (thisDemo == DemoType.FirstPersonShooter || thisDemo == DemoType.ThirdPersonShooter)
        {
            enemiesLeft = GameObject.FindGameObjectsWithTag("Enemy").Length;        
        }
        timer += Time.deltaTime;
        Seconds = Mathf.Floor(timer % 60).ToString(":00");
        Minutes = Mathf.Floor(timer / 60).ToString("00");

        if (isPrinted == false)
        {
            if (thisDemo == DemoType.FirstPersonShooter || thisDemo == DemoType.ThirdPersonShooter)
            {
                if (enemiesLeft == 0)
                {
                    WriteToFile();
                    isPrinted = true;
                    currentNumber++;
                }
            }
            if (thisDemo == DemoType.FirstPersonDriving || thisDemo == DemoType.ThirdPersonDriving)
            {
                if (LapCounter.LapCount > 3)
                {
                    WriteDriveFile();
                    currentNumber++;
                }
            }
        }

        


    }


     void WriteDriveFile()
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine(string.Format("Time Taken {0}{1}, Collision Amount {2}, Top Speed {3}, Lap 1 {4}, Lap 2 {5}, Lap 3 {6}",
                                     Minutes, Seconds, Globals.numberOfCollisions, DrivingScript.fastestSpeed, 
                                     LapCounter.Lap[1], LapCounter.Lap[2],LapCounter.Lap[3]));
        sw.Write(sb.ToString());
        sw.Close();
    }

    void WriteToFile()
    {
        StringBuilder sb = new StringBuilder();
        string BulletsFired = playerMovement.bulletsFired.ToString();
        string BulletsHit = Enemy.bulletshit.ToString();
        sb.AppendLine(string.Format("Bullets Fired {0} , Bullets Hit {1}, Near Misses {5}, Time Taken {2}{3}, Collision Amount {4}",
                                     BulletsFired, BulletsHit,Minutes,Seconds,Globals.numberOfCollisions,Enemy.nearMiss));
        sw.Write(sb.ToString());
        sw.Close();
    }

    public static void WriteCollisionTypeToFile()
    {
        StringBuilder sb = new StringBuilder();
        //for (int i = 0; i < CollsionWithPlayer.names.Length; i++)
        {
            sb.AppendLine(string.Format("{0}", CollsionWithPlayer.names[CollsionWithPlayer.names.Length - 1]));
        }
        sw.Write(sb.ToString());
       // sw.Close();
        
        
    }

    public static void ShortCutRecord(bool isCheating)
    {
        StringBuilder sb = new StringBuilder();

        if (isCheating == true)
        {
            sb.AppendLine(string.Format("Took Shorcut on Lap {0}",LapCounter.LapCount ));
        }
        else
        {
            sb.AppendLine(string.Format("Did not take Shorcut on Lap {0}", LapCounter.LapCount));
        }

        sw.Write(sb.ToString());
    }

    string GetNextFile(string FileName)
    {
        
        string path = Path.GetDirectoryName(FileName);
        string extenstion = Path.GetExtension(FileName);
        string fileNameNoExt = Path.Combine(path, Path.GetFileNameWithoutExtension(FileName));

        print(path + extenstion + fileNameNoExt);
        int i = 0;

        while(File.Exists(FileName))
        {
            i++;
            FileName = string.Format("{0}({1}){2}", fileNameNoExt, i, extenstion);
           
        }
        return FileName;
    }


    public enum DemoType
    {
        FirstPersonShooter,
        ThirdPersonShooter,
        FirstPersonDriving,
        ThirdPersonDriving
    }

}
