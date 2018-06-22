using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.IO;
using System.Text;

public class EndState : MonoBehaviour {

    public static DemoType thisDemo;
    static  StreamWriter sw;
    static int targetNumber;
    static int currentNumber;
    int enemiesLeft;
    bool isPrinted;
    float timer;
    string minutes;
    string Seconds;
    string newString;
  

    //Verifies if the folder to store data in exists , if not it tries to create teh folder once
    bool VerifyFolder(string directory, int tryCount = 1)
    {
        
        if(Directory.Exists(directory))
        {
            return true;
        }
        else
        {
            if (tryCount == 2)
            {
                return false;
            }
            else
            {
                Directory.CreateDirectory(directory);
                return VerifyFolder(directory, tryCount++);
            }
        }
    }

	// Use this for initialization
	void Start () {
        //if the folder cant be created the demo will exceute without recording any data
        bool result = VerifyFolder(Application.dataPath + "/DataTest/");

        if (result)
        {
            //Determines the demo type
            if (thisDemo == DemoType.FirstPersonShooter)
            {
                newString = Application.dataPath + "/DataTest/FirstPersonShooterParticipant.txt";
            }
            else if (thisDemo == DemoType.ThirdPersonShooter)
            {
                newString = Application.dataPath + "/DataTest/ThirdPersonShooterParticipant.txt";
            }
            else if (thisDemo == DemoType.FirstPersonDriving)
            {
                newString = Application.dataPath + "/DataTest/FirstPersonDrivingParticipant.txt";
            }
            else
            {
                newString = Application.dataPath + "/DataTest/ThirdPersonDrivingParticipant.txt";
            }

            //sets up stream writer
            targetNumber = 1;
            currentNumber = targetNumber;
            newString = GetNextFile(newString);
            isPrinted = false;
            sw = new StreamWriter(File.Open(newString, FileMode.CreateNew));

        }
        else
        {
            // couldn't start stream, so instance is running without recording.

        }
    }
    
  static void WriteToStream(string value, bool close = false)
    {
        if(sw != null)
        {
            sw.Write(value);
            if (close)
                sw.Close();
        }
    }

    // Update is called once per frame
    void Update () {

    //updaes the remaingin enemies in the shooter demo
        if (thisDemo == DemoType.FirstPersonShooter || thisDemo == DemoType.ThirdPersonShooter)
        {
            enemiesLeft = GameObject.FindGameObjectsWithTag("Enemy").Length;        
        }
        // keeps track of the time
        timer += Time.deltaTime;
        Seconds = Mathf.Floor(timer % 60).ToString(":00");
        minutes = Mathf.Floor(timer / 60).ToString("00");

        if (isPrinted == false)
        {
            //writes data to file if enemy count is == 0
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
                //writes data to file if lap count is > 3
                if (LapCounter.LapCount > 3)
                {
                    WriteDriveFile();
                    currentNumber++;
                }
            }
        }
    }

    //writes to file for the driver demo the: time taken, number of collisions and top speed and lap times
    void WriteDriveFile()
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine(string.Format("Time Taken {0}{1}, Collision Amount {2}, Top Speed {3}, Lap 1 {4}, Lap 2 {5}, Lap 3 {6}",
                                     minutes, Seconds, Globals.numberOfCollisions, DrivingScript.fastestSpeed, 
                                     LapCounter.Lap[1], LapCounter.Lap[2],LapCounter.Lap[3]));

        WriteToStream(sb.ToString(), true);
    }

    //writes to file for the shooter demo the: bullets fired, hit, time taken, number of collisions and near misses
    void WriteToFile()
    {
        StringBuilder sb = new StringBuilder();
        string BulletsFired = playerMovement.bulletsFired.ToString();
        string BulletsHit = Enemy.bulletsHit.ToString();
        sb.AppendLine(string.Format("Bullets Fired {0} , Bullets Hit {1}, Near Misses {5}, Time Taken {2}{3}, Collision Amount {4}",
                                     BulletsFired, BulletsHit,minutes,Seconds,Globals.numberOfCollisions,Enemy.nearMiss));
        
        WriteToStream(sb.ToString(), true);
    }

    //writes the name of the object the player collided with to file
    public static void WriteCollisionTypeToFile()
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine(string.Format("{0}", CollsionWithPlayer.names[CollsionWithPlayer.names.Length - 1]));
        WriteToStream(sb.ToString());   
    }

    //records wheater a participant took a shortcut on the driving demo
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

        WriteToStream(sb.ToString());
        
    }

    //Creates a file with corresponding demo type name and participant number auto attached
    string GetNextFile(string FileName)
    {
        
        string path = Path.GetDirectoryName(FileName);
        string extenstion = Path.GetExtension(FileName);
        string fileNameNoExt = Path.Combine(path, Path.GetFileNameWithoutExtension(FileName));
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
