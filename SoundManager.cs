using UnityEngine.Audio;
using System;
using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour
{
    public Sound[] sound;

	// Use this for initialization
	void Awake ()
    {
		foreach (Sound s in sound)
        {
            DontDestroyOnLoad(gameObject);

            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
            s.source.outputAudioMixerGroup = s.output;
            
}
	}

    private void Start()
    {
        
    }

    public void Play(string name)
    {
       Sound s = Array.Find(sound, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " was not found!");
            Debug.Break();
            return;
        }
       s.source.Play();
    }

    public void StopSound(string name)
    {
        Sound s = Array.Find(sound, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " was not found!");
            return;
        }
        s.source.Stop();
    }
    
    public void SetStereoPan(string name, int panDir) // "name" is the name of the file, "panDir" is either left(1) or right(2) if nothing is specified pan is center
    {
        Sound s = Array.Find(sound, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " was not found!");
            return;
        }
        if (panDir == 1)
            s.source.panStereo = -1.0f;
        else if (panDir == 2)
            s.source.panStereo = 1.0f;
        else
            s.source.panStereo = 0;
    }

    public void StopLoop(string name) // loop = false
    {
        Sound s = Array.Find(sound, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " was not found!");
            return;
        }
        s.source.loop = false;
    }

    public void StartLoop(string name) // loop = true
    {
        Sound s = Array.Find(sound, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " was not found!");
            return;
        }
        s.source.loop = true;
    }

    public void ChangePitch(string name, float newPitch) // += 2nd param to the sounds pitch
    {
        Sound s = Array.Find(sound, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " was not found!");
            return;
        }
        s.source.pitch += newPitch;
    }

    public bool GetPlayState(string name) // returns true if the current sound is being played
    {
        Sound s = Array.Find(sound, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " was not found!");
        }
        return s.source.isPlaying;
    }

    public float GetLength(string name) // length of param's clip in seconds
    {
        Sound s = Array.Find(sound, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " was not found!");
        }
        return s.source.clip.length;
    }

    public float GetTime(string name) // length of param's clip in seconds
    {
        Sound s = Array.Find(sound, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " was not found!");
        }
        return s.source.time;
    }

    public void StopAllSound() // Stops current playback of ALL sounds
    {
        foreach (Sound s in sound)
        {
            s.source.Stop();
        }
    }

    public void FadeOut(string name, float fadeSeconds) // Fades the sound by subtracting volume over time
    {
        Sound s = Array.Find(sound, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " was not found!");
        }
        StartCoroutine(FadeOutRoutine(s.source, fadeSeconds));
    }

    public void ChangeOutput(string name, AudioMixerGroup AMG)
    {
        Sound s = Array.Find(sound, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " was not found!");
        }
        s.source.outputAudioMixerGroup = AMG;
    }

    IEnumerator FadeOutRoutine(AudioSource audioSource, float fadeTime) // Fades the given audioSource over fadeTime seconds
    {
        float volume = audioSource.volume;

        while (audioSource.volume > 0)
        {
            audioSource.volume -= volume * Time.deltaTime / fadeTime;

            yield return null;
        }

        audioSource.Stop();
        audioSource.volume = volume;
    }


}
