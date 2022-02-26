using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // called when start game button pressed, loads the next scene in the build index.
    public void StartGame ()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

    }

    // exits the application.
    public void QuitGame ()
    {
        Application.Quit();
    }

    //displays the options menu UI.
    public void DisplayOptionsMenu ()
    {
        //TODO implement options menu.
    }
}
