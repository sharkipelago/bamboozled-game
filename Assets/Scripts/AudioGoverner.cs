using UnityEngine.Audio;
using UnityEngine;
using System;

public class AudioGoverner : MonoBehaviour
{
    public Sound[] sounds;

    //[SerializeField] AudioSource test;

    private void Awake()
    {
        foreach (Sound _sound in sounds)
        {
            _sound.source = gameObject.AddComponent<AudioSource>();
            //AudioSource thisSource = _sound.source;
            _sound.source.clip = _sound.clip;

            _sound.source.volume = _sound.volume;
            _sound.source.pitch = 1;
            _sound.source.loop = _sound.loop;
        }

        Debug.Log("Finished Loading Sounds");
   
    }

    private void Start()
    {
        Debug.Log("Trying to play sounds");
        PlaySound("Background Music");
    }

    public void PlaySound(string name)
    {
        Sound targetSound = Array.Find(sounds, _sound => _sound.name == name);
        if(targetSound == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }
        targetSound.source.volume *= PlayerPrefs.GetFloat("Volume");
        targetSound.source.Play();
        //test.Play();
        //Debug.Log("Source:" + targetSound.source + "Name:" + targetSound.name);
    }

}
