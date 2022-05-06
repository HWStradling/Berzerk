using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitToMenu : MonoBehaviour
{
    public void exitToMenu()
    {
        DestroyForMenu();
        SceneManager.LoadScene(0);
        
    }
    public static void DestroyForMenu()
    {
        GameObject player = GameObject.FindGameObjectsWithTag("Player")[0];
        GameObject uI = GameObject.FindGameObjectsWithTag("ui")[0];
        GameObject vCam = GameObject.FindGameObjectsWithTag("v_cam")[0];
        GameObject mCam = GameObject.FindGameObjectsWithTag("m_cam")[0];
        /* GameObject bgAudio = GameObject.FindGameObjectsWithTag("bg_audio")[0];*/
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (var enemy in enemies)
        {
            Destroy(enemy);
        }

        DontDestroyOnLoadScript.alreadyCreatedVCam = false;
        DontDestroyOnLoadScript.alreadyCreatedMCam = false;
        DontDestroyOnLoadScript.alreadyCreatedPlayer = false;
        DontDestroyOnLoadScript.alreadyCreatedUI = false;

        Destroy(vCam);
        Destroy(mCam);
        Destroy(uI);
        Destroy(player);
    }
}
