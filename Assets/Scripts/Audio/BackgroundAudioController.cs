using UnityEngine;

public class BackgroundAudioController : MonoBehaviour
{
    [SerializeField] AudioClip[] tracks;
    [SerializeField] AudioSource source;
    private AudioClip previousTrack;

    private void Start()
    {
        // sets audio listener volume from player prefs,
        AudioListener.volume = PlayerPrefs.GetFloat("volume", 1f);
    }
    // Update is called once per frame
    void Update()
    {// starts new track when one finishes,
        if (!source.isPlaying)
        {
            source.clip = tracks[Random.Range(0, tracks.Length)];// gets random track,
            // while new track is the same as the previous track, get a new one,
            while (source.clip == previousTrack)
            {
                source.clip = tracks[Random.Range(0, tracks.Length)];
            }
            previousTrack = source.clip;//save previous track as new track,
            source.Play();
        }
    }
}
