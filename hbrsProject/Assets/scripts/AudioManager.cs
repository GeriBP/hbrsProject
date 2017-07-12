using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

    AudioClip[] sounds;
    [SerializeField]
    AudioClip[] musics;
    bool playingEndMusic = false;

    [SerializeField]
    AudioSource[] sources;

    public enum SFX
    {
        Jump,
        Pistol,
        //Rifle,
        //Reload,
        //PickUp,
        //EnemyDeath,
        //PlayerDeath,

        EnumSize
    }

    public enum Music
    {
        //Menu,
        Game,

        EnumSize
    }

    private void Awake()
    {
        DontDestroyOnLoad(transform);
        int sourcesIndex = 0;
        sources = new AudioSource[(int)SFX.EnumSize + (int)Music.EnumSize];
        for (int i = 0; i < sources.Length; i++)
        {
            sources[i] = gameObject.AddComponent<AudioSource>();
            sources[i].playOnAwake = false;
        }

        sounds = new AudioClip[(int)SFX.EnumSize];

        for (int i = 0; i < (int)SFX.EnumSize; i++)
        {
            SFX sfx = (SFX)i;
            sounds[i] = (AudioClip)Resources.Load("Audio/Sounds/" + sfx.ToString());
            sources[sourcesIndex].clip = sounds[i];
            ++sourcesIndex;
        }

        musics = new AudioClip[(int)Music.EnumSize];
        for (int i = 0; i < (int)Music.EnumSize; i++)
        {
            Music mus = (Music)i;
            musics[i] = (AudioClip)Resources.Load("Audio/Music/" + mus.ToString());
            sources[sourcesIndex].clip = musics[i];
            sources[sourcesIndex].loop = true;
            ++sourcesIndex;
        }

        ResetMusics();

    }


    void ResetMusics()
    {
        for (int i = (int)SFX.EnumSize; i < sources.Length; i++)
        {
            sources[i].Play();
            sources[i].volume = 0;
        }
    }

    public void PlaySoundIfNotPlaying(SFX sound)
    {
        if (!sources[(int)sound].isPlaying)
            sources[(int)sound].Play();
    }

    public void PlaySound(SFX sound)
    {
        sources[(int)sound].Play();
    }

    public void StopSound(SFX sound)
    {
        if(sources[(int)sound].isPlaying)
            sources[(int)sound].Stop();
    }

    public void StopAllMusic()
    {
        for (int i = (int)SFX.EnumSize; i < sources.Length; i++)
        {
            sources[i].volume = 0;
        }
    }

    private static AudioManager _instance;
    public static AudioManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = (AudioManager)FindObjectOfType(typeof(AudioManager));
            }
            return _instance;
        }
    }
}
