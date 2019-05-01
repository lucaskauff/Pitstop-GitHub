using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Pitstop
{
    public class UIManager : MonoBehaviour
    {
        //GameManager
        SceneLoader sceneLoader;

        [Header("Player Components")]
        [SerializeField] CrystalController crystalController = default;
        [SerializeField] PlayerHealthManager playerHealthMan = default;

        [Header("Crystal UI Elements")]
        [SerializeField] Animator crystalAnim = default;
        [SerializeField] Image scanBarFill = default;
        [SerializeField] Image crystalSlot = default;

        [Header("Player HP Elements")]
        [SerializeField] Animator playerLife = default;
        [SerializeField] Image playerLifeBarFill = default;

        //Private
        private bool playerLifeAppearedCheck = false;

        void Start()
        {
            sceneLoader = GameManager.Instance.sceneLoader;
        }

        void Update()
        {
            SpecificScenesEvents();

            UpdateCrystalUI();

            UpdatePlayerLife();
        }

        public void SpecificScenesEvents()
        {
            if (sceneLoader.activeScene != "1_TEMPLE")
            {
                crystalAnim.SetBool("AlreadyThere", true);

                if (!playerLifeAppearedCheck)
                {
                    playerLife.SetTrigger("Appear");
                    playerLifeAppearedCheck = true;
                }
            }
            else if (sceneLoader.activeScene == "3_VILLAGE")
            {
                crystalAnim.SetBool("AlreadyThere", true);

                if (!playerLifeAppearedCheck)
                {
                    playerLife.SetTrigger("Disappear");
                    playerLifeAppearedCheck = true;
                }
            }
        }

        public void UpdateCrystalUI()
        {
            scanBarFill.fillAmount = crystalController.scanProgress / 5f;
        }

        public void UpdatePlayerLife()
        {
            playerLifeBarFill.fillAmount = playerHealthMan.playerCurrentHealth / playerHealthMan.playerMaxHealth;
        }

        public void ChangeImageInCrystalSlot(Sprite sprite)
        {
            crystalSlot.GetComponent<Image>().color = Color.white;
            crystalSlot.GetComponent<Image>().sprite = sprite;
        }

        public void MakeUIElementAppear(GameObject whatToReveal)
        {
            whatToReveal.SetActive(true);
        }
    }
}