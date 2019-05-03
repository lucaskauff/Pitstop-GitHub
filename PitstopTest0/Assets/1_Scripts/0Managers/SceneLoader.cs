using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Pitstop
{
    public class SceneLoader : MonoBehaviour
    {
        public string activeScene;
        public string loadingScreenSceneName = "6_LOADINGSCREEN";
        public float loadingDelay = 3f;

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

        IEnumerator LoadingScreen(string sceneToLoad)
        {
            SceneManager.LoadScene(loadingScreenSceneName);
            yield return new WaitForSeconds(loadingDelay);
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}