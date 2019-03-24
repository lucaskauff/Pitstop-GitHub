using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Pitstop
{
    public class SceneLoader : MonoBehaviour
    {
        public string activeScene;
        public GameObject activeStartingPoint = null;
        public bool startingPointFound = false;

        public void Awake()
        {
            activeScene = SceneManager.GetActiveScene().name;

            //Add all proper levels names
            if (activeScene == "1_TEMPLE" || activeScene == "2_FOREST" || activeScene == "3_VILLAGE")
            {
                activeStartingPoint = GameObject.Find("StartingPoint0");
            }
        }

        public void Update()
        {
            if (activeStartingPoint == null)
            {
                startingPointFound = false;
                activeStartingPoint = GameObject.Find("StartingPoint0");
            }
            else
            {
                startingPointFound = true;
            }
        }

        public void SaveStartingPoint(GameObject newStartingPoint)
        {
            activeStartingPoint = newStartingPoint;
        }

        public void LoadNewScene(string sceneToLoad)
        {
            activeStartingPoint = null;
            SceneManager.LoadScene(sceneToLoad);
            activeScene = sceneToLoad;            
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