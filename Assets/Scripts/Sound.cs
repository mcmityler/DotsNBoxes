using UnityEngine.Audio;
using UnityEngine;



// Sound OBJ to hold values of sounds.

[System.Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;
    [Range(0.0f,1.0f)]
    public float volume = 1.0f;
    [Range(0.1f,3.0f)]
    public float pitch = 1.0f;

    public bool loop = false;

    [HideInInspector]
    public AudioSource source;
}
