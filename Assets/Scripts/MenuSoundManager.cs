using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSoundManager : MonoBehaviour
{
    public AudioSource _audioSource;
    public List<AudioClip> _menuSoundFX = new List<AudioClip>();
    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    public void PlaySound(int clipToPlay)
    {
        _audioSource.clip = _menuSoundFX[clipToPlay];
        if(clipToPlay == 2)
        {
            _audioSource.time = 0.3f;
        } else
        {
            _audioSource.time = 0f;
        }
        
        _audioSource.Play();
    }

}
