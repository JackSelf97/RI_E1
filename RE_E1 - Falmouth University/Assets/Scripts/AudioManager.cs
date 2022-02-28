using UnityEngine.Audio;
using System;
using UnityEngine;

//https://www.youtube.com/watch?v=6OT43pvUyfY&ab_channel=Brackeys
// Not static! (because we use one scene)
public class AudioManager : MonoBehaviour
{
    public Sound_Script[] sounds;

    void Awake()
    {
        foreach (Sound_Script item in sounds)
        {
            item.source = gameObject.AddComponent<AudioSource>();
            item.source.clip = item.clip;

            item.source.volume = item.volume;
            item.source.pitch = item.pitch;
            item.source.loop = item.loop;
        }
    }

    private void Start()
    {
        PlaySound("Theme"); // Looping theme music
    }

    public void PlaySound(string name)
    {
        Sound_Script item = Array.Find(sounds, sound => sound.name == name); // find the name in the array of 'sounds'

        if (item == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }

        item.source.Play();
    }
}
