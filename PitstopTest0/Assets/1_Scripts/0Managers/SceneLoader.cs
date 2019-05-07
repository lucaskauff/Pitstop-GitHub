using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Pitstop
{
    public class SceneLoader : MonoBehaviour
    {
        public string activeScene;
        public string nextScene;
        public string loadingScreenSceneName = "6_LOADINGSCREEN";

        //for now : CANT TOUCH THIS
        public float loadingDelay = 6f;

        public void Awake()
        {
            activeScene = SceneManager.GetActiveScene().name;
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
                case "0_PROLOGUE":
                    nextScene = "PROLOGUE";
                    break;

                case "1_TEMPLE":
                    nextScene = "Act I : TEMPLE";
                    break;

                case "2_FOREST":
                    nextScene = "Act II : FOREST";
                    break;

                case "2_MINIBOSS":
                    nextScene = "";
                    break;

                case "3_VILLAGE":
                    nextScene = "Act III : VILLAGE";
                    break;

                case "4_DUNGEON":
                    nextScene = "Act IV : DUNGEON";
                    break;

                case "5_EPILOGUE":
                    nextScene = "EPILOGUE";
                    break;
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