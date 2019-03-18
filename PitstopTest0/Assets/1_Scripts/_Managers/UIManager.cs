using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Pitstop
{
    public class UIManager : MonoBehaviour
    {
        SceneLoader sceneLoader;
        InputManager inputManager;

        //Crystal
        public Image scanProgressBar;
        public Image crystalSlot;
        public CrystalController crystalController;
        public Animator crystalSlotCanvasAnim;

        public Image scanBarFill;
        public float fillPercentage = 0.821f;
        //private int scanBarAmount = 0;

        public RectTransform aiguille;
        public float aiguilleRotationSpeed = 1;
        private Quaternion aiguilleNewRotation;
        public float aiguilleOrientation0 = 40;
        public float aiguilleOrientation5 = 217;

        public Image playerLifes;
        public PlayerHealthManager playerHealthMan;

        //Enemy
        public Slider enemyHealthBar;
        public EnemyHealthManager enemyHealthMan;

        void Start()
        {
            sceneLoader = GameManager.Instance.sceneLoader;
            inputManager = GameManager.Instance.inputManager;
        }

        void Update()
        {
            SpecificScenesEvents();

            ScanProgressEvents();
        }

        public void SpecificScenesEvents()
        {
            switch (sceneLoader.activeScene)
            {
                case "1_TEMPLE":
                    break;

                case "2_FOREST":
                    playerLifes.GetComponent<Animator>().SetInteger("PlayerHealth", playerHealthMan.playerCurrentHealth);
                    break;

                case "2_1_MINIBOSS":
                    //will not be the case anymore
                    enemyHealthBar.maxValue = enemyHealthMan.enemyMaxHealth;
                    enemyHealthBar.value = enemyHealthMan.enemyCurrentHealth;
                    break;

                case "3_VILLAGE":
                    break;
            }
        }

        public void ScanProgressEvents()
        {
            aiguilleNewRotation = Quaternion.Euler(0, 0, -(((aiguilleOrientation5 - aiguilleOrientation0) / 5) * crystalController.scanProgress));
            aiguille.rotation = Quaternion.Lerp(aiguille.rotation, aiguilleNewRotation, Time.time * aiguilleRotationSpeed);

            scanBarFill.fillAmount = (fillPercentage / 5) * crystalController.scanProgress;
        }

        public void ChangeImageInCrystalSlot(Sprite sprite)
        {
            crystalSlot.GetComponent<Image>().color = Color.white;
            crystalSlot.GetComponent<Image>().sprite = sprite;
            //crystalSlot.GetComponent<Image>().SetNativeSize();
            crystalSlotCanvasAnim.SetTrigger("GoBlue");
        }

        public void MakeUIElementAppear(GameObject whatToReveal)
        {
            whatToReveal.SetActive(true);
        }
    }
}