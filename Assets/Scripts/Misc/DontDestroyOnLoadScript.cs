using UnityEngine;
using UnityEngine.SceneManagement;

public class DontDestroyOnLoadScript : MonoBehaviour
{
    static public bool alreadyCreatedVCam = false;
    static public bool alreadyCreatedPlayer = false;
    static public bool alreadyCreatedMCam = false;
    static public bool alreadyCreatedUI = false;
    void Start()
    {
        Debug.Log("player: " + alreadyCreatedPlayer);
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
        }
        else if (!alreadyCreatedUI && gameObject.CompareTag("ui"))
        {
            DontDestroyOnLoad(gameObject);
            alreadyCreatedUI = true;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnDisable ()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
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
            //Debug.Log("other scene");
            gameObject.SetActive(true);
        }
    }
}
