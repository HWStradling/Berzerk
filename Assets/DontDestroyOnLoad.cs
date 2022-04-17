using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DontDestroyOnLoad : MonoBehaviour
{
    static private bool alreadyCreatedVCam = false;
    static private bool alreadyCreatedPlayer = false;
    static private bool alreadyCreatedMCam = false;
    static private bool alreadyCreatedUI = false;
    void Start()
    {
        if (!alreadyCreatedVCam && gameObject.CompareTag("v_cam"))
        {
            DontDestroyOnLoad(gameObject);
            alreadyCreatedVCam = true;
        }
        else if (!alreadyCreatedMCam && gameObject.CompareTag("m_cam"))
        {
            DontDestroyOnLoad(gameObject);
            alreadyCreatedMCam = true;
        }
        else if (!alreadyCreatedPlayer && gameObject.CompareTag("Player"))
        {
            DontDestroyOnLoad(gameObject);
            alreadyCreatedPlayer = true;
        } else if (!alreadyCreatedUI && gameObject.CompareTag("ui"))
        {
            DontDestroyOnLoad(gameObject);
            alreadyCreatedUI= true;
        }
        else
        {
            Destroy(gameObject);
        }

    }
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;

        if (gameObject.CompareTag("v_cam"))
        {
            alreadyCreatedVCam = false;
        }
        else if (gameObject.CompareTag("m_cam"))
        {
            alreadyCreatedMCam = false;
        }
        else if (gameObject.CompareTag("Player"))
        {
            alreadyCreatedPlayer = false;
        }
        else if (gameObject.CompareTag("ui"))
        {
            alreadyCreatedUI = false;
        }
    }


    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "MenuScene")
        {
            Debug.Log("menu scene");
            /*Destroy(gameObject);*/
        }
        else
        {
            Debug.Log("other scene");
            gameObject.SetActive(true);
        }
    }
}
