using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [SerializeField]
    private Sound[] _musicSounds, _sfxSounds;

    [SerializeField]
    private AudioSource _musicSource, _sfxSource;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayMusic(string name)
    {
        Sound s = Array.Find(_musicSounds, x => x.Name == name);

        if (s == null)
        {
            Debug.Log($"Music \"{name}\" not found.");
        }
        else
        {
            _musicSource.clip = s.Clip;
            _musicSource.Play();
        }
    }

    public void PlaySFX(string name)
    {
        Sound s = Array.Find(_sfxSounds, x => x.Name == name);

        if (s == null)
        {
            Debug.Log($"SFX sound \"{name}\" not found.");
        }
        else
        {
            _sfxSource.clip = s.Clip;
            _sfxSource.PlayOneShot(s.Clip);
        }
    }
}
