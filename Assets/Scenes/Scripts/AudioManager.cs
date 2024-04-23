using UnityEngine.Audio;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;
    public static AudioManager instance;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Play specific music based on the scene name
        switch (scene.name)
        {
            case "Scene0-StartScreen":
                Play("MainMenu"); // Adjust with your actual sound name for the main menu
                break;
            case "Scene1.5-HomeBase":
                Stop("MainMenu");
                Play("Chill");
                Stop("Theme");
                Stop("CaveDrip");
                Stop("InsideCave");
                Stop("OutsideWind");
                break;
            case "Scene2.5-Environment":
                Play("OutsideWind");
                Play("Theme");
                Stop("Chill");
                Stop("CaveDrip");
                Stop("InsideCave");
                break;
            case "Scene2-Environment":
                Play("OutsideWind");
                Play("Theme");
                Stop("Chill");
                Stop("CaveDrip");
                Stop("InsideCave");
                break;
            case "Scene3-CaveInterior":
                Play("CaveDrip");
                Play("InsideCave");
                Stop("MainMenu");
                Stop("OutsideWind");
                Stop("Chill");
                break;
            default:
                // Optional: Play a default theme, or do nothing
                Play("Theme");
                break;
        }
    }
    void OnDestroy()
    {
        // Unsubscribe to ensure no memory leaks
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }


    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }
        s.source.Play();
    }
    public void Stop(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }
        s.source.Stop();
    }
}
