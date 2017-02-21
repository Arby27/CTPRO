using UnityEngine;
using System.Collections;
using System.IO;
using System.Text;

public class EndState : MonoBehaviour {

    int enemiesLeft;
    bool isPrinted;
    StreamWriter sw;
    float timer;
    string Minutes;
    string Seconds;
    string newString;
    public static DemoType thisDemo;  
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
            if (enemiesLeft == 0)
            {
                WriteToFile();
                isPrinted = true;
            }
        }

       


    }

    void WriteToFile()
    {
        StringBuilder sb = new StringBuilder();
        string BulletsFired = playerMovement.bulletsFired.ToString();
        string BulletsHit = Enemy.bulletshit.ToString();
        sb.AppendLine(string.Format("Bullets Fired {0} , Bullets Hit {1}, Time Taken {2}{3}, Collision Amount {4}",
                                     BulletsFired, BulletsHit,Minutes,Seconds,Globals.numberOfCollisions));
        sw.Write(sb.ToString());
        sw.Close();
    }

    void WriteCollisionTypeToFile()
    {
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < CollsionWithPlayer.names.Length; i++)
        {
            sb.AppendLine(string.Format("{0}", CollsionWithPlayer.names[i]));
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
