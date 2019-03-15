using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Pitstop
{
    public class SceneLoader : MonoBehaviour
    {
        public string activeScene;
        public GameObject activeStartingPoint;
        //public int sceneIndex = 0;

        public void Awake()
        {
            activeScene = SceneManager.GetActiveScene().name;
            activeStartingPoint = GameObject.Find("StartingPoint0");
        }

        public void SaveStartingPoint(GameObject newStartingPoint)
        {
            activeStartingPoint = newStartingPoint;
        }

        public void LoadNewScene(string sceneToLoad)
        {
            activeScene = sceneToLoad;
            //sceneIndex += 1;
            SceneManager.LoadScene(sceneToLoad);
            activeStartingPoint = GameObject.Find("StartingPoint0");
        }

        public void ReloadScene()
        {
            SceneManager.LoadScene(activeScene);
        }
    }
}