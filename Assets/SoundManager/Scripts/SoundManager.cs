using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using System;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

namespace Managers.Sounds
{
    public class SoundManager : MonoBehaviour
    {

        #region Inspector Variables


        [SerializeField, BoxGroup("Music"),  ShowIf(nameof(soundVariants), SoundVariants.Music)]
        private SoundCollection musicCollection;

        [SerializeField, BoxGroup("Music"), ShowIf(nameof(soundVariants), SoundVariants.Music)]
        private AudioSource musicSource;


        [SerializeField, ShowIf(nameof(soundVariants), SoundVariants.Ambient), BoxGroup("Ambient")]
        private SoundCollection ambientCollection;

        [SerializeField, ShowIf(nameof(soundVariants), SoundVariants.Ambient), BoxGroup("Ambient")]
        private AudioSource ambientSource;


        [SerializeField, ShowIf(nameof(soundVariants), SoundVariants.Player), BoxGroup("Player")]
        private SoundCollection playerCollection;

        [SerializeField, ShowIf(nameof(soundVariants), SoundVariants.Player), BoxGroup("Player")]
        private AudioSource playerSource;


        [SerializeField, ShowIf(nameof(soundVariants), SoundVariants.Enviroment), BoxGroup("Enviroment")]
        private SoundCollection enviromentCollection;

        [SerializeField, ShowIf(nameof(soundVariants), SoundVariants.Enviroment), BoxGroup("Enviroment")]
        private AudioSource enviromentSource;


        [SerializeField, ShowIf(nameof(soundVariants), SoundVariants.UI), BoxGroup("UI")]
        private SoundCollection uiCollection;

        [SerializeField, ShowIf(nameof(soundVariants), SoundVariants.UI), BoxGroup("UI")]
        private AudioSource uiSource;


        [SerializeField, ShowIf(nameof(soundVariants), SoundVariants.Alert), BoxGroup("Alert")]
        private SoundCollection alertCollection;

        [SerializeField, ShowIf(nameof(soundVariants), SoundVariants.Alert), BoxGroup("Alert")]
        private AudioSource alertSource;


        [SerializeField]
        private string audioMixerVolumePath;

        [SerializeField]
        private float changingMusicDuration = 2f;


        [SerializeField, EnumFlags,Space(20)]
        private SoundVariants soundVariants;

        #endregion Inspector Variables

        #region Properties

        public SoundCollection MusicCollection { get => musicCollection; }
        public AudioSource MusicSource { get => musicSource; }

        public SoundCollection AmbientCollection { get => ambientCollection; }
        public AudioSource AmbientSource { get => ambientSource; }

        public SoundCollection PlayerCollection { get => playerCollection; }
        public AudioSource PlayerSource { get => playerSource; }

        public SoundCollection EnviromentCollection { get => enviromentCollection; }
        public AudioSource EnviromentSource { get => enviromentSource; }

        public SoundCollection UICollection { get => uiCollection; }
        public AudioSource UISource { get => uiSource; }

        public SoundCollection AlertCollection { get => alertCollection; }
        public AudioSource AlertSource { get => alertSource; }


        public string AudioMixerVolumePath { get => audioMixerVolumePath; }

        public static SoundManager Instance { get; set; }

        #endregion Properties


        #region Initialization

        private void Awake()
        {
            if(Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(this);
            }
            else if(Instance != this)
            {
                Destroy(this.gameObject);
            }
        }

        #endregion Initialization


        #region Play Sounds

        public void PlayClip(AudioSource source, AudioClip clip, bool loop, float delay = 0)
        {
            source.loop = loop;
            StartCoroutine(DoubleFade(source, clip, 0, 2f, delay));
        }

        public void PlayClips(AudioSource source, List<AudioClip> clips, bool loop, float delay = 0)
        {
            StartCoroutine(PlayMultipleClips(source, clips, loop, delay));
        }

        public void PlayOneShoot(AudioSource source, AudioClip clip, float delay = 0)
        {
            StartCoroutine(OneShoot(source, clip, delay));
        }

        public void PlayOneShoots(AudioSource source, List<AudioClip> clip, float delay = 0)
        {
            StartCoroutine(PlayMultipleOneShoot(source, clip, delay));
        }

        #endregion Play Sounds

        #region Stop Sounds
        public void StopAudio(AudioSource source, bool individual = true)
        {
            if(individual)
                StopAllCoroutines();

            if (source.isPlaying)
                StartCoroutine(Fade(source, 0, changingMusicDuration, 0));
        }

        public void StopAll(bool everything = false)
        {
            StopAllCoroutines();
            if (everything)
            {
                StopAudio(MusicSource,false);
                StopAudio(AmbientSource, false);
                StopAudio(PlayerSource, false);
                StopAudio(EnviromentSource, false);
                StopAudio(UISource, false);
                StopAudio(AlertSource, false);
            }
        }

        #endregion Stop Sounds

        #region Muting Sounds

        public void MuteAll()
        {
            Mute(MusicCollection);
            Mute(AmbientCollection);
            Mute(PlayerCollection);
            Mute(EnviromentCollection);
            Mute(UICollection);
            Mute(AlertCollection);
        }

        public void UnMuteAll()
        {
            UnMute(MusicCollection);
            UnMute(AmbientCollection);
            UnMute(PlayerCollection);
            UnMute(EnviromentCollection);
            UnMute(UICollection);
            UnMute(AlertCollection);
        }

        public void Mute(AudioMixer mixer)
        {
            mixer.SetFloat(audioMixerVolumePath, -80);
        }

        public void Mute(SoundCollection soundCollection)
        {
            soundCollection.mixer.SetFloat(audioMixerVolumePath, -80);
        }

        public void UnMute(AudioMixer mixer)
        {
            mixer.SetFloat(audioMixerVolumePath, 0);
        }

        public void UnMute(SoundCollection soundCollection)
        {
            soundCollection.mixer.SetFloat(audioMixerVolumePath, 0);
        }

        #endregion Muting Sounds



        #region Coroutines
        private IEnumerator PlayMultipleClips(AudioSource source, List<AudioClip> clips, bool loop, float delay = 0)
        {
            yield return new WaitForSecondsRealtime(delay);

            source.loop = false;
            foreach (AudioClip clip in clips)
            {
                StartCoroutine(DoubleFade(source, clip, 0, changingMusicDuration, delay));
                yield return new WaitForSecondsRealtime(clip.length + changingMusicDuration + delay);
            }
            if (loop)
                PlayClips(source, clips, loop, delay);
        }

        private IEnumerator PlayMultipleOneShoot(AudioSource source, List<AudioClip> clips, float delay = 0)
        {
            yield return new WaitForSecondsRealtime(delay);

            foreach (AudioClip clip in clips)
            {
                source.PlayOneShot(clip);
                yield return new WaitForSecondsRealtime(clip.length);
            }
        }

        private IEnumerator OneShoot(AudioSource source, AudioClip clip, float delay)
        {
            yield return new WaitForSecondsRealtime(delay);
            source.PlayOneShot(clip);
        }

        private IEnumerator DoubleFade(AudioSource source, AudioClip clip, float targetVolume, float duration, float delay = 0)
        {
            yield return new WaitForSecondsRealtime(delay);
            float start = source.volume;

            float currentTime = 0;
            while (currentTime < duration)
            {
                currentTime += Time.unscaledDeltaTime;
                source.volume = Mathf.Lerp(start, targetVolume, currentTime / duration);
                yield return null;
            }

            source.clip = clip;
            source.Play();

            currentTime = 0;
            while (currentTime < duration)
            {
                currentTime += Time.unscaledDeltaTime;
                source.volume = Mathf.Lerp(targetVolume, start, currentTime / duration);
                yield return null;
            }
        }

        private IEnumerator Fade(AudioSource source, float targetVolume,float duration, float delay = 0)
        {
            yield return new WaitForSecondsRealtime(delay);

            float start = source.volume;
            float currentTime = 0;

            while(currentTime < duration)
            {
                currentTime += Time.unscaledDeltaTime;
                source.volume = Mathf.Lerp(start, targetVolume, currentTime / duration);
                yield return null;
            }
            source.Stop();
        }
        

        #endregion Coroutines

        #region Enums
        [Flags]
        private enum SoundVariants
        {
            Music = 1,
            Ambient = 2,
            Player = 4,
            Enviroment = 8,
            UI = 16,
            Alert = 32,
        }

        #endregion Enums

    }
}


