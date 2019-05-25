using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pitstop
{
    public class MainMenuManager : MonoBehaviour
    {
        GameManager gameManager;
        SceneLoader sceneLoader;

        [SerializeField] string nextSceneToLoad = null;
        [SerializeField] MainMenuText[] mainMenuTexts = default;

        private void Start()
        {
            gameManager = GameManager.Instance;
            sceneLoader = GameManager.Instance.sceneLoader;
        }

        private void Update()
        {
            if (gameManager.languageSetToEnglish)
            {
                foreach (var text in mainMenuTexts)
                {
                    text.ChangeTextLanguageTo("English");
                }
            }
            else
            {
                foreach (var text in mainMenuTexts)
                {
                    text.ChangeTextLanguageTo("French");
                }
            }
        }

        public void Play()
        {
            sceneLoader.LoadNewScene(nextSceneToLoad);
        }

        public void ChangeLanguageTo(string language)
        {
            if (language == "French" && gameManager.languageSetToEnglish)
            {
                gameManager.languageSetToEnglish = false;

                /*
                foreach (var text in mainMenuTexts)
                {
                    text.ChangeTextLanguageTo(language);
                }
                */
            }
            else if (language == "English" && !gameManager.languageSetToEnglish)
            {
                gameManager.languageSetToEnglish = true;

                /*
                foreach (var text in mainMenuTexts)
                {
                    text.ChangeTextLanguageTo(language);
                }
                */
            }
        }

        public void Quit()
        {
            sceneLoader.QuitGame();
        }
    }
}