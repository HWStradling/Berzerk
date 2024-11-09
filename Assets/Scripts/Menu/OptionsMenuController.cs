using UnityEngine;
using UnityEngine.UI;

public class OptionsMenuController : MonoBehaviour
{
    [SerializeField] GameObject mainMenu;
    [SerializeField] Animator animator;
    [SerializeField] Slider volumeSlider;
    private void OnEnable()
    {
        animator.Play("fade_in");
        volumeSlider.value = PlayerPrefs.GetFloat("volume", 1f);

  /*      if (!PlayerPrefs.HasKey("profile"))
        {
            PlayerPrefs.SetString("profile", "Profile 1");
        }*/
    }
    public void Close()
    {
        PlayerPrefs.SetFloat("volume", volumeSlider.value);
        animator.Play("fade_out");
    }
    public void EnableMainMenu()
    {
        mainMenu.SetActive(true);
    }
    public void DisableOptionsMenu()
    {
        gameObject.SetActive(false);
    }

    public void OnVolumeChanged()
    {
        AudioListener.volume = volumeSlider.value;
    }

}
