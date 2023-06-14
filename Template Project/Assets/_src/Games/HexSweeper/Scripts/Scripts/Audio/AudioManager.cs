using com.eyerunnman.patterns;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.eyerunnman.HexSweeper.Audio
{
    public class AudioManager : GameService
    {
        [SerializeField]
        AudioSource audioSourceSfx;
        [SerializeField]
        AudioSource audioSourceMusic;
        private void Awake()
        {
            ServiceLocator.Current.Register(this);
        }

        public void ToggleSound(bool status)
        {
            if (status)
            {
                audioSourceSfx.volume = 1;
                audioSourceMusic.volume = 0.25f;
            }
            else
            {
                audioSourceSfx.volume = 0;
                audioSourceMusic.volume = 0;
            }
        }

        public void PlaySfx(AudioClip audioClip)
        {
            audioSourceSfx.clip = audioClip;
            audioSourceSfx.Play();
        }
    }

}
