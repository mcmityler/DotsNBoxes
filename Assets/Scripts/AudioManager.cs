using UnityEngine.Audio;
using System;
using UnityEngine;
// use GameObject.FindObjectOfType<AudioManager>().Play("startClick"); to play sounds.
public class AudioManager : MonoBehaviour
{
    public Sound[] sounds; //array of sounds in game

    public static AudioManager instance;  //makes instance of audio manager to make sure there is only one when you bring it between scenes.
    void Awake()
    {
        //this below works because if one is already active it doesnt need to go through the awake function again.
        if(instance == null){ //check if there is an audio manager, if not make this the audio manager
            instance = this; 
        }
        else{ //if audio manager already exists destroy this audio manager.
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject); //make sure to keep audio manager when changing scenes.

        //add sounds from array to an audio source. && use inspector values.
        foreach (Sound s in sounds){
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    //Call this function with the name you input in inspector to play sounds. 
    public void Play (string name){
        Sound s = Array.Find(sounds, sound => sound.name == name); //find sound with name passed by ref.
        if(s == null){ //if no sound with this name then error message and return.
            Debug.LogWarning("sound named" + name + " wasnt found");
            return;
        }
        s.source.Play(); //play sound if it exists.
    }
}
