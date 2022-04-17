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
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else if (!alreadyCreatedMCam && gameObject.CompareTag("m_cam"))
        {
            DontDestroyOnLoad(gameObject);
            alreadyCreatedMCam = true;
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else if (!alreadyCreatedPlayer && gameObject.CompareTag("Player"))
        {
            DontDestroyOnLoad(gameObject);
            alreadyCreatedPlayer = true;
            SceneManager.sceneLoaded += OnSceneLoaded;
        } else if (!alreadyCreatedUI && gameObject.CompareTag("ui"))
        {
            DontDestroyOnLoad(gameObject);
            alreadyCreatedUI= true;
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }

    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "MenuScene")
        {
            Debug.Log("menu scene");
            gameObject.SetActive(false);
        }
        else
        {
            Debug.Log("other scene");
            gameObject.SetActive(true);
        }
    }
}
