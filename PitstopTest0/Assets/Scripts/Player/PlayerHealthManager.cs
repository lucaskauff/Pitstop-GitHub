using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthManager : MonoBehaviour
{
    SceneLoader sceneLoader;

    //Public
    public int playerMaxHealth = 3;
    public int playerCurrentHealth;

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