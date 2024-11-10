using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Audio_Manager : MonoBehaviour
{
    public AudioClip menuMusic;
    public AudioClip gameMusic;
    public AudioClip winMusic;
    public AudioClip loseMusic;
    private AudioSource audioSource;

    private static Audio_Manager instance;

    void Awake()
    {
        // Verifico si hay un MusicManager y destruyo el duplicado
        if (instance != null && instance != this)
        {
            Destroy(gameObject); 
            return;
        }

        //hago una unica instancia del audio manager
        instance = this;
        DontDestroyOnLoad(gameObject);
        audioSource = GetComponent<AudioSource>();
        PlayMusic(menuMusic);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    //cargamos las musicas segun que escena es
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        int sceneIndex = scene.buildIndex;

        if (sceneIndex == 1) 
        {
            PlayMusic(gameMusic);
        }
        else if (sceneIndex == 3) 
        {
            PlayMusic(winMusic);
        }
        else if (sceneIndex == 0 || sceneIndex == 2 || sceneIndex == 5 || sceneIndex == 6) 
        {
            PlayMusic(menuMusic);
        }
        else if (sceneIndex == 4)
        {
            PlayMusic(loseMusic);
        }
    }

    private void PlayMusic(AudioClip clip)
    {
        if (audioSource.clip == clip) return;  // Evita reiniciar la música si es la misma
        audioSource.clip = clip;
        audioSource.Play();
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
