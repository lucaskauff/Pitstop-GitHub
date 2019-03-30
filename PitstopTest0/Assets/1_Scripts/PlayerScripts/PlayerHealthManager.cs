using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pitstop
{
    public class PlayerHealthManager : MonoBehaviour
    {
        SceneLoader sceneLoader;

        SpriteRenderer myRenderer;

        //Public
        public int playerMaxHealth = 3;
        public int playerCurrentHealth;

        void Start()
        {
            sceneLoader = GameManager.Instance.sceneLoader;

            myRenderer = GetComponent<SpriteRenderer>();

            playerCurrentHealth = playerMaxHealth;
        }

        void Update()
        {
            if (playerCurrentHealth <= 0)
            {
                sceneLoader.ReloadScene();
            }
        }

        public void HurtPlayer(int damageToGive)
        {
            playerCurrentHealth -= damageToGive;
        }

        public void HealPlayer(int healToGive)
        {
            playerCurrentHealth += healToGive;
        }

        public void ResetHealth()
        {
            playerCurrentHealth = playerMaxHealth;
        }
    }
}