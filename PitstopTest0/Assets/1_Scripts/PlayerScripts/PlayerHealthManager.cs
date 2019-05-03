﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.Rendering.PostProcessing;

namespace Pitstop
{
    public class PlayerHealthManager : MonoBehaviour
    {
        //GameManager
        SceneLoader sceneLoader;

        [Header("My Components")]
        //to use on player taking dmg
        //[SerializeField] Animator myAnim = default;
        [SerializeField] CinemachineImpulseSource myImpulseSource = default;

        [Header("Public Variables")]
        public float playerMaxHealth = 3;
        public float playerCurrentHealth;

        [Header("Serializable")]
        [SerializeField] PostProcessVolume postProRedVignette = default;
        [SerializeField] float feedbackLength = 0.5f;
        [SerializeField] float maxBloodSize = 1;
        [SerializeField] float feedbackSpeed = 1;

        //Private
        Vignette vignetteLayer = null;
        float timer = 0f;
        bool hasToRecover = false;
        bool feedbackLaunched = false;

        void Start()
        {
            sceneLoader = GameManager.Instance.sceneLoader;

            playerCurrentHealth = playerMaxHealth;
        }

        void Update()
        {
            if (playerCurrentHealth <= 0)
            {
                sceneLoader.ReloadScene();
            }

            if (hasToRecover && !feedbackLaunched)
            {
                StartCoroutine(GotAttacked());
                feedbackLaunched = true;
            }
            else
            {
                StopCoroutine(GotAttacked());
                feedbackLaunched = false;
            }
        }

        public void HurtPlayer(int damageToGive)
        {
            playerCurrentHealth -= damageToGive;
            hasToRecover = true;
        }

        public void HealPlayer(int healToGive)
        {
            playerCurrentHealth += healToGive;
        }

        public void ResetHealth()
        {
            playerCurrentHealth = playerMaxHealth;
        }

        //not perfect yet
        IEnumerator GotAttacked()
        {
            myImpulseSource.GenerateImpulse();

            postProRedVignette.profile.TryGetSettings(out vignetteLayer);
            vignetteLayer.intensity.value = Mathf.Lerp(0, maxBloodSize, timer);
            timer += feedbackSpeed * Time.deltaTime;

            yield return new WaitForSeconds(feedbackLength);

            vignetteLayer.intensity.value = 0;
            timer = 0;
            hasToRecover = false;
        }
    }
}