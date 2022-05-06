using UnityEngine;

public class EnemySoundManager : MonoBehaviour
{
    [SerializeField] private AudioSource source;
    [SerializeField] private AudioClip shoot;

    public void PlaySound(string name)
    {
        switch (name)
        {
            case "shoot":
                source.PlayOneShot(shoot);
                break;
            default:
                break;
        }
    }
}
