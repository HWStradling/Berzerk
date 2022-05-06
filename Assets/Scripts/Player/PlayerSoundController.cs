using UnityEngine;

public class PlayerSoundController : MonoBehaviour
{
    [SerializeField] AudioSource source;
    [SerializeField] AudioClip shoot, reload, hit, die, pickup;
    // Start is called before the first frame update
    public void PlaySound(string name)
    {
        switch (name)
        {
            case "shoot":
                source.PlayOneShot(shoot);
                break;
            case "reload":
                source.PlayOneShot(reload);
                break;
            case "hit":
                source.PlayOneShot(hit);
                break;
            case "die":
                source.PlayOneShot(die);
                break;
            case "pickup":
                source.PlayOneShot(pickup);
                break;
            default:
                break;
        }
    }
}
