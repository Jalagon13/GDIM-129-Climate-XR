using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace MagnetFishing
{
    public class AudioTestingScript : MonoBehaviour
    {
        [SerializeField] private AudioClip _ambientSound;

        private static int _audioSourceNum = 10;
        private static List<AudioSource> _audioSourcesList = new List<AudioSource>();

        private static AudioTestingScript _instance;

        private void Awake()
        {
            _instance = this;
        }

        private void OnEnable()
        {
            if (transform.childCount > 0) return;

            for (int i = 0; i < _audioSourceNum; i++)
            {
                var audioSourceGameObject = new GameObject("AudioSource " + i);
                audioSourceGameObject.transform.SetParent(transform);
                var audioSource = audioSourceGameObject.AddComponent<AudioSource>();
                audioSource.playOnAwake = false;
                _audioSourcesList.Add(audioSource);
            }
        }

        private void OnDisable()
        {
            _audioSourcesList = new();
        }

        private IEnumerator Start()
        {
            yield return new WaitForSeconds(1f);
            PlayClip(_ambientSound, true, false);
        }

        public static void PlayClip(AudioClip clipToPlay, bool looping, bool randPitch, float volume = 0.5f, float pitch = 1f)
        {
            if (clipToPlay == null) 
                throw new Exception("Cannot PLAY " + clipToPlay.name + " because it's null");

            foreach (AudioSource source in _audioSourcesList)
            {
                if (source.clip == null || source.clip == clipToPlay)
                {
                    source.clip = clipToPlay;
                    AudioHandle(looping, randPitch, source, clipToPlay, volume, pitch);
                    return;
                }
            }

            throw new Exception("Cannot PLAY " + clipToPlay.name + " because there are no available Audio Sources to play from");
        }

        private static void AudioHandle(bool looping, bool randPitch, AudioSource source, AudioClip clip, float volume, float pitch)
        {
            pitch = randPitch ? Random.Range(pitch - 0.1f, pitch + 0.6f) : pitch;
            source.volume = Mathf.Min(1, volume);
            source.pitch = Mathf.Min(3, pitch);
            source.loop = looping;

            if (looping)
            {
                if (!source.isPlaying)
                {
                    Debug.Log("Source playing looped");
                    source.Play();
                }
            }
            else
            {
                _instance.StartCoroutine(ClipHandle(source, clip, source.clip.length, pitch, volume));
            }
        }

        private static IEnumerator ClipHandle(AudioSource source, AudioClip clip, float clipLength, float pitch, float volume)
        {
            source.PlayOneShot(clip, Mathf.Min(1, volume));
            yield return new WaitForSeconds(clipLength);
            source.clip = null;
        }
    }
}
