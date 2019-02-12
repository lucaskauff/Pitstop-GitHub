using System.Collections;
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
    
    //Enemy
    public Slider enemyHealthBar;
    public EnemyHealthManager enemyHealthMan;

    void Start()
    {
        inputManager = GameManager.Instance.inputManager;
    }

    void Update()
    {
        scanProgressBar.GetComponent<Animator>().SetInteger("ScanProgress", crystalControl.scanProgress);
        playerLifes.GetComponent<Animator>().SetInteger("PlayerHealth", playerHealthMan.playerCurrentHealth);

        enemyHealthBar.maxValue = enemyHealthMan.enemyMaxHealth;
        enemyHealthBar.value = enemyHealthMan.enemyCurrentHealth;
    }

    public void ChangeImageInCrystalSlot(Sprite sprite)
    {
        crystalSlot.GetComponent<Image>().color = Color.white;
        crystalSlot.GetComponent<Image>().sprite = sprite;
    }
}