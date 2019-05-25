using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pitstop
{
    public class PauseMenuManager : MonoBehaviour
    {
        GameManager gameManager;
        SceneLoader sceneLoader;
        InputManager inputManager;

        [SerializeField] string sceneName = null;

        //UI Objects
        [SerializeField]
        GameObject Pause_Menu;
        [SerializeField] float pauseFadeTimer = 0.3f;

        //Animation inputs
        public Animator anim;

        [SerializeField] MainMenuText[] pauseMenuTexts = default;

        private void Start()
        {
            gameManager = GameManager.Instance;
            sceneLoader = GameManager.Instance.sceneLoader;
            inputManager = GameManager.Instance.inputManager;
        }

        public void MainMenu()
        {
            Time.timeScale = 1;
            sceneLoader.LoadNewScene(sceneName);
        }

        public void ReloadTheScene()
        {
            Time.timeScale = 1;
            sceneLoader.ReloadScene();
        }

        private void Update()
        {
            //Pause Menu Interaction
            if (inputManager.escKey && Pause_Menu.activeSelf)
            {
                StopAllCoroutines();
                Time.timeScale = 1;
                Pause_Menu.SetActive(false);
            }
            else if (inputManager.escKey)
            {
                Pause_Menu.SetActive(true);
                StartCoroutine(PauseCoroutine());
            }

            if (gameManager.languageSetToEnglish)
            {
                foreach (var text in pauseMenuTexts)
                {
                    text.ChangeTextLanguageTo("English");
                }
            }
            else
            {
                foreach (var text in pauseMenuTexts)
                {
                    text.ChangeTextLanguageTo("French");
                }
            }
        }

        public void ChangeLanguageTo(string language)
        {
            if (language == "French" && gameManager.languageSetToEnglish)
            {
                gameManager.languageSetToEnglish = false;

                /*
                foreach (var text in pauseMenuTexts)
                {
                    text.ChangeTextLanguageTo(language);
                }
                */
            }
            else if (language == "English" && !gameManager.languageSetToEnglish)
            {
                gameManager.languageSetToEnglish = true;

                /*
                foreach (var text in pauseMenuTexts)
                {
                    text.ChangeTextLanguageTo(language);
                }
                */
            }
        }

        IEnumerator PauseCoroutine()
        {
            yield return new WaitForSeconds(pauseFadeTimer);
            Time.timeScale = 0;
        }
    }
}