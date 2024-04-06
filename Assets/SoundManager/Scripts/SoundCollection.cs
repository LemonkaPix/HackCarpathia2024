using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[CreateAssetMenu(menuName = "Managers/SoundManger/SoundCollection")]
public class SoundCollection : ScriptableObject
{
    public AudioMixer mixer;
    public List<AudioClip> clips = new List<AudioClip>();
}
