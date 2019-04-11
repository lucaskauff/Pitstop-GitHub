using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Pitstop
{
    public class UIManager : MonoBehaviour
    {
        SceneLoader sceneLoader;

        [Header("Crystal UI"), SerializeField]
        public Image crystalSlot;
        public Animator crystalSlotCanvasAnim;

        public Image scanBarFill;
        public float fillPercentage = 0.821f;
        public RectTransform aiguille;
        public float aiguilleRotationSpeed = 1;        
        public float aiguilleOrientation0 = 40;
        public float aiguilleOrientation5 = 217;

        [Header("Player Related"), SerializeField]
        CrystalController crystalController = default;
        [SerializeField]
        PlayerHealthManager playerHealthMan = default;
        [SerializeField]
        Animator playerLifes = default;

        //Private
        private Quaternion aiguilleNewRotation;
        private bool playerLifesAppearedCheck = false;

        void Start()
        {
            sceneLoader = GameManager.Instance.sceneLoader;
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
                    if (!playerLifesAppearedCheck)
                    {
                        playerLifes.SetTrigger("Appear");
                        playerLifesAppearedCheck = true;
                    }

                    playerLifes.SetInteger("PlayerHealth", playerHealthMan.playerCurrentHealth);
                    break;

                case "2_1_MINIBOSS":                    
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