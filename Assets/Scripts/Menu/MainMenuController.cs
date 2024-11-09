using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] GameObject optionsMenu;
    [SerializeField] Animator animator;

    private void Awake()
    {
        SaveSystem.LoadGame(); // loads game player data from save file, creates new if null.
        
    }
    private void OnEnable()
    {
        animator.Play("fade_in");
    }
    public void Close()
    {
        animator.Play("fade_out");

    }
    public void EnableOptionsMenu()
    {
        optionsMenu.SetActive(true);
    }
    public void DisableMainMenu()
    {
        gameObject.SetActive(false);
    }

    public void StartGame()
    {
        // handle profile selection, + loading
        if (SaveSystem.LoadGame())
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        } 
        

    }

    // exits the application.
    public void QuitGame()
    {
        Application.Quit();
    }
}
