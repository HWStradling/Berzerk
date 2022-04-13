using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SingletonAchievmentsController : MonoBehaviour
{
    public static SingletonAchievmentsController instance { get; private set;}

    static bool[] acheivmentsArray = new bool[5];
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
    public void UnlockAcheivment(int acheivment)
    {
        if (acheivment < acheivmentsArray.Length)
        {
            switch (acheivment)
            {
                case 0:
                    if ( acheivmentsArray[acheivment] == false)
                    {
                        acheivmentsArray[acheivment] = true;
                        StartCoroutine(DisplayAcheivment("Well Done You Have Picked Up Your First Gun"));
                    }

                    break;
                default:
                    break;
            }
        } else
        {
            Debug.Log("invalid achivment index passed"); 
        }
        
    }

    IEnumerator DisplayAcheivment(string message)
    {
        Debug.Log(GameObject.FindGameObjectsWithTag("acheivment_text").Length);
        yield return new WaitUntil(() => GameObject.FindGameObjectsWithTag("acheivment_text").Length > 0);
        GameObject acheivmentContainer = GameObject.FindGameObjectsWithTag("acheivment_container")[0];
        GameObject acheivmentText = GameObject.FindGameObjectsWithTag("acheivment_text")[0];
        Debug.Log("setting new achievment message");
        acheivmentContainer.GetComponent<SpriteRenderer>().enabled = true;
        acheivmentText.GetComponent<Text>().enabled = true;
        acheivmentText.GetComponent<Text>().text = message;
        yield return new WaitForSeconds(5);
        acheivmentContainer.GetComponent<SpriteRenderer>().enabled = false;
        acheivmentText.GetComponent<Text>().enabled = false;

    }
}
