using System;
using System.Collections;
using System.Collections.Generic;
using Managers.Sounds;
using UnityEngine;

public class MainMenuController : MonoBehaviour
{
    private void Start()
    {
        var soundManager = SoundManager.Instance;
        soundManager.PlayOneShoot(soundManager.MusicSource, soundManager.MusicCollection.clips[1]);
        // SoundManager.Instance.PlayClip(SoundManager.Instance.MusicSource,SoundManager.Instance.MusicCollection.clips[1],true,0);
    }
}
