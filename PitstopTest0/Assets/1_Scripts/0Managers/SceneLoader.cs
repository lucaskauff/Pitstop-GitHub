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
                    if (gameManager.languageSetToEnglish) nextScene = "Act I : False Start";
                    else nextScene = "Acte I : Faux-depart";
                    break;

                case "2_FOREST":
                    if (gameManager.languageSetToEnglish) nextScene = "Act II : Back on the track";
                    else nextScene = "Acte II : Retour sur la piste";
                    break;

                case "2_MINIBOSS":
                    if (gameManager.languageSetToEnglish) nextScene = "Act III : Tight Turn";
                    else nextScene = "Acte III : Virage tendu";
                    break;

                case "3_VILLAGE":
                    if (gameManager.languageSetToEnglish) nextScene = "Act IV : Run-off";
                    else nextScene = "Acte IV : Zone degagee";
                    break;

                case "4_DUNGEON":
                    if (gameManager.languageSetToEnglish) nextScene = "Act V : Pole position";
                    else nextScene = "Acte V : Sprint final";
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
 