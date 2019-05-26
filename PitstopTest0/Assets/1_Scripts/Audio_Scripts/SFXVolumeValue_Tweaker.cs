using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;


namespace Pitstop
{
    public class SFXVolumeValue_Tweaker : MonoBehaviour
    {
        GameManager gameManager;

        private AudioSource audioSrc;
        private float sfxVolume = 1f;
        public Slider sfxSlider;

        [SerializeField, Range(0, 1)] float amount = 1;

        void Start()
        {
            gameManager = GameManager.Instance;

            audioSrc = GetComponent<AudioSource>();
        }

        void Update()
        {
            audioSrc.volume = gameManager.sfxVolume * amount;
            sfxSlider.value = gameManager.sfxVolume;
        }

        public void SetVolume(float vol)
        {
            gameManager.sfxVolume = vol;
        }
    }

}
