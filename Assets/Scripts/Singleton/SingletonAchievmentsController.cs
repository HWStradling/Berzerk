using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SingletonAchievmentsController : MonoBehaviour
{
    public static SingletonAchievmentsController instance { get; private set; }

    public static bool[] acheivmentsArray { get; set; } = new bool[20];
    public static Queue<int> acheivmentsQueue { get; set; } = new Queue<int>();
    private static bool running = false;
    private static string currentProfile;
    private void Awake()
    {
        if (instance)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            currentProfile = PlayerPrefs.GetString("profile", "Profile 1");
            bool[] tempAcheivmentsArray;
            try
            {
                 tempAcheivmentsArray = SaveSystem.PlayerData.Achievments;
            }
            catch {
                 tempAcheivmentsArray = new bool[0];
            }
            
            for (int i = 0; i < tempAcheivmentsArray.Length; i++)
            {
                acheivmentsArray[i] = tempAcheivmentsArray[i];
            }
        }
    }
    private void FixedUpdate()
    {
        if (!running && acheivmentsQueue.Count > 0)
        {
            running = true;
            StartCoroutine(DisplayAcheivment());
        }
    }
    public void UnlockAcheivment(int acheivment)
    {
        if (acheivment < acheivmentsArray.Length && acheivmentsArray[acheivment] == false)
        {
            acheivmentsQueue.Enqueue(acheivment);
        }
        else
        {
            Debug.Log("invalid achivment index passed or acheivment already unlocked");
        }
    }

    IEnumerator DisplayAcheivment()
    {
        yield return new WaitUntil(() => GameObject.FindGameObjectsWithTag("acheivment_container").Length > 0);
        if (PlayerPrefs.GetString("profile", "Profile 1") != currentProfile) // case switch profile with queued acheivment,
        {
            Debug.Log("profile change");
            acheivmentsQueue.Clear();
        }
        if (acheivmentsQueue.Count == 0)
        {
            yield break;
        }
        int newAchievment = acheivmentsQueue.Dequeue();
        string message = getAcheivmentMessage(newAchievment);
        ShowAcheivment(message);
        acheivmentsArray[newAchievment] = true;
        yield return new WaitForSeconds(8);
        if (GameObject.FindGameObjectsWithTag("acheivment_container").Length > 0)
        {
            DisableAcheivmentWindow();
            running = false;
        }
        else
        {
            running = false;
        }
    }

    private string getAcheivmentMessage(int acheivment)
    {
        switch (acheivment)
        {
            case 0:
                return "Big Iron: It May Shoot Slow But It Still Packs A Punch";
            case 2:
                return "Bonafide Killer: You Made Your First Kill, Cold Blooded";
            case 3:
                return "Space Marine: You Found A Trusty Rifle, Use It Well";
            default:
                return "Error: acheivment not found";

        }
    }
    private void ShowAcheivment(string message)
    {
        GameObject acheivmentContainer = GameObject.FindGameObjectsWithTag("acheivment_container")[0];
        GameObject acheivmentText = GameObject.FindGameObjectsWithTag("acheivment_text")[0];
        acheivmentContainer.GetComponent<AchievmentWindowController>().FadeIn();
        acheivmentText.GetComponent<Text>().text = message;
        acheivmentText.GetComponent<AchievmentWindowController>().FadeIn();

    }
    private void DisableAcheivmentWindow()
    {
        GameObject acheivmentContainer = GameObject.FindGameObjectsWithTag("acheivment_container")[0];
        GameObject acheivmentText = GameObject.FindGameObjectsWithTag("acheivment_text")[0];
        if (acheivmentContainer.GetComponent<SpriteRenderer>().color.a > 0)
        {
            acheivmentText.GetComponent<AchievmentWindowController>().FadeOut();
            acheivmentContainer.GetComponent<AchievmentWindowController>().FadeOut();
        }

    }
}
