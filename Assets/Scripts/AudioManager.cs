
using UnityEngine;

// Classe pour gérer la lecture d'une liste de lecture audio
public class AudioManager : MonoBehaviour

{
    public AudioClip[] playlist;
    public AudioSource audioSource;
    private int musicIndex = 0;

    // Au démarrage, configurez le clip audio source pour jouer la première piste de la liste de lecture
    void Start()
    {
        audioSource.clip = playlist[0];
        audioSource.Play();
    }

    void Update()
    {
        // Vérifiez si la piste audio est terminée
        if (!audioSource.isPlaying)
        {
            PlayNextSong();
        }
    }

    // Méthode pour passer à la prochaine piste de la liste de lecture
    void PlayNextSong()
    {
        musicIndex = (musicIndex + 1) % playlist.Length;
        audioSource.clip = playlist[musicIndex];
        audioSource.Play();
    }
}
