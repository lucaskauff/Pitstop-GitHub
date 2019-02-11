﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public InputManager inputManager;

    //Crystal
    public Image scanProgressBar;
    public Image crystalSlot;
    public CrystalController crystalControl;

    public Image playerLifes;
    public PlayerHealthManager playerHealthMan;

    /*
    public Slider enemyHealthBar;
    public EnemyHealthManager enemyHealth;
    */

    void Start()
    {
        inputManager = GameManager.Instance.inputManager;
    }

    void Update()
    {
        scanProgressBar.GetComponent<Animator>().SetInteger("ScanProgress", crystalControl.scanProgress);
        playerLifes.GetComponent<Animator>().SetInteger("PlayerHealth", playerHealthMan.playerCurrentHealth);

        /*
        playerHealthBar.maxValue = playerHealth.playerMaxHealth;
        playerHealthBar.value = playerHealth.playerCurrentHealth;

        enemyHealthBar.maxValue = enemyHealth.enemyMaxHealth;
        enemyHealthBar.value = enemyHealth.enemyCurrentHealth;
        */
    }

    public void ChangeImageInCrystalSlot(Sprite sprite)
    {
        crystalSlot.GetComponent<Image>().color = Color.white;
        crystalSlot.GetComponent<Image>().sprite = sprite;
    }
}