using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AndioManager : Singleton<AndioManager>
{
    [SerializeField] private AudioSource backMusic;
    [SerializeField] private AudioSource playerMusic;

    private void Start()
    {
        SwitchBackMusic("common");
    }
    public void SwitchBackMusic(string name)
    {
        backMusic.clip = Resources.Load<AudioClip>(name);
        backMusic.Play();
    }

    public void SwitchPlayerMusic(string name)
    {
        var audioClip = Resources.Load<AudioClip>(name);
        playerMusic.PlayOneShot(audioClip);
    }
}
