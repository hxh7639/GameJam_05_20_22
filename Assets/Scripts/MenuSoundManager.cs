using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSoundManager : MonoBehaviour
{
    public AudioSource _audioSource;
    public List<AudioClip> _menuSoundFX = new List<AudioClip>();
    public List<AudioClip> _storySoundFX = new List<AudioClip>();
    public AudioClip _volumeAdjustmentSong;
    public PersistGameManager _persistGameManager;

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    public void PlayVolumeAdjustmentSong()
    {
        _audioSource.clip = _volumeAdjustmentSong;
        _audioSource.Play();
        Debug.Log("PlayVolumeAdjustmentSong");
    }

    public void PlayMenuSound(int clipToPlay)
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

    public IEnumerator PlayStorySound()
    {
        _audioSource.Stop();
        _audioSource.clip = _storySoundFX[0];
        _audioSource.time = 0;
        _audioSource.Play();
        yield return new WaitForSeconds(_audioSource.clip.length - 6.5f);

        _audioSource.clip = _storySoundFX[1];
        _audioSource.time = 0;
        _audioSource.Play();
        yield return new WaitForSeconds(_audioSource.clip.length);

        _audioSource.clip = _storySoundFX[2];
        _audioSource.Play();
        yield return new WaitForSeconds(_audioSource.clip.length - 4);

        FindObjectOfType<StoryManager>().DeathStoryStart();
        _persistGameManager.FadeIn(2);
        
    }

    public void PlayStorySFX(int clipToPlay)
    {
        _audioSource.clip = _storySoundFX[clipToPlay];   
        if(clipToPlay == 3)
        {
            _audioSource.time = Random.Range(3f, 7f);
        } else if(clipToPlay == 5)
        {
            _audioSource.time = 16.2f;
        } else
        {
            _audioSource.time = 0f;
        }     

        _audioSource.Play();
    }

    public void StopStorySFX()
    {
        _audioSource.Stop();
    }

}
