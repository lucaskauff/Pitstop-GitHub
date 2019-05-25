using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace Pitstop
{
    public class VolumeValue_Tweaker : MonoBehaviour
    {
        GameManager gameManager;

        private AudioSource audioSrc;
        private float musicVolume = 1f;
        public Slider musicSlider;
        void Start()
        {
            gameManager = GameManager.Instance;

            audioSrc = GetComponent<AudioSource>();
        }

        void Update()
        {
            audioSrc.volume = gameManager.musicVolume;
            musicSlider.value = gameManager.musicVolume;
        }

        public void SetVolume(float vol)
        {
            //musicVolume = vol;
            gameManager.musicVolume = vol;
        }
    }
}