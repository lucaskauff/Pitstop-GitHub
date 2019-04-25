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
        Animator crystalAnim = default;
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
        private bool crystalUiAppearedCheck = false;

        void Start()
        {
            sceneLoader = GameManager.Instance.sceneLoader;
        }

        void Update()
        {
            SpecificScenesEvents();

            ScanProgressEvents();

            playerLifes.SetInteger("PlayerHealth", playerHealthMan.playerCurrentHealth);
        }

        public void SpecificScenesEvents()
        {
            if (sceneLoader.activeScene != "1_TEMPLE")
            {
                crystalAnim.SetBool("CrystalAlreadyThere", true);

                if (!playerLifesAppearedCheck)
                {
                    playerLifes.SetTrigger("Appear");
                    playerLifesAppearedCheck = true;
                }
            }
            else if (sceneLoader.activeScene == "3_VILLAGE")
            {
                //Same for now but playerLifesBar should disappear
                crystalAnim.SetBool("CrystalAlreadyThere", true);

                if (!playerLifesAppearedCheck)
                {
                    playerLifes.SetTrigger("Appear");
                    playerLifesAppearedCheck = true;
                }
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