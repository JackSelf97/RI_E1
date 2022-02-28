using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
public class Sound_Script
{
    public string name;
    public AudioClip clip;

    [Range(0f,1)]
    public float volume;
    [Range(0.1f, 3f)]
    public float pitch;

    public bool loop;

    [HideInInspector]
    public AudioSource source;
}
