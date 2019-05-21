using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pitstop
{
    public class PauseMenuManager : MonoBehaviour
    {
        SceneLoader sceneLoader;
        InputManager inputManager;

        [SerializeField] string sceneName = null;

        //UI Objects
        [SerializeField]
        GameObject Pause_Menu;
        [SerializeField] float pauseFadeTimer = 1;

        //Animation inputs
        public Animator anim;

        private void Start()
        {
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
        }

        IEnumerator PauseCoroutine()
        {
            yield return new WaitForSeconds(pauseFadeTimer);
            Time.timeScale = 0;
        }
    }
}