using InventorySystem.Audio;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace InventorySystem.Audio
{
    public class AudioController
    {
        private AudioView audioView;
        private AudioDatabaseSO audioDatabase;

        public AudioController(AudioView audioViewPrefab, AudioDatabaseSO audioDatabase)
        {
            this.audioView = GameObject.Instantiate(audioViewPrefab);

            this.audioDatabase = audioDatabase;

            //InitializeAudioServices();
        }

        /*private void InitializeAudioServices()
        {
            PlaybackgroundMusic(AudioType.BACKGROUND_MUSIC, true);
        }*/

        public void PlaySoundEffects(AudioTypes audioType, bool loopSound = false)
        {
            AudioClip clip = GetSoundClip(audioType);
            if (clip != null)
            {
                audioView.audioEffects.loop = loopSound;
                audioView.audioEffects.PlayOneShot(clip);
            }
            else
            {
                Debug.LogError("No Audio Clip got selected");
            }
        }

        public void PlaybackgroundMusic(AudioTypes audioType, bool loopSound)
        {
            AudioClip clip = GetSoundClip(audioType);
            if (clip != null)
            {
                audioView.backgroundMusic.loop = loopSound;
                audioView.backgroundMusic.PlayOneShot(clip);
            }
            else
            {
                Debug.LogError("No Audio Clip got selected");
            }
        }


        private AudioClip GetSoundClip(AudioTypes audioType)
        {
            AudioClip audioClip = null;

            foreach (AudioSO audioSO in audioDatabase.audioClips)
            {
                if (audioSO.audioType == audioType)
                {
                    audioClip = audioSO.audioClip;
                    break;
                }                
            }

            return audioClip;
        }
    }
}