using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Pitstop
{
    public class SceneLoader : MonoBehaviour
    {
        public string activeScene;

        public void Start()
        {
            activeScene = SceneManager.GetActiveScene().name;
        }

        public void LoadNewScene(string sceneToLoad)
        {
            activeScene = sceneToLoad;
            SceneManager.LoadScene(sceneToLoad);
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
    }
}