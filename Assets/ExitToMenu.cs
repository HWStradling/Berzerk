using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitToMenu : MonoBehaviour
{
    public void exitToMenu()
    {
        Debug.Log("exit clicked");
        if (!SaveSystem.IsSaved)
        {
            //save game prompt

        } else
        {
            GameObject player = GameObject.FindGameObjectsWithTag("Player")[0];
            GameObject uI = GameObject.FindGameObjectsWithTag("ui")[0];
            GameObject vCam = GameObject.FindGameObjectsWithTag("v_cam")[0];
            GameObject mCam = GameObject.FindGameObjectsWithTag("m_cam")[0];
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

            foreach (var enemy in enemies)
            {
                Destroy(enemy);
            }
            Destroy(vCam);
            Destroy(mCam);
            Destroy(uI);
            Destroy(player);

            SceneManager.LoadScene(0);

        }
    }
}
