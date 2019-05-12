using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Pitstop
{
    public class SceneLoader : MonoBehaviour
    {
        GameManager gameManager;

        public string activeScene;
        public string nextScene;
        public string loadingScreenSceneName = "6_LOADINGSCREEN";

        //for now : CANT TOUCH THIS
        public float loadingDelay = 6f;

        public void Awake()
        {
            activeScene = SceneManager.GetActiveScene().name;
        }

        public void Start()
        {
            gameManager = GameManager.Instance;
        }

        public void LoadNewScene(string sceneToLoad)
        {
            activeScene = sceneToLoad;
            StartCoroutine(LoadingScreen(sceneToLoad));
        }

        public void ReloadScene()
        {
            SceneManager.LoadScene(activeScene);
        }

        public void QuitGame()
        {
            Debug.Log("Game leaved.");
            Application.Quit();
        }

        public void SwitchSceneName(string sceneToLoadName)
        {
            switch (sceneToLoadName)
            {
                /* probably won't have a name
                case "0_PROLOGUE":
                    if (gameManager.languageSetToEnglish) nextScene = "PROLOGUE";
                    break;
                */

                case "1_TEMPLE":
                    if (gameManager.languageSetToEnglish) nextScene = "Act I : TEMPLE";
                    else nextScene = "Acte I : TEMPLE";
                    break;

                case "2_FOREST":
                    if (gameManager.languageSetToEnglish) nextScene = "Act II : FOREST";
                    else nextScene = "Acte II : FORET";
                    break;

                case "2_MINIBOSS":
                    if (gameManager.languageSetToEnglish) nextScene = "Act III : EERICK";
                    else nextScene = "Acte III : EERICK";
                    break;

                case "3_VILLAGE":
                    if (gameManager.languageSetToEnglish) nextScene = "Act IV : VILLAGE";
                    else nextScene = "Acte IV : VILLAGE";
                    break;

                case "4_DUNGEON":
                    if (gameManager.languageSetToEnglish) nextScene = "Act V : TERRITORY OF THE BEAST";
                    else nextScene = "Acte V : TERRITOIRE DE LA BETE";
                    break;

                /* probably won't have a name
                case "5_EPILOGUE":
                    nextScene = "EPILOGUE";
                    break;
                */
            }
        }

        IEnumerator LoadingScreen(string sceneToLoad)
        {
            SwitchSceneName(sceneToLoad);
            SceneManager.LoadScene(loadingScreenSceneName);
            yield return new WaitForSeconds(loadingDelay);
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
 