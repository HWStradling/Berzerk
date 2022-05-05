using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SingletonAchievmentsController : MonoBehaviour
{
    public static SingletonAchievmentsController instance { get; private set;}

    static bool[] acheivmentsArray = new bool[20];
    static Queue<int> acheivmentsQueue = new Queue<int>();
    static bool running = false;
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
        }
    }
    private void FixedUpdate()
    {
        if (!running && acheivmentsQueue.Count > 0)
        {
            running = true;
            Debug.Log("acheivmenets coroutine running");
            StartCoroutine(DisplayAcheivment());
        }
    }
    public void UnlockAcheivment(int acheivment)
    {
        if (acheivment < acheivmentsArray.Length && acheivmentsArray[acheivment] == false)
        {
            acheivmentsQueue.Enqueue(acheivment);
        } else
        {
            Debug.Log("invalid achivment index passed or acheivment already unlocked"); 
        }
    }

    IEnumerator DisplayAcheivment()
    {
        yield return new WaitUntil(() => GameObject.FindGameObjectsWithTag("acheivment_container").Length > 0);
        GameObject acheivmentContainer = GameObject.FindGameObjectsWithTag("acheivment_container")[0];
        GameObject acheivmentText = GameObject.FindGameObjectsWithTag("acheivment_text")[0];

        int newAchievment = acheivmentsQueue.Dequeue();
        string message = getAcheivmentMessage(newAchievment);
        ShowAcheivment(message, acheivmentContainer, acheivmentText);
        yield return new WaitForSeconds(8);
        DisableAcheivmentWindow(acheivmentContainer, acheivmentText);
        running = false;
    }

    private string getAcheivmentMessage(int acheivment)
    {
        switch (acheivment)
        {
            case 0:
                return "Armed And Dangerous: Well Done You Have Picked Up Your First Gun";
            case 2:
                return "Bonafide Killer: You Made Your First Kill, Cold Blooded";
            case 3:
                return "Space Marine: You Found A Trusty Rifle, Use It Well";
            default:
                return "Error: acheivment not found";
                
        }
    }
    private void ShowAcheivment(string message, GameObject acheivmentContainer, GameObject acheivmentText)
    {
        //acheivmentContainer.GetComponent<SpriteRenderer>().enabled = true;
        //acheivmentText.GetComponent<Text>().enabled = true;
        acheivmentContainer.GetComponent<AchievmentWindowController>().FadeIn();
        acheivmentText.GetComponent<Text>().text = message;
        acheivmentText.GetComponent<AchievmentWindowController>().FadeIn();
        
    }
    private void DisableAcheivmentWindow(GameObject acheivmentContainer, GameObject acheivmentText)
    {
        acheivmentText.GetComponent<AchievmentWindowController>().FadeOut();
        acheivmentContainer.GetComponent<AchievmentWindowController>().FadeOut();
        //acheivmentContainer.GetComponent<SpriteRenderer>().enabled = false;
        //acheivmentText.GetComponent<Text>().enabled = false;
    }
}
